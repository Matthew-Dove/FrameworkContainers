using FrameworkContainers.Network.SqlCollective;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Tests.FrameworkContainers.Network
{
    [Ignore] // Don't assume there is a mssql localdb instance available.
    [TestClass]
    public class SqlTests
    {
        private const string DATABASE = "DATABASE_NAME";

        [TestInitialize]
        public async Task Initialize()
        {
            Sql.ConnectionString = $"Server=(localdb)\\mssqllocaldb;Database={DATABASE};Integrated Security=true;";
            await CleanupDatabase();
            await CreateDatabase();
            await SeedDatabase();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await CleanupDatabase();
        }

        #region Database Setup

        private const string TABLE_NAME = "tblFrameworkContainersItems";
        private const string INSERT_PROC = "usp_frameworkcontainers_insert_item";
        private const string SELECT_PROC = "usp_frameworkcontainers_select_item";
        private const string SEARCH_PROC = "usp_frameworkcontainers_search_item";
        private const string UPDATE_PROC = "usp_frameworkcontainers_update_item";
        private const string DELETE_PROC = "usp_frameworkcontainers_delete_item";

        private static async Task<int> ExecuteSql(string sql)
        {
            using (var connection = new SqlConnection(Sql.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task CreateDatabase()
        {
            // Create table.
            var tableSql = $@"
                CREATE TABLE {TABLE_NAME} (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL,
                    Description NVARCHAR(255) NULL,
                    LastUpdated DATETIME2 DEFAULT GETUTCDATE()
                );";
            _ = await ExecuteSql(tableSql);

            // Create stored procedures.
            var insertUspSql = $@"
                CREATE PROCEDURE {INSERT_PROC}
                    @Name NVARCHAR(100),
                    @Description NVARCHAR(255)
                AS
                BEGIN
                    INSERT INTO {TABLE_NAME} (Name, Description)
                    VALUES (@Name, @Description);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                END";
            _ = await ExecuteSql(insertUspSql);

            var selectUspSql = $@"
                CREATE PROCEDURE {SELECT_PROC}
                    @Id INT
                AS
                BEGIN
                    SELECT Id, Name, Description, LastUpdated FROM {TABLE_NAME} WHERE Id = @Id;
                END";
            _ = await ExecuteSql(selectUspSql);

            var searchUspSql = $@"
                CREATE PROCEDURE {SEARCH_PROC}
                    @Name NVARCHAR(100)
                AS
                BEGIN
                    SELECT Id, Name, Description, LastUpdated FROM {TABLE_NAME} WHERE Name = @Name;
                END";
            _ = await ExecuteSql(searchUspSql);

            var updateUspSql = $@"
                CREATE PROCEDURE {UPDATE_PROC}
                    @Id INT,
                    @Description NVARCHAR(255)
                AS
                BEGIN
                    UPDATE {TABLE_NAME}
                    SET Description = @Description, LastUpdated = GETUTCDATE()
                    WHERE Id = @Id;
                END";
            _ = await ExecuteSql(updateUspSql);

            var deleteUspSql = $@"
                 CREATE PROCEDURE {DELETE_PROC}
                     @Id INT
                 AS
                 BEGIN
                     DELETE FROM {TABLE_NAME} WHERE Id = @Id;
                 END";
            _ = await ExecuteSql(deleteUspSql);
        }

        private async Task SeedDatabase()
        {
            // Seed table.
            var seedSql = $@"
                 INSERT INTO {TABLE_NAME} (Name, Description) VALUES
                 ('Initial Item 1', 'Desc 1'),
                 ('Initial Item 2', 'Desc 2');";
            _ = await ExecuteSql(seedSql);
        }

        private async Task CleanupDatabase()
        {
            // Cleanup stored procedures.
            var dropProcsSql = $@"
                IF OBJECT_ID('{INSERT_PROC}', 'P') IS NOT NULL DROP PROCEDURE {INSERT_PROC};
                IF OBJECT_ID('{SELECT_PROC}', 'P') IS NOT NULL DROP PROCEDURE {SELECT_PROC};
                IF OBJECT_ID('{SEARCH_PROC}', 'P') IS NOT NULL DROP PROCEDURE {SEARCH_PROC};
                IF OBJECT_ID('{UPDATE_PROC}', 'P') IS NOT NULL DROP PROCEDURE {UPDATE_PROC};
                IF OBJECT_ID('{DELETE_PROC}', 'P') IS NOT NULL DROP PROCEDURE {DELETE_PROC};";
            _ = await ExecuteSql(dropProcsSql);

            // Cleanup table.
            var dropTableSql = $"IF OBJECT_ID('{TABLE_NAME}', 'U') IS NOT NULL DROP TABLE {TABLE_NAME};";
            _ = await ExecuteSql(dropTableSql);
        }

        #endregion

        private class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        private static Item[] GetItems(IDataReader dr)
        {
            var items = new List<Item>();

            while (dr.Read())
            {
                items.Add(new Item
                {
                    Id = dr.Get<int>(nameof(Item.Id)),
                    Name = dr.Get<string>(nameof(Item.Name)),
                    Description = dr.Get<string>(nameof(Item.Description)),
                    LastUpdated = dr.Get<DateTime>(nameof(Item.LastUpdated))
                });
            }

            return items.ToArray();
        }

        [TestMethod]
        public async Task Insert_BulkItems()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            var guid = Guid.NewGuid().ToString();
            var desc = $"Item 2 shipped at: [{DateTime.UtcNow.ToString("s")}].";
            var item1 = new Item { Name = guid, Description = "Item 1" };
            var item2 = new Item { Name = guid, Description = desc };
            dataTable.Rows.Add(item1.Name, item1.Description);
            dataTable.Rows.Add(item2.Name, item2.Description);

            await Sql.BulkInsertAsync(TABLE_NAME, dataTable);

            var items = await Sql.ExecuteReaderAsync(
                GetItems,
                SEARCH_PROC,
                [new SqlParameter("@Name", guid)]
            );

            Assert.AreEqual(2, items.Length);
            Assert.IsTrue(item1.LastUpdated <= DateTime.UtcNow);
            Assert.AreEqual(guid, items[0].Name);
            Assert.AreEqual(desc, items[1].Description);
        }

        [TestMethod]
        public async Task Insert_Item()
        {
            var item = new Item { Name = "Item 11", Description = "Inserted Item" };

            var insert = await Sql.ExecuteScalarAsync<int>(
                INSERT_PROC,
                [new SqlParameter("@Name", item.Name), new SqlParameter("@Description", item.Description)]
            );

            Assert.IsTrue(insert > 0);
        }

        [TestMethod]
        public async Task Select_Item()
        {
            var guid = Guid.NewGuid().ToString();
            var item = new Item { Name = guid, Description = "Item to select" };

            var insert = await Sql.ExecuteScalarAsync<int>(
                INSERT_PROC,
                [new SqlParameter("@Name", item.Name), new SqlParameter("@Description", item.Description)]
            );

            var select = await Sql.ExecuteReaderAsync(
                GetItems,
                SELECT_PROC,
                [new SqlParameter("@Id", insert)]
            );

            Assert.AreEqual(1, select.Length);
            Assert.AreEqual(guid, select[0].Name);
        }

        [TestMethod]
        public async Task Update_Item()
        {
            var guid = Guid.NewGuid().ToString();
            var item = new Item { Name = "Item 22", Description = "This description has not been updated yet" };

            var insert = await Sql.ExecuteScalarAsync<int>(
                INSERT_PROC,
                [new SqlParameter("@Name", item.Name), new SqlParameter("@Description", item.Description)]
            );

            var update = await Sql.ExecuteNonQueryAsync(
                UPDATE_PROC,
                [new SqlParameter("@Id", insert), new SqlParameter("@Description", guid)]
            );

            var select = await Sql.ExecuteReaderAsync(
                GetItems,
                SELECT_PROC,
                [new SqlParameter("@Id", insert)]
            );

            Assert.AreEqual(1, select.Length);
            Assert.AreEqual(guid, select[0].Description);
        }

        [TestMethod]
        public async Task Delete_Item()
        {
            var item = new Item { Name = "Item 33", Description = "Item to delete" };

            var insert = await Sql.ExecuteScalarAsync<int>(
                INSERT_PROC,
                [new SqlParameter("@Name", item.Name), new SqlParameter("@Description", item.Description)]
            );

            var delete = await Sql.ExecuteNonQueryAsync(
                DELETE_PROC,
                [new SqlParameter("@Id", insert)]
            );

            var select = await Sql.ExecuteReaderAsync(
                GetItems,
                SELECT_PROC,
                [new SqlParameter("@Id", insert)]
            );

            Assert.AreEqual(0, select.Length);
        }
    }
}

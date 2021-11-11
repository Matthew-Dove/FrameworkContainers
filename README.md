# FrameworkContainers
Useful parts of Frameworks wrapped into single types.  

[PM> Install-Package FrameworkContainers](https://www.nuget.org/packages/FrameworkContainers/)  

## Intro

This library contains snippets of code I find myself adding over, and over again to new projects.  
The code is reusable, as it's not domain specific, though it is specific to the way I code (*I guess*). 

There is not a lot in here, as I didn't want to download the kitchen sink each time I needed a component from this library.  
The components are not complex, any developer could easily create them in a few hours, to the same quality.  
That said, it's rather the point; I want those few hours back for each new project (*and I'm sick of copy-pasting!*).  

## Components

Each **component** has a `static` core, from here the methods can be accessed easily inline; as a standard `type` (*i.e. no dependency injection required*).  
If dependency injection is needed, then each **component** also have a **Client** `type`, with an accompanying `interface`.  

The **components** have been designed to handle *most* use cases, with the true aim to be simple for the developer to use.  
If you find some component does not meet your needs, then drop it and write custom code instead of fighting the framework.  
The components are built to be performant, and have the best defaults set; they are hard to use in a wrong way.  

There are three distinct ways to use a component (*excluding the static / client options mentioned above*).  
You can use the raw type `T`, which will throw any encountered exceptions.  
You can use `Response<T>`, which will internally log any exceptions, and only return a value of `T` when it can.  
You can use `Maybe<T, Exception>`, which will return either `T`, or the error type `Exception`.  
If you would like to know more about types: `Response`, and `Maybe`; please visit their repo [ContainerExpressions](https://github.com/Matthew-Dove/ContainerExpressions).

## Http Component

Send, and receive all the things over the network.  
This component has support for `get`, `post`, `put`, and `delete`; with both `sync`, and `async` APIs.  
The `sync` APIs are built on top of `System.Net.WebRequest`.  
The `async` APIs are built on top of `System.Net.Http.HttpClient`.  

### [Things we do for you]

  * Set a default timeout of 30 seconds.
  * Set the content type, and content length.
  * Write in UTF8.
  * Read error responses, adding them to the generated `HttpException`.
  * Renew DNS every minute (*for the domain(s) you are calling*).
  * Disable insecure ciphers, only leaving up to date ones active.

### [Examples]

Get http body `T`:
```cs
string body = Http.Get("/api/weather");
```

Get http body `Response<T>`:
```cs
Response<string> body = Http.Response.Get("/api/weather");
```

Get http body `Maybe<T, Exception>`:
```cs
Maybe<string, HttpException> body = Http.Response.Get("/api/weather");
```

Get http body `IHttpClient`:
```cs
IHttpClient http = new HttpClient();
string body = http.Get("/api/weather");
```

Create a new `User` from an `Onboard` request object using `JSON`, and the `async` API:
```cs
User user = await Http.PostJsonAsync<User, Onboard>(request);
```

## Json Component

Serialize, and deserialize JSON to, and from domain models.  
This component's APIs are built on top of `System.Text.Json.JsonSerializer`.  

### [Things we do for you]

  * Two serializer categories: *Performant*, and *Permissive*; one for speed, and one that "just works".
  * JSON converters for standard types to "just work", they include: `bool`, `DateTime`, `Enum`, `Float`, `Guid`, `Int`, and `Long`.

### [Examples]

Json to model:
```cs
string json = "{\"name\": \"John Smith\"}";
User user = Json.ToModel<User>(json);
```

Model to json:
```cs
User user = new User { Name = "John Smith" };
string json = Json.FromModel(user);
```

Setting the JSON options:
```cs
User user = Json.ToModel<User>(json, JsonOptions.Performant);
```

Using a JSON converter (*strings are converted to ints, nulls are converetd to 0, invalid values still throw errors*):
```cs
[JsonConverter(typeof(DefaultIntConverter))]
public int Age { get; set; }
```

using `Response<T>`:
```cs
Response<User> user = Json.Response.ToModel<User>(json);
```

Using `Maybe<T, Exception>`:
```cs
Maybe<User, FormatDeserializeException> user = Json.Maybe.ToModel<User>(json);
```

Using `IJsonClient`:
```cs
IJsonClient json = new JsonClient();
User user = json.ToModel<User>(json);
```

## Xml Component

Serialize, and deserialize XML to, and from domain models.  
This component's APIs are built on top of `System.Xml.Serialization.XmlSerializer`.  

### [Things we do for you]

  * Remove default namespaces, and omit the XML declaration.
  * Allow custom character encodings.

### [Examples]

XML to model:
```cs
User user = Xml.ToModel<User>(xml);
```

Model to XML:
```cs
string xml = Xml.FromModel(user);
```

Setting the XML options:
```cs
string xml = Xml.FromModel(user, new XmlOptions(true, true, null));
```

using `Response<T>`:
```cs
Response<User> user = Xml.Response.ToModel<User>(json);
```

Using `Maybe<T, Exception>`:
```cs
Maybe<User, FormatDeserializeException> user = Xml.Maybe.ToModel<User>(json);
```

Using `IXmlClient`:
```cs
IXmlClient xml = new XmlClient();
User user = xml.ToModel<User>(json);
```

## Sql Component

Insert, update, select, and delete your way though the database (*via stored procedures*).  
This component's APIs are built on top of `System.Data.SqlClient.SqlConnection`.  

### [Things we do for you]

  * Provide both `sync`, and `async` APIs for: `ExecuteReader`, `ExecuteNonQuery`, and `BulkInsert`.
  * Allow easy access to returned values with the `Get<T>` extension method.

### [Examples]

Setting the global connection string:
```cs
Sql.ConnectionString = connectionString;
```

ExecuteNonQuery:
```cs
int rows = Sql.ExecuteNonQuery("usp_insert_user", new SqlParameter("@name", "John Smith"));
```

ExecuteNonQuery, overriding the global connection string:
```cs
int rows = Sql.ExecuteNonQuery("usp_insert_user", connectionString, new SqlParameter("@name", "John Smith"));
```

Async ExecuteReader using `Get<T>`:
```cs
IEnumerable<User> Read(IDataReader dr)
{
	List<User> results = new List<User>();
	while (dr.Read())
	{
		results.Add(new User
		{
			name = dr.Get<string>("Name")
		});
	}
	return results;
}

IEnumerable<User> users = await Sql.ExecuteReaderAsync(Read, "usp_select_users");
```

BulkInsert:
```cs
DataTable table = new DataTable();
table.Columns.Add("Name");
foreach (User user in users)
{
	var row = table.NewRow();
	row["Name"] = user.Name;
	table.Rows.Add(row);
}
table.AcceptChanges();

Sql.BulkInsert("tblUsers", table);
```

using `Response<T>`:
```cs
Response<int> rows = Sql.Response.ExecuteNonQuery("usp_insert_user", new SqlParameter("@name", "John Smith"));
```

Using `Maybe<T, Exception>`:
```cs
Maybe<int, Exception> rows = Sql.Maybe.ExecuteNonQuery("usp_insert_user", new SqlParameter("@name", "John Smith"));
```

Using `ISqlClient`:
```cs
ISqlClient sql = new SqlClient();
int rows = sql.ExecuteNonQuery("usp_insert_user", new SqlParameter("@name", "John Smith"));
```

# Credits
* [Icon](https://www.flaticon.com/free-icon/bird_2630452) made by [Vitaly Gorbachev](https://www.flaticon.com/authors/vitaly-gorbachev) from [Flaticon](https://www.flaticon.com/).

# Changelog

## 2.0.0

* Started a changelog - reset the project.
* Added `Sql` for `ExecuteReader`, `ExecuteNonQuery`, and `BulkInsert` database operations.
* Added `Http` for `Get`, `Pos`t`, `Put`, and `Delete` network operations.
* Added `Json` for `Serialize`, and `Deserialize` model operations.
* Added `Xml` for `Serialize`, and `Deserialize` model operations.
* Updated nuget pack settings, to make the project easier to deploy.
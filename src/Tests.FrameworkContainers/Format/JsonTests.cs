using FrameworkContainers.Format;
using FrameworkContainers.Models.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Tests.FrameworkContainers.Format
{
    [TestClass]
    public class JsonTests
    {
        /**
         * JSON TESTS:
         * 
         * int => ""
         * int => null
         * enum => ""
         * enum => null
         * enum => "local"
         * enum => int
         * string => null
         * case => Case
         * guid => string
         * datetime => string
         * string => JSON
         * bool => "true"
         * bool => "True"
         * bool => "1"
         * object => {}
         * array => []
         * 
         * Errors: To / From Model.
        **/
        #region Test Models

        private enum Enum
        {
            Zero = 0,
            One = 1
        }

        // Model to test out standard serializer usage.
        private interface IVanilla
        {
            bool Bool { get; set; }
            char Char { get; set; }
            decimal Decimal { get; set; }
            double Double { get; set; }
            float Float { get; set; }
            int Int { get; set; }
            long Long { get; set; }
            string String { get; set; }
            Guid Guid { get; set; }
            DateTime DateTime { get; set; }
            Enum Enum { get; set; }
            object Object { get; set; }
            object[] Array { get; set; }
        }

        private class VanillaReference : IVanilla
        {
            public bool Bool { get; set; }
            public char Char { get; set; }
            public decimal Decimal { get; set; }
            public double Double { get; set; }
            public float Float { get; set; }
            public int Int { get; set; }
            public long Long { get; set; }
            public string String { get; set; }
            public Guid Guid { get; set; }
            public DateTime DateTime { get; set; }
            public Enum Enum { get; set; }
            public object Object { get; set; }
            public object[] Array { get; set; }
        }

        private struct VanillaValueeeee : IVanilla
        {
            public bool Bool { get; set; }
            public char Char { get; set; }
            public decimal Decimal { get; set; }
            public double Double { get; set; }
            public float Float { get; set; }
            public int Int { get; set; }
            public long Long { get; set; }
            public string String { get; set; }
            public Guid Guid { get; set; }
            public DateTime DateTime { get; set; }
            public Enum Enum { get; set; }
            public object Object { get; set; }
            public object[] Array { get; set; }
        }

        private class Model
        {
            [JsonConverter(typeof(DefaultEnumConverter<Enum>))]
            public Enum Enum { get; set; }

            [JsonConverter(typeof(DefaultIntConverter))]
            public int Int { get; set; }
        }

        #endregion

        #region Standard Serialization

        [TestMethod]
        public void Std_Bool()
        {
            var target = true;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Bool = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Bool = target }));

            Assert.AreEqual(target, reference.Bool);
            Assert.AreEqual(target, valueeeee.Bool);
        }

        [TestMethod]
        public void Std_Char()
        {
            var target = 'A';

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Char = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Char = target }));

            Assert.AreEqual(target, reference.Char);
            Assert.AreEqual(target, valueeeee.Char);
        }

        [TestMethod]
        public void Std_Decimal()
        {
            var target = 420.69M;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Decimal = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Decimal = target }));

            Assert.AreEqual(target, reference.Decimal);
            Assert.AreEqual(target, valueeeee.Decimal);
        }

        [TestMethod]
        public void Std_Double()
        {
            var target = 420.69D;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Double = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Double = target }));

            Assert.AreEqual(target, reference.Double);
            Assert.AreEqual(target, valueeeee.Double);
        }

        [TestMethod]
        public void Std_Float()
        {
            var target = 420.69F;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Float = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Float = target }));

            Assert.AreEqual(target, reference.Float);
            Assert.AreEqual(target, valueeeee.Float);
        }

        [TestMethod]
        public void Std_Int()
        {
            var target = 420;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Int = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Int = target }));

            Assert.AreEqual(target, reference.Int);
            Assert.AreEqual(target, valueeeee.Int);
        }

        [TestMethod]
        public void Std_Long()
        {
            var target = 420L;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Long = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Long = target }));

            Assert.AreEqual(target, reference.Long);
            Assert.AreEqual(target, valueeeee.Long);
        }

        [TestMethod]
        public void Std_String()
        {
            var target = "420";

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { String = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { String = target }));

            Assert.AreEqual(target, reference.String);
            Assert.AreEqual(target, valueeeee.String);
        }

        [TestMethod]
        public void Std_Guid()
        {
            var target = Guid.NewGuid();

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Guid = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Guid = target }));

            Assert.AreEqual(target, reference.Guid);
            Assert.AreEqual(target, valueeeee.Guid);
        }

        [TestMethod]
        public void Std_DateTime()
        {
            var target = DateTime.Now;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { DateTime = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { DateTime = target }));

            Assert.AreEqual(target, reference.DateTime);
            Assert.AreEqual(target, valueeeee.DateTime);
        }

        [TestMethod]
        public void Std_Enum()
        {
            var target = Enum.One;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Enum = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Enum = target }));

            Assert.AreEqual(target, reference.Enum);
            Assert.AreEqual(target, valueeeee.Enum);
        }

        [TestMethod]
        public void Std_Object()
        {
            var target = (object)420;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Object = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Object = target }));

            Assert.AreEqual(target.ToString(), reference.Object.ToString());
            Assert.AreEqual(target.ToString(), valueeeee.Object.ToString());
        }

        [TestMethod]
        public void Std_Array()
        {
            var target = new object[] { 420, "420.69", 420.69F };

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Array = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Array = target }));

            Assert.AreEqual(string.Join(",", target.Select(x => x.ToString())), string.Join(",", reference.Array.Select(x => x.ToString())));
            Assert.AreEqual(string.Join(",", target.Select(x => x.ToString())), string.Join(",", valueeeee.Array.Select(x => x.ToString())));
        }

        #endregion

        #region EdgeCase Serialization

        [TestMethod]
        public void EC_Enum_StringName()
        {
            var json = $"{{\"Enum\": \"One\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_StringNumber()
        {
            var json = $"{{\"Enum\": \"1\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_Int()
        {
            var json = $"{{\"Enum\": 1}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_NullString()
        {
            var json = $"{{\"Enum\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(Enum.Zero, model.Enum);
        }

        [TestMethod]
        public void EC_Enum_EmptyString()
        {
            var json = $"{{\"Enum\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(Enum.Zero, model.Enum);
        }

        [TestMethod]
        public void EC_Int_StringValue()
        {
            var json = $"{{\"Int\": \"1\"}}";

            var reference = Json.ToModel<VanillaReference>(json, JsonOptions.Permissive);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json, JsonOptions.Permissive);

            Assert.AreEqual(1, reference.Int);
            Assert.AreEqual(1, valueeeee.Int);
        }

        [TestMethod]
        public void EC_Int_EmptyString()
        {
            var json = $"{{\"Int\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0, model.Int);
        }

        [TestMethod]
        public void EC_Int_NullString()
        {
            var json = $"{{\"Int\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0, model.Int);
        }

        #endregion
    }
}

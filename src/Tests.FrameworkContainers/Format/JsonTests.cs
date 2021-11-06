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
         * object => {}
         * array => []
         * case => Case
         * 
         * GetInt64
         * GetSingle
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

            [JsonConverter(typeof(DefaultGuidConverter))]
            public Guid Guid { get; set; }

            [JsonConverter(typeof(DefaultDateTimeConverter))]
            public DateTime DateTime { get; set; }

            [JsonConverter(typeof(DefaultBoolConverter))]
            public bool Bool { get; set; }

            [JsonConverter(typeof(DefaultFloatConverter))]
            public float Float { get; set; }

            [JsonConverter(typeof(DefaultLongConverter))]
            public long Long { get; set; }
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
        public void EC_Enum_Value()
        {
            var target = Enum.One;
            var json = $"{{\"Enum\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Enum);
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

        [TestMethod]
        public void EC_Int_Value()
        {
            var target = 1;
            var json = $"{{\"Int\": {target}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Int);
        }

        [TestMethod]
        public void EC_String_Empty()
        {
            var target = string.Empty;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { String = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { String = target }));

            Assert.AreEqual(target, reference.String);
            Assert.AreEqual(target, valueeeee.String);
        }

        [TestMethod]
        public void EC_String_Null()
        {
            var target = (string)null;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { String = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { String = target }));

            Assert.AreEqual(target, reference.String);
            Assert.AreEqual(target, valueeeee.String);
        }

        [TestMethod]
        public void EC_Guid_Default()
        {
            var target = new Guid();

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Guid = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Guid = target }));

            Assert.AreEqual(target, reference.Guid);
            Assert.AreEqual(target, valueeeee.Guid);
        }

        [TestMethod]
        public void EC_Guid_String()
        {
            var target = Guid.NewGuid();
            var json = $"{{\"Guid\": \"{target}\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(target, reference.Guid);
            Assert.AreEqual(target, valueeeee.Guid);
        }

        [TestMethod]
        public void EC_Guid_EmptyString()
        {
            var json = $"{{\"Guid\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new Guid(), model.Guid);
        }

        [TestMethod]
        public void EC_Guid_NullString()
        {
            var json = $"{{\"Guid\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new Guid(), model.Guid);
        }

        [TestMethod]
        public void EC_Guid_Value()
        {
            var target = Guid.NewGuid();
            var json = $"{{\"Guid\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Guid);
        }

        [TestMethod]
        public void EC_DateTime_EmptyString()
        {
            var json = $"{{\"DateTime\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new DateTime(), model.DateTime);
        }

        [TestMethod]
        public void EC_DateTime_NullString()
        {
            var json = $"{{\"DateTime\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new DateTime(), model.DateTime);
        }

        [TestMethod]
        public void EC_DateTime_Value()
        {
            var target = DateTime.Now;
            var json = $"{{\"DateTime\": \"{target:o}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.DateTime);
        }

        [TestMethod]
        public void EC_Bool_Value()
        {
            var target = true;
            var json = $"{{\"Bool\": {target.ToString().ToLower()}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringTrue()
        {
            var target = true;
            var json = $"{{\"Bool\": \"{target.ToString().ToLower()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringFalse()
        {
            var target = false;
            var json = $"{{\"Bool\": \"{target.ToString().ToLower()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringTrueUpperCase()
        {
            var target = true;
            var json = $"{{\"Bool\": \"{target.ToString().ToUpper()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringEmpty()
        {
            var json = $"{{\"Bool\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringNull()
        {
            var json = $"{{\"Bool\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringIntTrue()
        {
            var json = $"{{\"Bool\": \"1\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(true, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringIntFalse()
        {
            var json = $"{{\"Bool\": \"0\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_IntTrue()
        {
            var json = $"{{\"Bool\": 1}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(true, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_IntFalse()
        {
            var json = $"{{\"Bool\": 0}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Float_EmptyString()
        {
            var json = $"{{\"Float\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Float);
        }

        [TestMethod]
        public void EC_Float_NullString()
        {
            var json = $"{{\"Float\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Float);
        }

        [TestMethod]
        public void EC_Float_Value()
        {
            var target = 1F;
            var json = $"{{\"Float\": {target}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Float);
        }

        [TestMethod]
        public void EC_Float_ValueString()
        {
            var target = 1F;
            var json = $"{{\"Float\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Float);
        }

        [TestMethod]
        public void EC_Long_EmptyString()
        {
            var json = $"{{\"Long\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Long);
        }

        [TestMethod]
        public void EC_Long_NullString()
        {
            var json = $"{{\"Long\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0L, model.Long);
        }

        [TestMethod]
        public void EC_Long_Value()
        {
            var target = 1L;
            var json = $"{{\"Long\": {target}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Long);
        }

        [TestMethod]
        public void EC_Long_ValueString()
        {
            var target = 1L;
            var json = $"{{\"Long\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Long);
        }

        #endregion
    }
}

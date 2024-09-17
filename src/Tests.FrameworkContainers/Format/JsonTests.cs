using ContainerExpressions.Containers;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Format.JsonCollective.Models;
using FrameworkContainers.Format.JsonCollective.Models.Converters;
using FrameworkContainers.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json.Serialization;

namespace Tests.FrameworkContainers.Format
{
    [TestClass]
    public class JsonTests
    {
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

            [JsonConverter(typeof(SmartEnumConverterUpperCase<Colour>))]
            public EnumRange<Colour> SmartEnum { get; set; }

            [JsonConverter(typeof(SmartEnumConverter<NoZero>))]
            public EnumRange<NoZero> SmartEnumNoZero { get; set; }

            public string PascalCase { get; set; }

            public string camelCase { get; set; }
        }

        private class ErrorModel
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Error { get { throw new Exception("Error!!!"); } }
        }

        private class Colour : SmartEnum
        {
            public static readonly Colour None = new(0);
            public static readonly Colour Red = new(1);
            public static readonly Colour Green = new(2);

            private Colour(int value) : base(value) { }
        }

        private class NoZero : SmartEnum
        {
            public static readonly NoZero One = new(1);

            private NoZero(int value) : base(value) { }
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
        public void Std_Object_Null()
        {
            var target = (object)null;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Object = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Object = target }));

            Assert.AreEqual(target, reference.Object);
            Assert.AreEqual(target, valueeeee.Object);
        }

        [TestMethod]
        public void Std_Array()
        {
            var target = new object[] { 420, "420.69", 420.69F };

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Array = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Array = target }));

            Assert.AreEqual(float.Parse(target[0].ToString()), float.Parse(reference.Array[0].ToString()));
            Assert.AreEqual(float.Parse(target[1].ToString()), float.Parse(reference.Array[1].ToString()));
            Assert.AreEqual(float.Parse(target[2].ToString()), float.Parse(reference.Array[2].ToString()));

            Assert.AreEqual(float.Parse(target[0].ToString()), float.Parse(valueeeee.Array[0].ToString()));
            Assert.AreEqual(float.Parse(target[1].ToString()), float.Parse(valueeeee.Array[1].ToString()));
            Assert.AreEqual(float.Parse(target[2].ToString()), float.Parse(valueeeee.Array[2].ToString()));
        }

        [TestMethod]
        public void Std_Empty()
        {
            var target = new object[] { };

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Array = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Array = target }));

            Assert.AreEqual(target.Length, reference.Array.Length);
            Assert.AreEqual(target.Length, valueeeee.Array.Length);
        }

        [TestMethod]
        public void Std_Array_Null()
        {
            var target = (object[])null;

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Array = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Array = target }));

            Assert.AreEqual(target, reference.Array);
            Assert.AreEqual(target, valueeeee.Array);
        }

        [TestMethod]
        public void Std_Array_Null_Value()
        {
            var target = new object[] { null, 1 };

            var reference = Json.ToModel<VanillaReference>(Json.FromModel(new VanillaReference { Array = target }));
            var valueeeee = Json.ToModel<VanillaValueeeee>(Json.FromModel(new VanillaValueeeee { Array = target }));

            Assert.AreEqual(target.Length, reference.Array.Length);
            Assert.AreEqual(target.Length, valueeeee.Array.Length);
            Assert.AreEqual(target[0], reference.Array[0]);
            Assert.AreEqual(target[1].ToString(), reference.Array[1].ToString());
            Assert.AreEqual(target[0], valueeeee.Array[0]);
            Assert.AreEqual(target[1].ToString(), valueeeee.Array[1].ToString());
        }

        #endregion

        #region EdgeCase Serialization

        [TestMethod]
        public void EC_Enum_StringName()
        {
            var json = $"{{\"enum\": \"One\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_StringNumber()
        {
            var json = $"{{\"enum\": \"1\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_Int()
        {
            var json = $"{{\"enum\": 1}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(Enum.One, reference.Enum);
            Assert.AreEqual(Enum.One, valueeeee.Enum);
        }

        [TestMethod]
        public void EC_Enum_NullString()
        {
            var json = $"{{\"enum\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(Enum.Zero, model.Enum);
        }

        [TestMethod]
        public void EC_Enum_EmptyString()
        {
            var json = $"{{\"enum\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(Enum.Zero, model.Enum);
        }

        [TestMethod]
        public void EC_Enum_Value()
        {
            var target = Enum.One;
            var json = $"{{\"enum\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Enum);
        }

        [TestMethod]
        public void EC_Int_StringValue()
        {
            var json = $"{{\"int\": \"1\"}}";

            var reference = Json.ToModel<VanillaReference>(json, JsonOptions.Permissive);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json, JsonOptions.Permissive);

            Assert.AreEqual(1, reference.Int);
            Assert.AreEqual(1, valueeeee.Int);
        }

        [TestMethod]
        public void EC_Int_EmptyString()
        {
            var json = $"{{\"int\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0, model.Int);
        }

        [TestMethod]
        public void EC_Int_NullString()
        {
            var json = $"{{\"int\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0, model.Int);
        }

        [TestMethod]
        public void EC_Int_Value()
        {
            var target = 1;
            var json = $"{{\"int\": {target}}}";

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
            var json = $"{{\"guid\": \"{target}\"}}";

            var reference = Json.ToModel<VanillaReference>(json);
            var valueeeee = Json.ToModel<VanillaValueeeee>(json);

            Assert.AreEqual(target, reference.Guid);
            Assert.AreEqual(target, valueeeee.Guid);
        }

        [TestMethod]
        public void EC_Guid_EmptyString()
        {
            var json = $"{{\"guid\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new Guid(), model.Guid);
        }

        [TestMethod]
        public void EC_Guid_NullString()
        {
            var json = $"{{\"guid\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new Guid(), model.Guid);
        }

        [TestMethod]
        public void EC_Guid_Value()
        {
            var target = Guid.NewGuid();
            var json = $"{{\"guid\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Guid);
        }

        [TestMethod]
        public void EC_DateTime_EmptyString()
        {
            var json = $"{{\"dateTime\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new DateTime(), model.DateTime);
        }

        [TestMethod]
        public void EC_DateTime_NullString()
        {
            var json = $"{{\"dateTime\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new DateTime(), model.DateTime);
        }

        [TestMethod]
        public void EC_DateTime_Value()
        {
            var target = DateTime.Now;
            var json = $"{{\"dateTime\": \"{target:o}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.DateTime);
        }

        [TestMethod]
        public void EC_Bool_Value()
        {
            var target = true;
            var json = $"{{\"bool\": {target.ToString().ToLower()}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringTrue()
        {
            var target = true;
            var json = $"{{\"bool\": \"{target.ToString().ToLower()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringFalse()
        {
            var target = false;
            var json = $"{{\"bool\": \"{target.ToString().ToLower()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringTrueUpperCase()
        {
            var target = true;
            var json = $"{{\"bool\": \"{target.ToString().ToUpper()}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringEmpty()
        {
            var json = $"{{\"bool\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringNull()
        {
            var json = $"{{\"bool\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringIntTrue()
        {
            var json = $"{{\"bool\": \"1\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(true, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_StringIntFalse()
        {
            var json = $"{{\"bool\": \"0\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_IntTrue()
        {
            var json = $"{{\"bool\": 1}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(true, model.Bool);
        }

        [TestMethod]
        public void EC_Bool_IntFalse()
        {
            var json = $"{{\"bool\": 0}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(false, model.Bool);
        }

        [TestMethod]
        public void EC_Float_EmptyString()
        {
            var json = $"{{\"float\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Float);
        }

        [TestMethod]
        public void EC_Float_NullString()
        {
            var json = $"{{\"float\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Float);
        }

        [TestMethod]
        public void EC_Float_Value()
        {
            var target = 1F;
            var json = $"{{\"float\": {target}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Float);
        }

        [TestMethod]
        public void EC_Float_ValueString()
        {
            var target = 1F;
            var json = $"{{\"float\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Float);
        }

        [TestMethod]
        public void EC_Long_EmptyString()
        {
            var json = $"{{\"long\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0F, model.Long);
        }

        [TestMethod]
        public void EC_Long_NullString()
        {
            var json = $"{{\"long\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(0L, model.Long);
        }

        [TestMethod]
        public void EC_Long_Value()
        {
            var target = 1L;
            var json = $"{{\"long\": {target}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Long);
        }

        [TestMethod]
        public void EC_Long_ValueString()
        {
            var target = 1L;
            var json = $"{{\"long\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.Long);
        }

        [TestMethod]
        public void EC_ModelPascalCase_JsonCamelCase()
        {
            var target = "Hello World!";
            var json = $"{{\"pascalCase\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json, JsonOptions.PerformantCamelCase);

            Assert.AreEqual(target, model.PascalCase);
        }

        [TestMethod]
        public void EC_ModelPascalCase_JsonPascal()
        {
            var target = "Hello World!";
            var json = $"{{\"PascalCase\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json, JsonOptions.Performant);

            Assert.AreEqual(target, model.PascalCase);
        }

        [TestMethod]
        public void EC_ModelCamelCase_JsonPascalCase()
        {
            var target = "Hello World!";
            var json = $"{{\"CamelCase\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json, JsonOptions.Permissive);

            Assert.AreEqual(target, model.camelCase);
        }

        [TestMethod]
        public void EC_ModelCamelCase_JsonCamelCase()
        {
            var target = "Hello World!";
            var json = $"{{\"camelCase\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.camelCase);
        }

        [TestMethod]
        public void EC_SmartEnum_EmptyString()
        {
            var json = $"{{\"smartEnum\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.IsTrue(model.SmartEnum == Colour.None);
        }

        [TestMethod]
        public void EC_SmartEnum_NullString()
        {
            var json = $"{{\"smartEnum\": null}}";

            var model = Json.ToModel<Model>(json);

            Assert.IsTrue(model.SmartEnum == Colour.None);
        }

        [TestMethod]
        public void EC_SmartEnum_StringValue()
        {
            var target = SmartEnum<Colour>.FromNames("red,green");
            var json = $"{{\"smartEnum\": \"{target}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.SmartEnum);
        }

        [TestMethod]
        public void EC_SmartEnum_IntValue()
        {
            var target = SmartEnum<Colour>.FromNames("red,green");
            var json = $"{{\"smartEnum\": {3}}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(target, model.SmartEnum);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatDeserializeException))]
        public void EC_SmartEnum_StringInvalidValue()
        {
            var json = $"{{\"smartEnum\": \"{"blue"}\"}}";
            var model = Json.ToModel<Model>(json);
        }

        [TestMethod]
        public void EC_SmartEnum_StringInvalidValue_NoException()
        {
            var json = $"{{\"smartEnum\": \"{"blue"}\"}}";
            var model = Json.Response.ToModel<Model>(json);
            Assert.IsFalse(model);
        }

        [TestMethod]
        public void EC_SmartEnum_SerializeModel()
        {
            var model = new Model { SmartEnum = SmartEnum<Colour>.FromObjects(Colour.Red, Colour.Green) };

            var json = Json.FromModel(model);

            Assert.IsTrue(json.Contains("RED,"));
            Assert.IsFalse(json.Contains("red,"));
        }

        [TestMethod]
        public void EC_SmartEnum_StringIntValue()
        {
            var json = $"{{\"smartEnum\": \"{3}\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(SmartEnum<Colour>.FromObjects(Colour.Red, Colour.Green), model.SmartEnum);
        }

        [TestMethod]
        public void EC_SmartEnum_EmptyString_NoZero()
        {
            var json = $"{{\"smartEnumNoZero\": \"\"}}";

            var model = Json.ToModel<Model>(json);

            Assert.AreEqual(new EnumRange<NoZero>(), model.SmartEnumNoZero);
        }

        #endregion

        #region Serialization Errors

        [TestMethod]
        public void Error_Deserialize_IsCustomType()
        {
            var isCustomType = false;
            var json = $"{{\"int\": \"{420.69}\"}}";

            try
            {
                var model = Json.ToModel<VanillaReference>(json);
            }
            catch (FormatDeserializeException)
            {
                isCustomType = true;
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isCustomType);
        }

        [TestMethod]
        public void Error_Deserialize_Input()
        {
            var isInputValid = false;
            var json = $"{{\"int\": \"{420.69}\"}}";

            try
            {
                var model = Json.ToModel<VanillaReference>(json);
            }
            catch (FormatDeserializeException fde)
            {
                isInputValid = fde.Input.Equals(json);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isInputValid);
        }

        [TestMethod]
        public void Error_Deserialize_TargetType()
        {
            var isTargetTypeValid = false;
            var json = $"{{\"int\": \"{420.69}\"}}";

            try
            {
                var model = Json.ToModel<VanillaReference>(json);
            }
            catch (FormatDeserializeException fde)
            {
                isTargetTypeValid = fde.TargetType.Equals(typeof(VanillaReference));
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isTargetTypeValid);
        }

        [TestMethod]
        public void Error_Deserialize_Format()
        {
            var isFormatValid = false;
            var json = $"{{\"int\": \"{420.69}\"}}";

            try
            {
                var model = Json.ToModel<VanillaReference>(json);
            }
            catch (FormatDeserializeException fde)
            {
                isFormatValid = fde.Format.Equals(FormatRange.Json);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isFormatValid);
        }

        [TestMethod]
        public void Error_Serialize_IsCustomType()
        {
            var isCustomType = false;
            var model = new ErrorModel();

            try
            {
                var json = Json.FromModel(model);
            }
            catch (FormatSerializeException)
            {
                isCustomType = true;
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isCustomType);
        }

        [TestMethod]
        public void Error_Serialize_Model()
        {
            var isModelValid = false;
            var model = new ErrorModel();

            try
            {
                var json = Json.FromModel(model);
            }
            catch (FormatSerializeException fse)
            {
                isModelValid = fse.Model is ErrorModel em && em.Id.Equals(model.Id);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isModelValid);
        }

        [TestMethod]
        public void Error_Serialize_Format()
        {
            var isFormatValid = false;
            var model = new ErrorModel();

            try
            {
                var json = Json.FromModel(model);
            }
            catch (FormatSerializeException fse)
            {
                isFormatValid = fse.Format.Equals(FormatRange.Json);
            }
            catch (Exception)
            {

            }

            Assert.IsTrue(isFormatValid);
        }

        #endregion

        #region Response Maybe

        [TestMethod]
        public void Response_Deserialize_IsValid()
        {
            var target = 420;
            var json = $"{{\"int\": {target}}}";

            var model = Json.Response.ToModel<Model>(json);

            Assert.IsTrue(model);
            Assert.AreEqual(target, model.Value.Int);
        }

        [TestMethod]
        public void Response_Deserialize_IsNotValid()
        {
            var json = "{\"int\": \"420.69\"}";

            var model = Json.Response.ToModel<Model>(json);

            Assert.IsFalse(model);
        }

        [TestMethod]
        public void Response_Serialize_IsValid()
        {
            var model = new Model { Int = 420 };

            var json = Json.Response.FromModel(model);
            var result = json.Bind(Json.Response.ToModel<Model>);

            Assert.IsTrue(json);
            Assert.IsTrue(result);
            Assert.AreEqual(model.Int, result.Value.Int);
        }

        [TestMethod]
        public void Response_Serialize_IsNotValid()
        {
            var model = new ErrorModel();

            var json = Json.Response.FromModel(model);
            var result = json.Bind(Json.Response.ToModel<Model>);

            Assert.IsFalse(json);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Maybe_Deserialize_IsValid()
        {
            var target = 420;
            var json = $"{{\"int\": {target}}}";

            var model = Json.Maybe.ToModel<Model>(json);

            Assert.IsTrue(model.Match(x => target == x.Int, _ => false));
        }

        [TestMethod]
        public void Maybe_Deserialize_IsNotValid()
        {
            var json = "{\"int\": \"420.69\"}";

            var model = Json.Maybe.ToModel<Model>(json);

            Assert.IsFalse(model.Match(_ => true, _ => false));
        }

        [TestMethod]
        public void Maybe_Deserialize_IsNotValid_HasJson()
        {
            var json = "{\"int\": \"420.69\"}";

            var model = Json.Maybe.ToModel<Model>(json);
            var input = model.Match(_ => string.Empty, x => ((FormatDeserializeException)x).Input);

            Assert.AreEqual(json, input);
        }

        [TestMethod]
        public void Maybe_Serialize_IsValid()
        {
            var model = new Model { Int = 420 };

            var json = Json.Maybe.FromModel(model);

            Assert.IsTrue(json.Match(_ => true, _ => false));
            Assert.IsTrue(json.Match(x => x.Contains(model.Int.ToString()), _ => false));
        }

        [TestMethod]
        public void Maybe_Serialize_IsNotValid()
        {
            var model = new ErrorModel();

            var json = Json.Maybe.FromModel(model);

            Assert.IsFalse(json.Match(_ => true, _ => false));
        }
        #endregion
    }
}

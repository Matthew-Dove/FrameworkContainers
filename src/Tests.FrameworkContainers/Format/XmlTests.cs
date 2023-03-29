using FrameworkContainers.Format.XmlCollective;
using FrameworkContainers.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.FrameworkContainers.Format
{
    [TestClass]
    public class XmlTests
    {
        #region Test Models

        public interface IIDoNotMakeARight
        {
            Guid Id { get; set; }
            string Name { get; set; }
        }

        public class Reference : IIDoNotMakeARight
        {
            public Guid Id { get; set; }
            public string Name { get { return _name == "error" ? throw new Exception("Invalid name.") : _name; } set { _name = value; } }
            private string _name;

            public override bool Equals(object obj) => obj is Reference r && r.Id == Id;
            public override int GetHashCode() => Id.GetHashCode();
        }

        public struct Valueeeee : IIDoNotMakeARight
        {
            public Guid Id { get; set; }
            public string Name { get { return _name == "error" ? throw new Exception("Invalid name.") : _name; } set { _name = value; } }
            private string _name;

            public override bool Equals(object obj) => obj is Valueeeee v && v.Id == Id;
            public override int GetHashCode() => Id.GetHashCode();
        }

        #endregion

        [TestMethod]
        public void ModelToXml()
        {
            var id = Guid.NewGuid();

            var referenceXml = Xml.FromModel(new Reference { Id = id, Name = "John Smith" });
            var valueeeeeXml = Xml.FromModel(new Valueeeee { Id = id, Name = "Jane Smith" });

            var referenceModel = Xml.ToModel<Reference>(referenceXml);
            var valueeeeeModel = Xml.ToModel<Valueeeee>(valueeeeeXml);

            Assert.AreEqual(referenceModel.Id, valueeeeeModel.Id);
        }

        [TestMethod]
        public void XmlToModel()
        {
            var id = Guid.NewGuid();

            var referenceXml = @$"<Reference>
  <Id>{id}</Id>
  <Name>John Smith</Name>
</Reference>";
            var valueeeeeXml = @$"<Valueeeee>
  <Id>{id}</Id>
  <Name>Jane Smith</Name>
</Valueeeee>";

            var referenceModel = Xml.ToModel<Reference>(referenceXml);
            var valueeeeeModel = Xml.ToModel<Valueeeee>(valueeeeeXml);

            Assert.AreEqual(referenceModel.Id, valueeeeeModel.Id);
        }

        [TestMethod]
        public void Error_Deserialize()
        {
            var isError = false;
            var xml = @$"<Valueeeee>
  <Id>{Guid.NewGuid()}</Id>
  <Name>John Smith</Name>
</Valueeeee>";

            try
            {
                var model = Xml.ToModel<Reference>(xml);
            }
            catch (FormatDeserializeException fde)
            {
                isError = fde.Format == FormatRange.Xml && fde.TargetType == typeof(Reference) && fde.Input == xml;
            }

            Assert.IsTrue(isError);
        }

        [TestMethod]
        public void Error_Serialize()
        {
            var isError = false;
            var model = new Reference { Id = Guid.NewGuid(), Name = "error" };

            try
            {
                var xml = Xml.FromModel(model);
            }
            catch (FormatSerializeException fse)
            {
                isError = fse.Format == FormatRange.Xml && fse.Model is Reference r && r.Id == model.Id;
            }

            Assert.IsTrue(isError);
        }
    }
}

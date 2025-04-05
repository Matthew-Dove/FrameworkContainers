using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkContainers.Format.XmlCollective;
using System;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Network.SqlCollective;
using FrameworkContainers.Network.HttpCollective;

namespace Tests.FrameworkContainers.Infrastructure
{
    [TestClass]
    public class DependencyInjectionTests
    {
        private const string _assembly = "Tests.FrameworkContainers";

        private IServiceCollection _services;

        [TestInitialize]
        public void Initialize()
        {
            _services = new ServiceCollection();
        }

        [TestMethod]
        public void FindServicesForInterfacesByNamingConvention()
        {
            _services.AddServicesByConvention(_assembly);
            var sp = _services.BuildServiceProvider();

            var message = sp.GetService<IMessage>();
            var pong = message.GetMessage();

            Assert.AreEqual("Pong", pong);
        }

        [TestMethod]
        public void FindInternalClassesForDI()
        {
            _services.AddServicesByConvention(_assembly, scanInternals: true);
            var sp = _services.BuildServiceProvider();

            var hidden = sp.GetService<IHiddenService>();
            var aboo = hidden.Peek();

            Assert.AreEqual("aboo", aboo);
        }

        [TestMethod]
        public void AddXmlClient()
        {
            _services.AddServicesByConvention(_assembly);
            _services.AddFrameworkXmlClient();
            var sp = _services.BuildServiceProvider();

            var client = sp.GetService<IXmlSerializer>();
            var payload = new Model();

            var xml = client.ToXml(payload);
            var model = client.FromXml(xml);

            Assert.AreEqual(payload.Id, model.Id);
        }

        [TestMethod]
        public void AddJsonClient()
        {
            _services.AddServicesByConvention(_assembly);
            _services.AddFrameworkJsonClient();
            var sp = _services.BuildServiceProvider();

            var client = sp.GetService<IJsonSerializer>();
            var payload = new Model();

            var json = client.ToJson(payload);

            Assert.IsTrue(json.Contains(payload.Id.ToString()));
        }

        [TestMethod]
        public void AddSqlClient()
        {
            _services.AddServicesByConvention(_assembly);
            _services.AddFrameworkSqlClient();
            var sp = _services.BuildServiceProvider();

            var client = sp.GetService<ISqlClient>();

            Assert.IsNotNull(client);
            Assert.IsInstanceOfType<SqlClient>(client);
        }

        [TestMethod]
        public void AddHttpClient()
        {
            _services.AddServicesByConvention(_assembly);
            _services.AddFrameworkHttpClient();
            var sp = _services.BuildServiceProvider();

            var client = sp.GetService<IHttpClient<Model, Model>>();
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType<HttpClient<Model, Model>>(client);

            var maybe = sp.GetService<IHttpMaybe<Model, Model>>();
            Assert.IsNotNull(maybe);
            Assert.IsInstanceOfType<HttpMaybe<Model, Model>>(maybe);

            var response = sp.GetService<IHttpResponse<Model, Model>>();
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType<HttpResponse<Model, Model>>(response);
        }

        [TestMethod]
        public void AddEverythingEverywhereAllAtOnce()
        {
            _services.AddServicesByConvention(_assembly);
            _services.AddFrameworkEverythingEverywhereAllAtOnce();
            var sp = _services.BuildServiceProvider();

            var xml = sp.GetService<IXmlClient>();
            Assert.IsNotNull(xml);
            Assert.IsInstanceOfType<IXmlClient>(xml);

            var json = sp.GetService<IJsonClient>();
            Assert.IsNotNull(json);
            Assert.IsInstanceOfType<IJsonClient>(json);

            var sql = sp.GetService<ISqlClient>();
            Assert.IsNotNull(sql);
            Assert.IsInstanceOfType<ISqlClient>(sql);

            var http = sp.GetService<IHttpClient>();
            Assert.IsNotNull(http);
            Assert.IsInstanceOfType<IHttpClient>(http);
        }
    }

    #region DI Test Services

    public interface IHiddenService
    {
        string Peek();
    }

    internal class HiddenService : IHiddenService
    {
        public string Peek() => "aboo";
    }

    public interface IPingService
    {
        string Ping();
    }

    public class PingService : IPingService
    {
        public string Ping() => "Pong";
    }

    public interface IMessage
    {
        string GetMessage();
    }

    public class Message : IMessage
    {
        private readonly IPingService _ping;

        public Message(IPingService ping)
        {
            _ping = ping;
        }

        public string GetMessage() => _ping.Ping();
    }

    public class Model
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public interface IXmlSerializer
    {
        string ToXml(Model model);
        Model FromXml(string xml);
    }

    public class XmlSerializer : IXmlSerializer
    {
        private readonly IXmlClient<Model> _client;

        public XmlSerializer(IXmlClient<Model> client)
        {
            _client = client;
        }

        public Model FromXml(string xml) => _client.ToModel(xml);

        public string ToXml(Model model) => _client.FromModel(model);
    }

    public interface IJsonSerializer
    {
        string ToJson<T>(T model);
    }

    public class JsonSerializer : IJsonSerializer
    {
        private readonly IJsonClient _client;

        public JsonSerializer(IJsonClient client)
        {
            _client = client;
        }

        public string ToJson<T>(T model) => _client.FromModel(model);
    }

    #endregion
}

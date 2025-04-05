using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using FrameworkContainers.Format.JsonCollective;
using FrameworkContainers.Network.HttpCollective;
using FrameworkContainers.Network.HttpCollective.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.FrameworkContainers.Network
{
    [TestClass]
    public class ApiTests
    {
        private static HttpServer _server;
        private static string _url;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            _url = $"http://localhost:{GetPort()}";
            _server = new HttpServer($"{_url}/");

            _server.GetHandler = (req) => new { Message = "GET Success", Path = req.Url.PathAndQuery };
            _server.PostHandler = (req, body) => new { Message = "POST Success", ReceivedData = Json.ToModel<JsonElement>(body) };
            _server.PutHandler = (req, body) => new { Message = "PUT Success", ReceivedData = Json.ToModel<JsonElement>(body) };
            _server.PatchHandler = (req, body) => new { Message = "PATCH Success", ReceivedData = Json.ToModel<JsonElement>(body) };

            _server.Start();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _server.Dispose();
        }

        private static int GetPort()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            var endPoint = (IPEndPoint)socket.LocalEndPoint;
            return endPoint.Port;
        }

        #region Json Http Methods

        [TestMethod]
        public async Task Get()
        {
            var result = await Http.GetJsonAsync<JsonElement>($"{_url}/api/test");

            Assert.AreEqual("GET Success", result.GetProperty("message").GetString());
            Assert.AreEqual("/api/test", result.GetProperty("path").GetString());
        }

        [TestMethod]
        public async Task Post()
        {
            var data = new { Name = "Created User", Email = "created@example.com" };

            var result = await Http.PostJsonAsync<object, JsonElement>(data, $"{_url}/api/users");

            Assert.AreEqual("POST Success", result.GetProperty("message").GetString());
            var receivedData = result.GetProperty("receivedData");
            Assert.AreEqual("Created User", receivedData.GetProperty("name").GetString());
            Assert.AreEqual("created@example.com", receivedData.GetProperty("email").GetString());
        }

        [TestMethod]
        public async Task Put()
        {
            var data = new { Id = 1, Name = "Updated User", Email = "updated@example.com" };

            var result = await Http.PutJsonAsync<object, JsonElement>(data, $"{_url}/api/users/1");

            Assert.AreEqual("PUT Success", result.GetProperty("message").GetString());
            var receivedData = result.GetProperty("receivedData");
            Assert.AreEqual(1, receivedData.GetProperty("id").GetInt32());
            Assert.AreEqual("Updated User", receivedData.GetProperty("name").GetString());
            Assert.AreEqual("updated@example.com", receivedData.GetProperty("email").GetString());
        }

        [TestMethod]
        public async Task Patch()
        {
            var data = new { Name = "Partially Updated User" };

            var result = await Http.PatchJsonAsync<object, JsonElement>(data, $"{_url}/api/users/1");

            Assert.AreEqual("PATCH Success", result.GetProperty("message").GetString());
            var receivedData = result.GetProperty("receivedData");
            Assert.AreEqual("Partially Updated User", receivedData.GetProperty("name").GetString());
        }

        [TestMethod]
        public async Task Delete()
        {
            var result = await Http.DeleteStatusAsync($"{_url}/api/users/1");

            Assert.AreEqual("No Content", result);
        }

        #endregion

        [TestMethod]
        public async Task CaptureHttpBody()
        {
            var data = new { Name = "capture me" };
            string request = null, response = null;
            var log = HttpLogger.Create((requestBody, responseBody) => { request = requestBody; response = responseBody; return ValueTask.CompletedTask; });

            _ = await Http.PostJsonAsync<object, JsonElement>(data, $"{_url}/api/users", log);

            Assert.IsFalse(string.IsNullOrEmpty(request));
            Assert.IsFalse(string.IsNullOrEmpty(response));

            var result = Json.ToModel<JsonElement>(response);

            Assert.AreEqual("POST Success", result.GetProperty("message").GetString());
            var receivedData = result.GetProperty("receivedData");
            Assert.AreEqual("capture me", receivedData.GetProperty("name").GetString());
        }
    }
}
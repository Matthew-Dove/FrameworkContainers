using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Models.Network;
using FrameworkContainers.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests.FrameworkContainers.Network
{
    [TestClass]
    public class HttpTests
    {
        private const string _mediaType = "application/json";

        private static string GetUrl(string path) => $"http://localhost:8080/api/mock{path}";

        [TestMethod]
        public void Post()
        {
            try
            {
                var response = Http.Post("{}", GetUrl("/error"), _mediaType, new Header("Authorization", "Bearer QWxhZGRpbjpvcGVuIHNlc2FtZQ=="));
            }
            catch (HttpException he)
            {

            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public async Task PostAsync()
        {
            try
            {
                var response = await Http.PostAsync("{}", GetUrl("/error"), _mediaType, new Header("Authorization", "Bearer QWxhZGRpbjpvcGVuIHNlc2FtZQ=="));
            }
            catch (HttpException he)
            {

            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

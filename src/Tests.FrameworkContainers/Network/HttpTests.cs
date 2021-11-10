﻿using FrameworkContainers.Models.Exceptions;
using FrameworkContainers.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests.FrameworkContainers.Network
{
    [TestClass]
    public class HttpTests
    {
        private const string _contentType = "application/json";

        private static string GetUrl(string path) => $"http://localhost:8080/api/mock{path}";

        [TestMethod]
        public void Post()
        {
            try
            {
                var response = Http.Response.Post("{}", GetUrl("/create"), _contentType);
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
                var response = await Http.Response.PostAsync("{}", GetUrl("/create"), _contentType);
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

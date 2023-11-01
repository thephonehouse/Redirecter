using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Redirecter;
using Redirecter.Controllers;
using RedirecterCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedirecterTest
{
    public class RedirectController_Test
    {
        private static readonly IRedirectService redirectService = new RedirectServiceFactory().CreateExampleService();
        
        private readonly RedirectController controller = new (
                logger: NullLoggerFactory.Instance.CreateLogger<RedirectController>(),
                redirectService: redirectService);

        /// <summary>
        /// Test if an request for an known id returns an redirect
        /// </summary>
        [Fact]
        public void Test_RedirectId()
        { 
            foreach (var model in redirectService.Models)
            {
                var result = controller.GetRedirect(model.Id);

                Assert.NotNull(result);
                Assert.IsType<RedirectResult>(result);

                var rResult = result as RedirectResult;

                Assert.True(rResult?.Url == model.Url);

            }
        }

        /// <summary>
        /// Test if an request for an unkown id returns an not found
        /// </summary
        [Theory]
        [InlineData("29196D38-BE5C-4463-A159-9FB6608622F9")]
        public void Test_RedirectMissingId(Guid id)
        {
            var result = controller.GetRedirect(id);

            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);

            var rResult = result as ContentResult;

            Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Test if an request for an known name returns an redirect
        /// </summary>
        [Fact]
        public void Test_RedirectName()
        {
            foreach (var model in redirectService.Models)
            {
                var result = controller.GetRedirect(model.Name);

                Assert.NotNull(result);
                Assert.IsType<RedirectResult>(result);

                var rResult = result as RedirectResult;

                Assert.True(rResult?.Url == model.Url);

            }
        }

        /// <summary>
        /// Test if an request for an unknow name returns an not found
        /// </summary>
        [Theory]
        [InlineData("test")]
        [InlineData("googl")]
        [InlineData("hilfe")]
        public void Test_RedirectMissingName(string name)
        {
            var result = controller.GetRedirect(name);

            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);

            var rResult = result as ContentResult;

            Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Test if an invalid name parameter returns an bad request
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData(null)]
        [InlineData("c3po-45")]
        [InlineData("@pple")]
        [InlineData("_halloWelt")]
        public void Test_RedirectInvalidName(string name)
        {
            var result = controller.GetRedirect(name);

            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);

            var rResult = result as ContentResult;

            Assert.True(rResult?.StatusCode == StatusCodes.Status400BadRequest);
        }


    }
}

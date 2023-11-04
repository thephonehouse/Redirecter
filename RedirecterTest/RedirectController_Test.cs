using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Redirecter;
using Redirecter.Controllers;
using RedirecterCore;
using RedirecterCore.StorageProviders;
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

        private readonly RedirectController controller = new(
                logger: NullLoggerFactory.Instance.CreateLogger<RedirectController>(),
                redirectService: redirectService);

        #region ValidUntil

        private static readonly IRedirectService validUntilService = new RedirectServiceFactory()
            .SetRedirectServiceProvider(new RedirectStorageProviderObject(new RedirectModel[]
            {
                new RedirectModel("https://google.com", "google", validUntil: DateTimeOffset.Parse("08.12.2020")) {Id = Guid.NewGuid() },
                new RedirectModel("https://example.org", "example", validUntil: DateTimeOffset.Parse("01.01.1970")) {Id = Guid.NewGuid() }
            }))
            .CreateService();

        private readonly RedirectController validUntilController = new(
                logger: NullLoggerFactory.Instance.CreateLogger<RedirectController>(),
                redirectService: validUntilService);

        #endregion

        #region NotValidBefore

        private static readonly IRedirectService notValidBeforeService = new RedirectServiceFactory()
            .SetRedirectServiceProvider(new RedirectStorageProviderObject(new RedirectModel[]
            {
                new RedirectModel("https://google.com", "google", notValidBefore: DateTimeOffset.Parse("08.12.3065")) {Id = Guid.NewGuid() },
                new RedirectModel("https://example.org", "example", notValidBefore: DateTimeOffset.Parse("01.01.2056")) {Id = Guid.NewGuid() }
            }))
            .CreateService();        

        private readonly RedirectController notValidBeforeController = new(
                logger: NullLoggerFactory.Instance.CreateLogger<RedirectController>(),
                redirectService: notValidBeforeService);

        #endregion

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
        /// Test if an request for an known id with invalid valid until property returns an not found
        /// </summary>
        [Fact]
        public void Test_ValidUntilRedirectId()
        {
            foreach (var model in validUntilService.Models)
            {
                var result = validUntilController.GetRedirect(model.Id);

                Assert.NotNull(result);
                Assert.IsType<ContentResult>(result);

                var rResult = result as ContentResult;

                Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);

            }

        }

        /// <summary>
        /// Test if an request for an known id with invalid not valid before property returns an not found
        /// </summary>
        [Fact]
        public void Test_NotValidBeforeRedirectId()
        {
            foreach (var model in notValidBeforeService.Models)
            {
                var result = notValidBeforeController.GetRedirect(model.Id);

                Assert.NotNull(result);
                Assert.IsType<ContentResult>(result);

                var rResult = result as ContentResult;

                Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);

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
        /// Test if an request for an known id with invalid valid until property returns an not found
        /// </summary>
        [Fact]
        public void Test_ValidUntilRedirecNamed()
        {
            foreach (var model in validUntilService.Models)
            {
                var result = validUntilController.GetRedirect(model.Name);

                Assert.NotNull(result);
                Assert.IsType<ContentResult>(result);

                var rResult = result as ContentResult;

                Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);

            }

        }

        /// <summary>
        /// Test if an request for an known id with invalid not valid before property returns an not found
        /// </summary>
        [Fact]
        public void Test_NotValidBeforeRedirectName()
        {
            foreach (var model in notValidBeforeService.Models)
            {
                var result = notValidBeforeController.GetRedirect(model.Name);

                Assert.NotNull(result);
                Assert.IsType<ContentResult>(result);

                var rResult = result as ContentResult;

                Assert.True(rResult?.StatusCode == StatusCodes.Status404NotFound);

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

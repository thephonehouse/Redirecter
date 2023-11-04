using Microsoft.Extensions.Logging.Abstractions;
using Redirecter.Controllers;
using Redirecter;
using RedirecterCore.StorageProviders;
using RedirecterCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RedirecterTest
{
    public class Test_RedirectServiceFactory
    {
        private static readonly RedirectStorageProviderObject storageObject = new(new RedirectModel[]
        {
            new RedirectModel("https://google.com", "google") {Id = Guid.NewGuid() },
            new RedirectModel("https://example.org", "example", validUntil: DateTimeOffset.Parse("01.01.1970")) {Id = Guid.NewGuid() }
        });

        private static readonly RedirectStorageProviderObject invalidStorageObjectName = new(new RedirectModel[]
        {
            new RedirectModel("https://google.com", "google") {Id = Guid.NewGuid() },
            new RedirectModel("https://google.com", "google") {Id = Guid.NewGuid() }
        });

        private static readonly RedirectStorageProviderObject invalidStorageObjectId = new(new RedirectModel[]
        {
            new RedirectModel("https://google.com", "google") {Id = Guid.Empty },
            new RedirectModel("https://google.com", "google") {Id = Guid.Empty },
        });

        private static readonly RedirectStorageProviderExample example = RedirectStorageProviderExample.Instance;


        /// <summary>
        /// Test if an service is created
        /// </summary>
        [Fact]
        public void Test_FactorySetup()
        {
            var service = new RedirectServiceFactory()
                .SetRedirectServiceProvider(storageObject)
                .CreateService();
            
            Assert.NotNull(service);
        }

        [Fact]
        public void Test_FactorySetupWithDoubleName()
        {
            Assert.ThrowsAny<RedirectServiceFactoryException>(
                () => new RedirectServiceFactory()
                .SetRedirectServiceProvider(invalidStorageObjectName)
                .CreateService());
        }

        [Fact]
        public void Test_FactorySetupWithDoubleId()
        {
            Assert.ThrowsAny<RedirectServiceFactoryException>(
                () => new RedirectServiceFactory()
                .SetRedirectServiceProvider(invalidStorageObjectId)
                .CreateService());
        }

        [Fact]
        public void Test_FactorySetupWithExample()
        {
            var service = new RedirectServiceFactory()
                .CreateExampleService();

            Assert.IsType<RedirectService>(service);

            var redirectService = service as RedirectService;

            Assert.NotNull(redirectService);
            Assert.Equal(redirectService.ServiceProvider, RedirectStorageProviderExample.Instance);
        }


    }
}

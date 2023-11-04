using RedirecterCore;
using RedirecterCore.StorageProviders;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Redirecter
{
    public class RedirectServiceFactory
    {

        private IRedirectStorageProvider _serviceProvider = new RedirectStorageProviderObject();

        public RedirectServiceFactory()
        {

        }

        public RedirectServiceFactory SetRedirectServiceProvider(IRedirectStorageProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
            return this;
        }

        public void CheckForValidServiceProvider()
        {
            var models = _serviceProvider.Models;

            if (TestIfUnique(models, x => x.Id)) throw new RedirectServiceFactoryException("Ids are not unique!");

            if (TestIfUnique(models, x => x.Name)) throw new RedirectServiceFactoryException("Names are not unique!");

        }

        private static bool TestIfUnique<T>(IEnumerable<RedirectModel> models, Func<RedirectModel, T> func) =>
            models.Select(func).Count() != models.Select(func).Distinct().Count();


        public IRedirectService CreateService()
        {
            CheckForValidServiceProvider();

            return new RedirectService(_serviceProvider);
        }

        public IRedirectService CreateExampleService()
        {
            return new RedirectService(RedirectStorageProviderExample.Instance);
        }
    }
}

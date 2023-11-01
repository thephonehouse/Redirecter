using RedirecterCore;
using RedirecterCore.StorageProviders;
using System.Runtime.InteropServices;

namespace Redirecter
{
    public class RedirectServiceFactory 
    {

        private IRedirectStorageProvider _serviceProvider = new RedirectStorageProviderExample();

        public RedirectServiceFactory()
        {
            
        }

        public RedirectServiceFactory SetRedirectServiceProvider(IRedirectStorageProvider serviceProvider)
        {
            
            _serviceProvider = serviceProvider;
            return this; 
        }

        public IRedirectService CreateService()
        { 
            return new RedirectService(_serviceProvider);
        }

        public IRedirectService CreateExampleService()
        {
            return new RedirectService(new RedirectStorageProviderExample());
        }
    }
}

using RedirecterCore;
using System.Runtime.InteropServices;

namespace Redirecter
{
    public class RedirectServiceFactory
    {
        private string path = string.Empty;

        public RedirectServiceFactory()
        { 
        }

        private void Parse()
        {

        }

        public IRedirectService Create()
        {
            var list = new List<RedirectModel>();

            return new RedirectService(list);
        }

        public IRedirectService CreateExampleService()
        {
            return new RedirectService(new List<RedirectModel>
            {
                new RedirectModel("https://google.com", "google"),
                new RedirectModel("https://example.org", "example")
            });
        }
    }
}

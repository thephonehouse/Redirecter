using RedirecterCore;
using System.Runtime.CompilerServices;

namespace Redirecter
{
    public class RedirectService : IRedirectService
    {
        public IEnumerable<RedirectModel> Models { get => serviceProvider.Models; }

        internal IRedirectStorageProvider serviceProvider;

        internal RedirectService(IRedirectStorageProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public RedirectModel? GetById(Guid id)
        {
            return serviceProvider.GetById(id);
        }

        public RedirectModel? GetByName(string name)
        {
            return serviceProvider.GetByName(name.ToLower());
        }

         
    }
}

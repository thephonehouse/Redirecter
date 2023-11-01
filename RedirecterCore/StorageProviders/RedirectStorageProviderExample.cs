using Redirecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirecterCore.StorageProviders
{
    public class RedirectStorageProviderExample : IRedirectStorageProvider
    {
        public IEnumerable<RedirectModel> Models { get => models; }

        private readonly static RedirectModel[] models = new RedirectModel[]
        {
            new RedirectModel("https://google.com", "google") {Id = Guid.Parse("00000000-0000-0000-0000-000000000000")},
            new RedirectModel("https://example.org", "example") {Id = Guid.Parse("00000000-0000-0000-0000-000000000001")}
        };

        public RedirectModel? GetById(Guid id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public RedirectModel? GetByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }
    }
}

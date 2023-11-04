using Redirecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedirecterCore.StorageProviders
{
    public sealed class RedirectStorageProviderObject : IRedirectStorageProvider
    {
        public IEnumerable<RedirectModel> Models => models;
        private readonly List<RedirectModel> models;

        public RedirectStorageProviderObject(IEnumerable<RedirectModel>? models = null)
        {
            this.models = (List<RedirectModel>)(models is not null ? models.ToList() : new List<RedirectModel>());
        }

        public RedirectModel? GetById(Guid id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public RedirectModel? GetByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }

        public void AddModel(RedirectModel model) => models.Add(model);

        public void AddModels(IEnumerable<RedirectModel> models) => this.models.AddRange(models);
    }
}

using RedirecterCore;
using System.Runtime.CompilerServices;

namespace Redirecter
{
    public class RedirectService : IRedirectService
    {
        public IEnumerable<RedirectModel> Models { get => models; }

        internal RedirectModel[] models;

        internal RedirectService()
        {
            models = Array.Empty<RedirectModel>();
        }

        internal RedirectService(IEnumerable<RedirectModel>models)
        {
            this.models = models.ToArray();
        }

        public RedirectModel? GetById(Guid id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public RedirectModel? GetByName(string name)
        {
            string name2Lower = name.ToLower();

            return models.FirstOrDefault(x => x.Name == name2Lower);
        }
    }
}

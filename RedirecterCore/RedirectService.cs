using RedirecterCore;
using System.Runtime.CompilerServices;

namespace Redirecter
{
    public class RedirectService : IRedirectService
    {
        public IEnumerable<RedirectModel> Models { get => serviceProvider.Models; }

        internal IRedirectStorageProvider serviceProvider;

        /// <inheritdoc/>
        internal RedirectService(IRedirectStorageProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public RedirectModel? GetById(Guid id)
        {
            var model = serviceProvider.GetById(id);

            if (model is not null && CheckValidity(model) is 0) model = null;

            return model;
        }

        /// <inheritdoc/>
        public RedirectModel? GetByName(string name)
        {
            var model = serviceProvider.GetByName(name.ToLower());

            if (model is not null && CheckValidity(model) is 0) model = null;

            return model;
        }

        /// <summary>
        /// Check if the validity of Date and time are valid
        /// </summary>
        /// <param name="model">the redirect model</param>
        /// <returns>0 if valid; -1 if not valid before; 1 if not valid until</returns>
        private int CheckValidity(RedirectModel model)
        {
            // Check if valid before is in the past
            if (model.NotValidBefore > DateTimeOffset.UtcNow)
            {
                return -1;
            }

            // Check if valid until is in the future
            if (model.ValidUntil < DateTimeOffset.UtcNow)
            {
                return 1;
            }


            return 0;
        }
         
    }
}

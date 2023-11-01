using Redirecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirecterCore
{
    public interface IRedirectService
    {
        /// <summary>
        /// An IEnumerable of the available Redirect Models
        /// </summary>
        public IEnumerable<RedirectModel> Models { get; }

        /// <summary>
        /// Get the RedirectModel by its id
        /// </summary>
        /// <param name="id">the id of the redirect model</param>
        /// <returns>an redirect model if found or null if not found</returns>
        public RedirectModel? GetById(Guid id);

        /// <summary>
        /// Get the RedirectModel by its complete name
        /// Method is case-insensitiv
        /// </summary>
        /// <param name="name">the name of the redirect model</param>
        /// <returns>an redirect model if found or null if not found</returns>
        public RedirectModel? GetByName(string name);
    }
}

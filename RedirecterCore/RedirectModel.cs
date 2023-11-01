using System.ComponentModel.DataAnnotations;

namespace Redirecter
{
    public class RedirectModel
    {
        /// <summary>
        /// Primary Id of the Redirect URL
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// The URL that should be redirected
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Optional comment
        /// </summary>
        [Required]
        public string Name { get; set; }

        public RedirectModel() : this(string.Empty, string.Empty)
        {

        }

        public RedirectModel(string url, string name)
        {
            this.Url = url;
            this.Name = name;   
        }
    }
}

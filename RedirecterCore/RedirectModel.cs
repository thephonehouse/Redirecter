using System.ComponentModel.DataAnnotations;

namespace Redirecter
{
    public class RedirectModel
    {
        /// <summary>
        /// Primary Id of the Redirect URL
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The URL that should be redirected
        /// </summary>
        [Required]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Optional comment
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        public RedirectModel()
        {

        }

        public RedirectModel(int id, string url)
        {
            this.Id = id;
            this.Url = url;
        }
    }
}

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

        /// <summary>
        /// This model is valid until this date
        /// </summary>
        public DateTimeOffset ValidUntil { get; set; }

        /// <summary>
        /// This model is not valid before date
        /// </summary>
        public DateTimeOffset NotValidBefore { get; set; }

        public RedirectModel() : this(string.Empty, string.Empty)
        {

        }

        public RedirectModel(string url, string name)
        {
            this.Url = url;
            this.Name = name;   
        }

        public RedirectModel(string url, string name, DateTimeOffset? validUntil, DateTimeOffset? notValidBefore) 
        { 
        
            Url = url;
            Name = name;
            ValidUntil = (DateTimeOffset)(validUntil is not null ? validUntil : DateTimeOffset.MaxValue);
            NotValidBefore = (DateTimeOffset)(notValidBefore is not null ? notValidBefore : DateTimeOffset.MinValue);
        }
    }
}

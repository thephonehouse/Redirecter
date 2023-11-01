using System.Runtime.InteropServices;

namespace Redirecter
{
    public class RedirectServiceFactory
    {
        private string path;

        public RedirectServiceFactory(string path)
        {
            this.path = path;
        }

        private void Parse()
        {

        }

        private IDictionary<int, RedirectModel> CreateDictonary(List<RedirectModel> list)
        {
            IDictionary<int, RedirectModel> dictionary = new Dictionary<int, RedirectModel>();

            list.ForEach(x => dictionary.Add(x.Id, x));

            return dictionary;
        }

        public IDictionary<int, RedirectModel> Create()
        {
            var list = new List<RedirectModel>();

            return CreateDictonary(list);
        }

        public IDictionary<int, RedirectModel> CreateExamples()
        {
            return CreateDictonary(new List<RedirectModel>
            {
                new RedirectModel(1, "https://google.com"),
                new RedirectModel(2, "https://example.org")
            });            
        }
    }
}

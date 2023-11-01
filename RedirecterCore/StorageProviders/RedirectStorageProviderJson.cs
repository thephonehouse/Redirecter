using Newtonsoft.Json;
using Redirecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace RedirecterCore.StorageProviders
{
    public class RedirectStorageProviderJson : IRedirectStorageProvider
    {
        public IEnumerable<RedirectModel> Models { get => models; }

        private RedirectModel[] models = Array.Empty<RedirectModel>();


        public RedirectStorageProviderJson(string path)
        {
            if (path.Split('.').Last() is not "json")
            {
                throw new ArgumentException("Provided file is not an json file!");
            }

            if (File.Exists(path) is false)
            {
                if (Directory.Exists(path))
                {
                    throw new ArgumentException("Provided directory does not exists!");
                }

                string json = JsonConvert.SerializeObject(new RedirectModel[] { new RedirectModel("https://example.org", "example") }, Formatting.Indented);
                File.WriteAllText(path, json);
            }

            using StreamReader fileReader = File.OpenText(path);
            JsonSerializer serializer = new();
            var deserializedModels = serializer.Deserialize<RedirectModel[]>(new JsonTextReader(fileReader));

            if (deserializedModels is null)
            {
                throw new ArgumentException("Could not deserialize the given file!");
            }

            models = deserializedModels;

        }

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

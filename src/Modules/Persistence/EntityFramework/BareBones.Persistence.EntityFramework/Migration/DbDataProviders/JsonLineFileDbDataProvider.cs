using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class JsonLineFileDbDataProvider<TData> : IDbDataProvider<IEnumerable<TData>>
    {
        private readonly string _filePath;

        public JsonLineFileDbDataProvider(string filePath)
        {
            // TODO: replace File with System.IO.Abstractions
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);
            _filePath = filePath;
        }

        public IEnumerable<TData> GetData()
        {
            var data = new List<TData>();
            using (var streamReader = new StreamReader(_filePath))
            using (var reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;

                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        data.Add(serializer.Deserialize<TData>(reader));
                    }
                }
            }

            return data;
        }
    }
}

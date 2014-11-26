using System;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using ElasticSearcher.ElasticUtils;
using ElasticSearcher.Models;
using ElasticSearcher.Searches;
using Nest;

namespace ElasticSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(node, "elastic-searcher");
            settings.SetDefaultTypeNameInferrer(t => t.Name.ToUpperInvariant());

            IElasticClient client = new ElasticClient(settings);
            IPersonSearch personSearcher = new PersonSearch(client);

            Utils.IndexItems(client);
            var value = Console.ReadLine();
            int id = Convert.ToInt32(value);

            if (id != 0)
            {
                personSearcher.GetItem<Person>(id);
            }
            else
            {
                Utils.ClearItems(client);
            }

            Console.ReadKey();
        }
    }
}

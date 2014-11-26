using System;
using System.Linq;
using ElasticSearcher.Models;
using Nest;
using Newtonsoft.Json;

namespace ElasticSearcher.Searches
{
    public class PersonSearch : IPersonSearch
    {
        private readonly IElasticClient _client;

        public PersonSearch(IElasticClient client)
        {
            _client = client;
        }

        public T GetItem<T>(int id) where T : class
        {
            QueryContainer query1 = new QueryDescriptor<Person>().Wildcard(p => p.Firstname, string.Format("martijn{0}", (id < 5 ? id.ToString() : "*")));
            QueryContainer query2 = new QueryDescriptor<Person>().Wildcard(p => p.Id, id.ToString());

            var searchRequest = new SearchRequest
            {
                From = 0,
                Size = 10,
                Query = query1 || query2
            };

            var searchResults = _client.Search<T>(searchRequest);
            Console.WriteLine(JsonConvert.SerializeObject(searchResults.Documents));

            return searchResults.Documents.FirstOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ElasticSearcher.Models;
using Nest;

namespace ElasticSearcher.ElasticUtils
{
    public static class Utils
    {
        public static void ClearItems(IElasticClient client)
        {
            client.DeleteIndex(i => i.Index<Person>());
            //client.DeleteByQuery<Person>(q => q.AllIndices().MatchAll());
        }

        public static void IndexItems(IElasticClient client)
        {
            for (var i = 0; i < 5; i++)
            {
                var j = i + 1;
                var person = new Person
                {
                    Id = j,
                    Firstname = string.Format("Martijn{0}", j),
                    Lastname = string.Format("{0}Laarman", j)
                };

                if (ValidateIfIdIsAlreadyUsedForIndex(j, client))
                {
                    client.Index(person);
                }
            }
        }

        private static bool ValidateIfIdIsAlreadyUsedForIndex(int id, IElasticClient client)
        {
            var idsList = new List<string> { id.ToString() };
            var result = client.Search<Person>(s => s
                   .AllTypes()
                   .Query(p => p.Ids(idsList)));
            return !result.Documents.Any();
        }
    }
}

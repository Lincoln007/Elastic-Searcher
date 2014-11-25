using System;
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

                client.Index(person);
            }
        }
    }
}

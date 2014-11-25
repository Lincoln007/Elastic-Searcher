using ElasticSearcher.Models;

namespace ElasticSearcher.Searches
{
    public interface IPersonSearch
    {
        Person GetItem(int id);
    }
}
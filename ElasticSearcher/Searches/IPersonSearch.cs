using ElasticSearcher.Models;

namespace ElasticSearcher.Searches
{
    public interface IPersonSearch
    {
        T GetItem<T>(int id) where T : class;
    }
}
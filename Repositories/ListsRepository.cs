using TodoApi.Models;
using TodoApi.Databases;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Repositories
{
    public class ListsRepository : IListsRepository
    {
        private readonly IMongoCollection<List> _collection;

        public ListsRepository(ITodoAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<List>(settings.ListsCollectionName);
        }

        async public Task<IEnumerable<List>> Get() =>
            await _collection.Find(list => true).ToListAsync();

        async public Task<List> Get(string id) =>
            await _collection.Find<List>(list => list.Id == id).FirstOrDefaultAsync();

        async public Task<List> Create(List list)
        {
            await _collection.InsertOneAsync(list);
            return list;
        }

        async public Task Update(string id, List listIn) =>
            await _collection.ReplaceOneAsync(list => list.Id == id, listIn);

        async public Task Remove(List listIn) =>
            await _collection.DeleteOneAsync(list => list.Id == listIn.Id);

        async public Task Remove(string id) =>
            await _collection.DeleteOneAsync(list => list.Id == id);
    }

    public interface IListsRepository
    {
        Task<List> Create(List list);
        Task<IEnumerable<List>> Get();
        Task<List> Get(string id);
        Task Remove(List listIn);
        Task Remove(string id);
        Task Update(string id, List listIn);
    }
}
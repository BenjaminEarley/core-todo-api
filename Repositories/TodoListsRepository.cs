using TodoApi.Models;
using TodoApi.Databases;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Repositories
{
    public class ListsRepository : IListsRepository
    {
        private readonly IMongoCollection<TodoList> _collection;

        public ListsRepository(ITodoAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TodoList>(settings.ListsCollectionName);
        }

        async public Task<IEnumerable<TodoList>> Get() =>
            await _collection.Find(todo => true).ToListAsync();

        async public Task<TodoList> Get(string id) =>
            await _collection.Find<TodoList>(todo => todo.Id == id).FirstOrDefaultAsync();

        async public Task<TodoList> Create(TodoList todo)
        {
            await _collection.InsertOneAsync(todo);
            return todo;
        }

        async public Task Update(string id, TodoList todoIn) =>
            await _collection.ReplaceOneAsync(todo => todo.Id == id, todoIn);

        async public Task Remove(TodoList todoIn) =>
            await _collection.DeleteOneAsync(todo => todo.Id == todoIn.Id);

        async public Task Remove(string id) =>
            await _collection.DeleteOneAsync(todo => todo.Id == id);
    }

    public interface IListsRepository
    {
        Task<TodoList> Create(TodoList todo);
        Task<IEnumerable<TodoList>> Get();
        Task<TodoList> Get(string id);
        Task Remove(TodoList todoIn);
        Task Remove(string id);
        Task Update(string id, TodoList todoIn);
    }
}
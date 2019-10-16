using TodoApi.Models;
using TodoApi.Databases;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly IMongoCollection<TodoItem> _todos;

        public TodoService(ITodoAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _todos = database.GetCollection<TodoItem>(settings.TodosCollectionName);
        }

        async public Task<IEnumerable<TodoItem>> Get() =>
            await _todos.Find(todo => true).ToListAsync();

        async public Task<TodoItem> Get(string id) =>
            await _todos.Find<TodoItem>(todo => todo.Id == id).FirstOrDefaultAsync();

        async public Task<TodoItem> Create(TodoItem todo)
        {
            await _todos.InsertOneAsync(todo);
            return todo;
        }

        async public Task Update(string id, TodoItem todoIn) =>
            await _todos.ReplaceOneAsync(todo => todo.Id == id, todoIn);

        async public Task Remove(TodoItem todoIn) =>
            await _todos.DeleteOneAsync(todo => todo.Id == todoIn.Id);

        async public Task Remove(string id) =>
            await _todos.DeleteOneAsync(todo => todo.Id == id);
    }

    public interface ITodoService
    {
        Task<TodoItem> Create(TodoItem todo);
        Task<IEnumerable<TodoItem>> Get();
        Task<TodoItem> Get(string id);
        Task Remove(TodoItem todoIn);
        Task Remove(string id);
        Task Update(string id, TodoItem todoIn);
    }
}
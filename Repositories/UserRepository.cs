using TodoApi.Models;
using TodoApi.Databases;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(ITodoAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        async public Task<IEnumerable<User>> Get() =>
            await _users.Find(user => true).ToListAsync();

        async public Task<User> Get(string id) =>
            await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();

        async public Task<User> Create(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        async public Task Update(string id, User userIn) =>
            await _users.ReplaceOneAsync(user => user.Id == id, userIn);

        async public Task Remove(User userIn) =>
            await _users.DeleteOneAsync(user => user.Id == userIn.Id);

        async public Task Remove(string id) =>
            await _users.DeleteOneAsync(user => user.Id == id);
    }

    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> Get();
        Task<User> Get(string id);
        Task Remove(User userIn);
        Task Remove(string id);
        Task Update(string id, User userIn);
    }
}
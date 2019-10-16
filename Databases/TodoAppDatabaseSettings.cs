namespace TodoApi.Databases
{
    public class TodoAppDatabaseSettings : ITodoAppDatabaseSettings
    {
        public string ListsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITodoAppDatabaseSettings
    {
        string ListsCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
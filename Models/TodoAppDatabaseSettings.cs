namespace TodoApi.Models
{
    public class TodoAppDatabaseSettings : ITodoAppDatabaseSettings
    {
        public string TodosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ITodoAppDatabaseSettings
    {
        string TodosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
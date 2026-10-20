namespace StockHub.Services.Entities
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
    }
}

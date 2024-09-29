public class ParametersMongoDB
{
    public required string Connection { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionNames { get; set; }
    public required string MaxVectorSearchResults { get; set; }
    public required string VectorIndexType { get; set; }
}

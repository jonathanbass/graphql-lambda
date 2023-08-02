using Amazon.DynamoDBv2.DataModel;

namespace GraphQLServerless.Models
{
    [DynamoDBTable("movies")]
    public class Movie
    {
        [DynamoDBHashKey("id")]
        public string? Id { get; set; }

        [DynamoDBProperty("title")]
        public string? Title { get; set; }

        [DynamoDBProperty("year")]
        public int Year { get; set; }

        [DynamoDBProperty("runtime")]
        public int Runtime { get; set; }

        [DynamoDBProperty("genre")]
        [DynamoDBIgnore]
        public IEnumerable<string> Genre { get; set; } = Enumerable.Empty<string>();

        [DynamoDBProperty("cast")]
        [DynamoDBIgnore]
        public IEnumerable<string> Cast { get; set; } = Enumerable.Empty<string>();
    }
}

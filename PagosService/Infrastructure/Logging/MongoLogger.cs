using MongoDB.Bson;
using MongoDB.Driver;
using PagosService.Application.Interfaces;
using PagosService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace PagosService.Infrastructure.Logging
{
    public class MongoLogger : IMongoLogger
    {
        private readonly IMongoCollection<BsonDocument> _logCollection;

        public MongoLogger(IOptions<MongoSettings> mongoOptions)
        {
            var settings = mongoOptions.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);

            if (!CollectionExists(database, settings.Collection))
                database.CreateCollection(settings.Collection);

            _logCollection = database.GetCollection<BsonDocument>(settings.Collection);
        }

        public async Task RegistrarLogAsync(string nivel, string mensaje, Dictionary<string, object>? contexto = null)
        {
            var logDoc = new BsonDocument
            {
                { "nivel", nivel },
                { "mensaje", mensaje },
                { "fecha", DateTime.UtcNow },
                { "contexto", contexto != null ? new BsonDocument(contexto) : new BsonDocument() }
            };

            await _logCollection.InsertOneAsync(logDoc);
        }

        private bool CollectionExists(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collections.Any();
        }
    }
}

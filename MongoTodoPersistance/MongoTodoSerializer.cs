using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo;
using Todo.Persistance;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace MongoTodoPersistance
{
    public class MongoTodoSerializer : ITodoItemSaver
    {
        private readonly string name;

        public MongoTodoSerializer(string name)
        {
            this.name = name;
        }

        public async Task<bool> Save(List<TodoItem> items)
        {
            var toSave = new { name, items };
            var serialized = JsonSerializer.Serialize(toSave);

            var client = new MongoClient($"mongodb+srv://{ApiKeys.MongoKey}@cluster0-p7ojg.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("TodoApp");
            var todos = database.GetCollection<BsonDocument>("Todos");
            var filter = Builders<BsonDocument>.Filter.Eq("name", name);

            var testing = await todos.ReplaceOneAsync(
                filter: filter,
                options: new ReplaceOptions { IsUpsert = true },
                replacement: new BsonDocument()
                {
                    {"name",name},
                    {"items",JsonSerializer.Serialize(items) }
                });



            return true;
        }
    }

    public class MongoTodoDeserializer : ITodoLoader
    {
        private readonly string name;

        public MongoTodoDeserializer(string name)
        {
            this.name = name;
        }

        public class TodoDataObject
        {
            public string Name { get; set; }
            public bool Completed { get; set; }
        }

        public class TodosDataObject
        {
            public string name { get; set; }
            public string items { get; set; }
        }

        public async IAsyncEnumerable<TodoItem> Items()
        {
            var client = new MongoClient($"mongodb+srv://{ApiKeys.MongoKey}@cluster0-p7ojg.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("TodoApp");
            var todos = database.GetCollection<BsonDocument>("Todos");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var items = todos.Find(new BsonDocument() {
                {"name",name }
            }).Project(projection).FirstOrDefault();

            var testing = BsonSerializer.Deserialize<TodosDataObject>(items);
            var listOfItems = JsonSerializer.Deserialize<IList<TodoDataObject>>(testing.items);
            foreach (var item in listOfItems)
            {
                yield return new TodoItem(item.Name) { Completed = item.Completed };
            }
        }
    }
}

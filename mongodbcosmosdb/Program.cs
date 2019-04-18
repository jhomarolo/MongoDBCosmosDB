using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBCosmosDB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = string.Empty;

             connectionString = @"mongodb://mvpconf19:5BDzAq8cp844GDuwirhd7OF75hPBSMNEYuNAriBEeLbW5WFjk75z1m14mcjqqUFYYk5xJaDkjMzdje5ChwpOpw==@mvpconf19.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
             MongoClientSettings settings = MongoClientSettings.FromUrl(
               new MongoUrl(connectionString)
             );
             settings.SslSettings =
               new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
             var client = new MongoClient(settings);

            //connectionString = "mongodb://localhost:27017";

           // var client = new MongoClient(connectionString);

            var database = client.GetDatabase("mvp");
            var collection = database.GetCollection<Person>("conf");

            collection.InsertOneAsync(new Person { Name = "Igor" }).Wait();
            collection.InsertOneAsync(new Person { Name = "Jhonathan" }).Wait();
            collection.InsertOneAsync(new Person { Name = "Leandro" }).Wait();

            Console.WriteLine("Inserted data into user collection");

            Console.WriteLine();
            Console.WriteLine("Getting by name Jhonathan");
            Console.WriteLine("-------------------------------");

            var list = collection.Find(x => x.Name == "Jhonathan")
                .ToListAsync().Result;

            foreach (var person in list)
            {
                Console.WriteLine(person.Name);
            }

            Console.ReadKey();
        }
    }
}

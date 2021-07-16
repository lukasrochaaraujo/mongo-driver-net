using MongoDB.Driver;
using MongoNetDriver.Model;
using System;
using System.Threading.Tasks;

namespace MongoNetDriver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MongoMapper.ApplyMapping();

            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("people");
            var collection = database.GetCollection<HomePeople>("home");

            //list all home people
            var majorityAgeList = await collection.Find(p => p.Age >= 18).ToListAsync();
            majorityAgeList.ForEach(people => Console.WriteLine(people));

            //create
            HomePeople newHomePeople = new("Fulano", 17);
            await collection.InsertOneAsync(newHomePeople);
            newHomePeople = await collection.Find(p => p.Name.Equals("Fulano")).FirstAsync();
            Console.WriteLine(newHomePeople);

            //filter builder
            var filterBuilder = Builders<HomePeople>.Filter;

            //filter to find
            var filterToFind = filterBuilder.Eq("name", newHomePeople.Name) & filterBuilder.Eq("age", newHomePeople.Age);

            //filter and update
            newHomePeople.Name = "Sicrano";
            var updatedInfo = Builders<HomePeople>
                                .Update.Set("name", newHomePeople.Name)
                                       .Set("age", newHomePeople.Age);

            await collection.FindOneAndUpdateAsync(filterToFind, updatedInfo);
            Console.WriteLine(newHomePeople);

            //filter and delete
            var findFilterToDelete = filterBuilder.Eq("name", newHomePeople.Name) & filterBuilder.Eq("age", newHomePeople.Age);         
            await collection.FindOneAndDeleteAsync(findFilterToDelete);
            Console.WriteLine($"{newHomePeople.Name} was deleted from collection.");
        }
    }
}

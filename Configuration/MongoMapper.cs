using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoNetDriver.Model;

namespace MongoNetDriver
{
    public static class MongoMapper
    {
        public static void ApplyMapping()
        {
            //HomePeople
            BsonClassMap.RegisterClassMap<HomePeople>(cm => 
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(f => f.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapMember(f => f.Age)
                    .SetElementName("age");
                cm.MapMember(f => f.Name)
                    .SetElementName("name");
            });
        }
    }
}
namespace MongoNetDriver.Model
{
    class HomePeople
    {
        public HomePeople(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
            => $"Id={Id},Name={Name},Age={Age}";
    }
}
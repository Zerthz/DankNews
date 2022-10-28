using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Functions.Cosmos
{
    public class AzureDb
    {
        // det här är default grejer i emulatorn
        // se https://learn.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=ssl-netstd21
        const string endpoint = "https://localhost:8081";
        const string key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        const string dbId = "SampleDB";
        const string containerId = "Persons";
        public async Task Connect()
        {
            try
            {
                using CosmosClient client = new CosmosClient(endpoint, key);

                // lite oklart vad vi hittar i en databaseresponse egentligen, typ id och lite annat
                var database = client.GetDatabase(dbId);
                var databaseResponse = await database.ReadAsync();
                Console.WriteLine("Read a database: {0}", databaseResponse.Resource.Id);

                // en query vad vi hämtar
                QueryDefinition query = new QueryDefinition("SELECT * FROM c");
                var container = client.GetContainer(dbId, containerId);

                Person person = new Person(Guid.NewGuid().ToString(), "Felix", 27);

                // Skapa ett nytt item, ID i classen måste antingen ha litet i eller ett JsonPropertyName som är id
                await container.CreateItemAsync<Person>(person);

                // hämta allt från containern och loopa igenom, säger att vi ska hämta det som person
                using (FeedIterator<Person> feedIterator = container.GetItemQueryIterator<Person>(query))
                {
                    while (feedIterator.HasMoreResults)
                    {
                        foreach (Person p in await feedIterator.ReadNextAsync())
                        {
                            System.Console.WriteLine("Name: {0}, Age: {1}", p.firstname, p.age);
                        }
                    }
                }

                // hämtar alla databaser
                Console.WriteLine("\n5. Reading all databases resources for an account");
                using (FeedIterator<DatabaseProperties> iterator = client.GetDatabaseQueryIterator<DatabaseProperties>(query))
                {
                    while (iterator.HasMoreResults)
                    {
                        foreach (DatabaseProperties db in await iterator.ReadNextAsync())
                        {
                            Console.WriteLine(db.Id);
                        }
                    }
                }


            }
            catch (CosmosException cre)
            {
                Console.WriteLine(cre.ToString());
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }

        }
    }

    public record Person(string id, string firstname, int age);
}
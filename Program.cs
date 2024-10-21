using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
class Program
{
    static void Main(string[] args)
    {
        // Define the endpoint and key for the Cosmos DB account
        string accountEndpoint = "https://yourdomain-cosmosdb-2.documents.azure.com:443/";
        string authKeyOrResourceToken = "5FdBpEG58cCjxTMW9y5gxGupbjZMPIpBd86SX86gpAAJ591ANq9zE5SUfQv5NZG50MFEHB78j3R2ACDbmONgww==";

        // Create an instance of the CosmosClient
        CosmosClient cosmosClient = new CosmosClient(accountEndpoint, authKeyOrResourceToken);

        string databaseId = "ToDoList";
        string containerId = "Items";

        // Create the database if it does not exist
        Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId).Result;

        // Create the container if it does not exist with the correct partition key path
        Container container = database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey").Result;

        // Insert 10 items into the container
        for (int i = 2; i <= 10; i++)
        {
            var item = new { id = i.ToString(), partitionKey = "partitionKeyValue", name = $"Sample LSantos2000 Item {i}" };
            container.CreateItemAsync(item, new PartitionKey(item.partitionKey)).Wait();
            Console.WriteLine($"Item {i} inserted successfully.");
        }

        Console.WriteLine("CosmosClient instance created successfully.");
    }
}
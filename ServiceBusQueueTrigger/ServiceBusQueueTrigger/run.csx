using System;
using System.Threading;
using System.Threading.Tasks;

private static int waitInMS = 5000;

public static void Run(string myQueueItem, ILogger log)
{
    Thread.Sleep(waitInMS); // Simulate complex business logic
    log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem} in {waitInMS} ms.");
}

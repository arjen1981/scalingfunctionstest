namespace CoreSenderApp
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    class Program
    {

        #region Connection
        const string ServiceBusConnectionString = "";
        const string QueueName = "scalingfunctionstest-queue";
        static IQueueClient queueClient;
        #endregion

        public static async Task Main(string[] args)
        {
            int numberOfMessages = 0;

            Console.WriteLine("How many messages woul you like to put in the queue?");
            if (!int.TryParse(Console.ReadLine(), out numberOfMessages))
                return;

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            // Send messages.
            SendMessages(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static void SendMessages(int numberOfMessagesToSend)
        {
            var tasks = new Task[numberOfMessagesToSend];

            Parallel.For(0, numberOfMessagesToSend, i =>
                {
                    // Create a new message to send to the queue
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue
                    tasks[i] = queueClient.SendAsync(message);
                });

            _ = Task.WhenAll(tasks);
        }
    }
}
// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using JobTest;

Console.WriteLine("Hello, World! This should be the latest version.");
try
{
    var sourceQueueName = Environment.GetEnvironmentVariable("QueueName");
    var serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnection");
    var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
    var receiver = serviceBusClient.CreateReceiver(sourceQueueName);
    var receivedMessage = await receiver.ReceiveMessageAsync();

    if (receivedMessage != null)
    {
        Console.WriteLine($"Received message = {receivedMessage}");
        var payloadString = Encoding.UTF8.GetString(receivedMessage.Body);
        var payload = JsonConvert.DeserializeObject<KpiRecalculationMessage>(payloadString);
        Console.WriteLine($"PayloadString= {payloadString}");
        if (payload != null)
        {

            Console.WriteLine($"Message with Id: {payload.Id}");
            Console.WriteLine($"Message with orgId: {payload.OrganizationId}");
            Console.WriteLine($"Message with taskId: {payload.TaskId}");
            await receiver.CompleteMessageAsync(receivedMessage);
            Console.WriteLine($"All good, process complete for orgId:{payload.OrganizationId}");
        }
    }
}
catch (Exception e)
{
    Console.WriteLine("Exception :(" + e.Message);
    throw;
}

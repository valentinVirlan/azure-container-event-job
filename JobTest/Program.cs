// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using JobTest;

Console.WriteLine("Hello, World! Where are you?");
try
{
    // var sourceQueueName = Environment.GetEnvironmentVariable("AZURE_STORAGE_QUEUE_NAME");
    //var serviceBusConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
    var sourceQueueName = "landry-queue";
    var serviceBusConnectionString = "Endpoint=sb://landry-aca.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7m3yp56/fqMn3ieQtzdZbkZsbTXokwliI+ASbMF/1GI=";
    var serviceBusClient = new ServiceBusClient(serviceBusConnectionString);
    var receiver = serviceBusClient.CreateReceiver(sourceQueueName);
    var receivedMessage = await receiver.ReceiveMessageAsync();

    if (receivedMessage != null)
    {
        var payloadString = Encoding.UTF8.GetString(receivedMessage.Body);
        var payload = JsonConvert.DeserializeObject<KpiRecalculationMessage>(payloadString);
        if (payload != null)
        {

            Console.WriteLine($"Message with orgId: {payload.OrganizationId}");
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

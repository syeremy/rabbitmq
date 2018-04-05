using System;
using System.Text;
using RabbitMQ.Client;

namespace Syeremy.RabbitmqSender
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Console.WriteLine("Sending Messages with RabbitMQ");
            Console.WriteLine();
            Console.WriteLine();

            var messageCount = 0;
            var sender = new Sender();

            Console.WriteLine("Press Enter to send a random message");

            while (true)
            {
                var key = Console.ReadKey();
                
                if(key.Key == ConsoleKey.Q) break;

                if (key.Key == ConsoleKey.Enter)
                {
                    var message = $"Message count {messageCount++}";
                    Console.WriteLine($"Sending : {message}");

                    sender.Send(message);
                }

            }
            
        }
        
        
        
//        static void Publish()
//        {
//            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
//            {
//                HostName = Hostname,
//                UserName =  UserName,
//                Password =  Password
//            };
//
//            try
//            {
//                using (var connection = connectionFactory.CreateConnection())
//                {
//                    using (var model = connection.CreateModel())
//                    {
//
//                        var properties = model.CreateBasicProperties();
//                        properties.SetPersistent(false);
//
//                        byte[] buffer = Encoding.Default.GetBytes("Here, como tali bu");
//
//                        model.BasicPublish(ExchangeName, RouteKey, true, properties, buffer);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }
    }
}

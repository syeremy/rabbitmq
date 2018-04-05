using System;
using RabbitMQ.Client;

namespace Syeremy.RabbitmqReceiver
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ queue Processor");
            Console.WriteLine();
            
            var consumer = new Consumer{Enabled = true};
            consumer.Start();
            Console.ReadLine();


        }


        static void Create()
        {
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
//                        model.QueueDeclare("queue-dotnet-core", true, false, false, null);
//                        
//                        model.ExchangeDeclare("exchange-dotnet-core", ExchangeType.Topic, true);
//                        
//                        model.QueueBind("queue-dotnet-core", "exchange-dotnet-core", "route-elcavernas");
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
        }
    }
}

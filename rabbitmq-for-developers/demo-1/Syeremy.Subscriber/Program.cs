using System;
using System.Threading.Tasks;

namespace Syeremy.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ queue processor");
            Console.WriteLine();
            Console.WriteLine();

            string queue1 = "my-queue", queue2 = "my-queue-2";

            var subscriber1 = new Subscriber(queue1){Enabled = true};
            Console.WriteLine($"Subscriber 1 with queue '{queue1}' up");
            //subscriber1.Start();
            Task.Run(new Action(subscriber1.Start));

            
            var subscriber2 = new Subscriber(queue2){Enabled = true};
            Console.WriteLine($"Subscriber 1 with queue '{queue2}' up");
            //subscriber2.Start();
            Task.Run(new Action(subscriber2.Start));

            
            Console.ReadLine();
        }
    }
}
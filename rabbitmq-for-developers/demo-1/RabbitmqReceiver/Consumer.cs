using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Syeremy.RabbitmqReceiver
{
    public class Consumer : IDisposable
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "queue-dotnet-core";
        //private const string ExchangeName = "exchange-dotnet-core";
        private const string ExchangeName = "";
        //private const string RouteKey = "route-elcavernas";
        private const bool IsDurable = true;
        
        
        //public delegate void OnReceiveMessage(string message);
        
        public bool Enabled { get; set; }
        
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;


        public Consumer()
        {
            DisplaySettings();
            Init();
        }


        public void Start()
        {
            var consumer = new QueueingBasicConsumer(_model);
            _model.BasicConsume(QueueName, false, consumer);

            while (Enabled)
            {
                var deliveryArgs = (BasicDeliverEventArgs) consumer.Queue.Dequeue();

                var message = Encoding.Default.GetString(deliveryArgs.Body);

                Console.WriteLine($"Message Received : {message}");
                _model.BasicAck(deliveryArgs.DeliveryTag, false);
            }
        }


        private void Init()
        {
            _connectionFactory = new RabbitMQ.Client.ConnectionFactory
            {
                HostName = HostName,
                UserName =  UserName,
                Password =  Password
            };

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
        }
        
        
        
        
        
        private void DisplaySettings()
        {
            Console.WriteLine($"Host: {HostName}");
            Console.WriteLine($"Username: {UserName}");
            Console.WriteLine($"Password: {Password}");
            Console.WriteLine($"QueueName: {QueueName}");
            Console.WriteLine($"ExchangeName: {ExchangeName}");
            Console.WriteLine($"Is Durable: {IsDurable}");
        }
        
        
        public void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();

            _connectionFactory = null;
            GC.SuppressFinalize(this);
        }
    }
}
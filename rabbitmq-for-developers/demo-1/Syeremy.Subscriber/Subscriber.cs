using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Syeremy.Subscriber
{
    public class Subscriber
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        //private const string ExchangeName = "exchange-dotnet-core";
        private const string ExchangeName = "my-exchange";
        //private const string RouteKey = "route-elcavernas";
        private const bool IsDurable = true;
        
        private readonly string _queueName;
        
        private delegate void ConsumeDelegate();


        
                
        public bool Enabled { get; set; }
        
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;
        private Subscription _subscription;


        public Subscriber(string queueName)
        {
            _queueName = queueName;

            DisplaySettings();
            Init();
        }



        public void Start()
        {
            _subscription = new Subscription(_model, _queueName, false);

            var consumer = new ConsumeDelegate(Poll);

            Console.WriteLine($"Ready to process messages to queue : {_queueName}!!!");
            consumer.Invoke();
        }

        private void Poll()
        {
            while (Enabled)
            {
                var deliveryArgs = _subscription.Next();

                var message = Encoding.Default.GetString(deliveryArgs.Body);

                Console.WriteLine($"[{_queueName}] Message {message} received!");
                
                _subscription.Ack(deliveryArgs);
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
            Console.WriteLine($"QueueName: {_queueName}");
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
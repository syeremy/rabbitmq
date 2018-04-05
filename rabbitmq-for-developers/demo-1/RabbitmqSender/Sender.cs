using System;
using System.Text;
using RabbitMQ.Client;

namespace Syeremy.RabbitmqSender
{
    public class Sender: IDisposable
    {
        private const string HostName = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string QueueName = "queue-dotnet-core";
        //private const string ExchangeName = "exchange-dotnet-core";
        private const string ExchangeName = "";
        //private const string RouteKey = "route-elcavernas";
        private const bool IsDurable = true;
        
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;


        public Sender()
        {
            DisplaySettings();
            Init();
        }
        
        public void Send(string message)
        {
            var properties = _model.CreateBasicProperties();
            properties.SetPersistent(IsDurable);

            var buffer = Encoding.Default.GetBytes(message);
            
            //For one-way
            // ExchangeName = "", QueueName = "queue-dotnet-core"
            //_model.BasicPublish(ExchangeName, QueueName, properties, buffer);
            
            //for publish-subscriber, no queue.
            _model.BasicPublish(ExchangeName, "", properties, buffer);
            
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
            _connection?.Close();

            if (_model != null && _model.IsOpen)
                _model.Abort();

            _connectionFactory = null;

            GC.SuppressFinalize(this);
        }
    }
}
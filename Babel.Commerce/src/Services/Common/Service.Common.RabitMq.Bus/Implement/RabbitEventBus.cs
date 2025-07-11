using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Common.RabitMq.Bus.BusRabbit;
using Service.Common.RabitMq.Bus.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.RabitMq.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {
        private readonly Dictionary<string, List<Type>> _manejadores;
        private readonly List<Type> _eventoTipos;
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitEventBus(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _manejadores=new Dictionary<string, List<Type>>();
            _eventoTipos=new List<Type>();
        }


        public async void Publish<T>(T evento) where T : Evento
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" ,UserName="Administrador",Password= "Babel2025" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            connection.ConnectionShutdownAsync += Connection_ConnectionShutdownAsync;
            var eventName = evento.GetType().Name;

            await channel.QueueDeclareAsync(eventName, false, false, false, null);

            var message = JsonConvert.SerializeObject(evento);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: eventName, body);
            
        }

        private Task Connection_ConnectionShutdownAsync(object sender, ShutdownEventArgs @event)
        {
            Console.WriteLine("ConnectionShutdownAsync");
            return Task.CompletedTask;
        }

        public  void Subscribe<T, TH>()
            where T : Evento
            where TH : IEventoManejador<T>
        {
            var eventoNombre = typeof(T).Name;
            var manejadorEventoTipo = typeof(TH);

            if (!_eventoTipos.Contains(typeof(T)))
            {
                _eventoTipos.Add(typeof(T));
            }

            if (!_manejadores.ContainsKey(eventoNombre))
            {
                _manejadores.Add(eventoNombre, new List<Type>());
            }

            if (_manejadores[eventoNombre].Any(x => x.GetType() == manejadorEventoTipo))
            {
                throw new ArgumentException($"El manejador {manejadorEventoTipo.Name} fue registrado anteriormente por {eventoNombre}");
            }

            _manejadores[eventoNombre].Add(manejadorEventoTipo);
            
            StartBasicConsume<T>();
        }

        private async void StartBasicConsume<T>() where T : Evento
        {
            var factory = new ConnectionFactory() 
            { HostName = "rabbitmq", 
              UserName = "Administrador", 
               Password = "Babel2025", 
                ConsumerDispatchConcurrency = 1 };

            try
            {
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                var eventoNombre = typeof(T).Name;

                await channel.QueueDeclareAsync(eventoNombre, false, false, false, null);
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += Consumer_Delegate;
                await channel.BasicConsumeAsync(eventoNombre, true, consumer);
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                throw ex ;
            }

        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var nombreEvento = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(nombreEvento, message).ConfigureAwait(false);
                //await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(e.DeliveryTag,multiple:false);
            }
            catch (Exception ex)
            {

            }
        }

        private async Task ProcessEvent(string nombreEvento, string message)
        {
            if (_manejadores.ContainsKey(nombreEvento))
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var subscriptions = _manejadores[nombreEvento];
                    foreach (var sb in subscriptions)
                    {
                        var manejador = scope.ServiceProvider.GetService(sb); //Activator.CreateInstance(sb);
                        if (manejador == null) continue;
                        var tipoEvento = _eventoTipos.SingleOrDefault(x => x.Name == nombreEvento);
                        var eventoDS = JsonConvert.DeserializeObject(message, tipoEvento);
                        var concretoTipo = typeof(IEventoManejador<>).MakeGenericType(tipoEvento);

                        await (Task)concretoTipo.GetMethod("Handle").Invoke(manejador, new object[] { eventoDS });
                    }
                }
            }
        }

        //public void StartConsume<T>() where T : Evento
        //{
        //    StartBasicConsume<T>();
        //}
    }
}

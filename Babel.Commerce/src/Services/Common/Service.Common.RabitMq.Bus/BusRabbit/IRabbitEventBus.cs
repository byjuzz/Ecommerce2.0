using Service.Common.RabitMq.Bus.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.RabitMq.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {

        void Publish<T>(T @evento) where T : Evento;
        //void StartConsume<T>() where T : Evento;
        void Subscribe<T, TH>() where T : Evento
                                where TH : IEventoManejador<T>;
    }
}

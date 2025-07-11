using Service.Common.RabitMq.Bus.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.RabitMq.Bus.BusRabbit
{
    public interface IEventoManejador<in TEvent>:IEventoManejador where TEvent : Evento
    {
        Task Handle(TEvent @event);
    }
    public interface IEventoManejador { }
}

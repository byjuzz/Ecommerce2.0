using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Client.Service.EventHandlers.Commands
{
    public class ClientCreateCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}

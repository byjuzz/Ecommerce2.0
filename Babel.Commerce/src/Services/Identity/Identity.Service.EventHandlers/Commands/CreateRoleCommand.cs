﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.EventHandlers.Commands
{
    public class CreateRoleCommand : IRequest<IdentityResult>
    {
        public string RoleName { get; set; }
    }

}

using MediaRequest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Commands
{
    public class AddRequestCommand : IRequest
    {
        public UserRequest Request { get; set; }
    }
}

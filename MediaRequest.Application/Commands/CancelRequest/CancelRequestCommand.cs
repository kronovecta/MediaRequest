using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Commands.CancelRequest
{
    public class CancelRequestCommand : IRequest
    {
        public int RequestID { get; set; }
    }
}

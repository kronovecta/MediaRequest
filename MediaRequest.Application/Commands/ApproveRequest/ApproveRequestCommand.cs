﻿using MediaRequest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Commands.ApproveRequest
{
    public class ApproveRequestCommand : IRequest<bool>
    {
        public string ApiKey { get; set; }
        public MovieRequestObject RequestObject { get; set; }
    }
}

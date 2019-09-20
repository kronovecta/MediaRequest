using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain
{
    public class UserRequest
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public string MovieId { get; set; }
        public bool Status { get; set; }
    }
}

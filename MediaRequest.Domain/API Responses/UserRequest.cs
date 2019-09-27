using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain
{
    public class UserRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string MovieId { get; set; }
        public bool Status { get; set; }
    }
}

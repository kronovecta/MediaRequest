using MediaRequest.Domain.API_Responses;
using MediaRequest.Domain.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class ActorViewModel
    {
        public Actor Actor { get; set; }
        public OtherWorks Movies { get; set; }
    }
}

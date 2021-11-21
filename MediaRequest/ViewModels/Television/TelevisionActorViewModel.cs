using MediaRequest.Domain.API_Responses.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class TelevisionActorViewModel
    {
        public Person Actor { get; set; }
        public IEnumerable<PersonCredit> Credits { get; set; }
    }
}

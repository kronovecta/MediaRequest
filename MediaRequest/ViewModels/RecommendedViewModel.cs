using MediaRequest.Domain.API_Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class RecommendedViewModel
    {
        public int? Page { get; set; }
        public Recommendations Recommendations { get; set; }
    }
}

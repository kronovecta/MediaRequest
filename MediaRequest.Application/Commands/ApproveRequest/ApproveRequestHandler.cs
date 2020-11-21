using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Commands.ApproveRequest
{
    public class ApproveRequestHandler : IRequestHandler<ApproveRequestCommand, bool>
    {
        private readonly ApiKeys _apikeys;
        private readonly ServicePath _path;

        private static List<HttpStatusCode> validStatusCodes = new List<HttpStatusCode>() { HttpStatusCode.OK, HttpStatusCode.Accepted, HttpStatusCode.Created };

        public ApproveRequestHandler(IOptions<ServicePath> path, IOptions<ApiKeys> apikeys)
        {
            _apikeys = apikeys.Value;
            _path = path.Value;
        }

        public async Task<bool> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request.RequestObject), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{_path.Radarr}api/movie?apikey={_apikeys.Radarr}", content);

                if(ValidateStatusCode(response.StatusCode))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValidateStatusCode(HttpStatusCode status)
        {
            return validStatusCodes.Contains(status);
        }
    }
}

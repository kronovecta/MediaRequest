using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Commands.ApproveRequest
{
    public class ApproveRequestHandler : IRequestHandler<ApproveRequestCommand, bool>
    {
        public async Task<bool> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(request.RequestObject), Encoding.UTF8, "application/json");

                var parameters = $"&title={request.RequestObject.title}&qualityProfileId={request.RequestObject.qualityProfileId}&titleSlug={request.RequestObject.titleSlug}&tmdbId={request.RequestObject.tmdbId}&year={request.RequestObject.year}&path={request.RequestObject.path}&images={request.RequestObject.images}";

                var response = await client.PostAsync($"https://tiger.seedhost.eu/robert/radarr/api/movie?apikey={request.ApiKey}", content);

                if (response.StatusCode.ToString().StartsWith("C"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

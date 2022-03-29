using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Domain.Configuration
{
    [FilterAlias("ImageProcessor")]
    public class ImageProcessorFilter : IFeatureFilter
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.Get<ImageProcessor>();

            return Task.FromResult(settings.Active && !string.IsNullOrWhiteSpace(settings.Url));
        }
    }

    public class ImageProcessor
    {
        public bool Active { get; set; }
        public string Url { get; set; }
    }
}

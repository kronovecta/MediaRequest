using MediaRequest.Domain.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Business.Services
{
    public interface IImageProcessorService
    {
    }

    public class ImageProcessorService : IImageProcessorService
    {
        private readonly ImageProcessor _imageProcessor;

        public ImageProcessorService(IOptions<ImageProcessor> imageProcessor)
        {
            _imageProcessor = imageProcessor.Value;
        }

        public string GetImageUrl(string input)
        {

            if (_imageProcessor.Active && !string.IsNullOrWhiteSpace(_imageProcessor.Url))
            {
                return _imageProcessor.Url + input;
            } else
            {
                return input;
            }
        }
    }
}

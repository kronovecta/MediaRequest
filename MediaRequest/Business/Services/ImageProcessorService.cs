using MediaRequest.Domain.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Business.Services
{
    public interface IImageProcessorService
    {
        Task<string> GetImageUrl(string media, int width, int height);
    }

    public class ImageProcessorService : IImageProcessorService
    {
        private readonly IFeatureManager _featureManager;
        private readonly ImageProcessor _imageProcessor;

        public ImageProcessorService(IFeatureManager featureManager, IOptions<ImageProcessor> imageProcessor)
        {
            _featureManager = featureManager;
            _imageProcessor = imageProcessor.Value;
        }

        public async Task<string> GetImageUrl(string media, int width, int height)
        {
            if(await _featureManager.IsEnabledAsync("ImageProcessor"))
            {
                return $"{_imageProcessor.Url}{width}x{height}/{media}";
            }
            else
            {
                return media;
            }
        }
    }
}

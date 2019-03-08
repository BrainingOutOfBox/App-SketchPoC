using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using SketchPoC.BLServices;
using SketchPoC.Dal;
using SketchPoC.PrismEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SketchPoC.ViewModels
{
	public class SketchDownloadPageViewModel : ViewModelBase
	{
        private readonly IEventAggregator _eventAggregator;
        private readonly IDalService _dalService;
        private readonly ISaveService _saveService;
        private string fileId;

        public SketchDownloadPageViewModel(
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IDalService dalService,
            ISaveService saveService) : base(navigationService)
        {
            _eventAggregator = eventAggregator;
            _dalService = dalService;
            _saveService = saveService;
            HasDownloadFinished = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var streamTask = DownloadImage().ContinueWith((stream) =>
            {
                DownloadedImageSource = ImageSource.FromStream(()=>stream.Result);
                HasDownloadFinished = true;
            });
            

        }

        private async Task<Stream> DownloadImage()
        {
            var streamTask = _dalService.Download(_saveService.FileId);
            Console.WriteLine("Downloading image..");
            return await streamTask;
        }

        private bool _hasDownloadFinished;

        public bool HasDownloadFinished
        {
            get => _hasDownloadFinished;
            set => SetProperty(ref _hasDownloadFinished, value);
        }
        private ImageSource _downloadedImageSource;
        public ImageSource DownloadedImageSource
        {
            get => _downloadedImageSource;
            set => SetProperty(ref _downloadedImageSource, value);
        }
    }
}

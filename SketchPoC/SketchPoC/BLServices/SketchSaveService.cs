using Prism.Events;
using SketchPoC.Dal;
using SketchPoC.PrismEvents;
using System;
using System.IO;

namespace SketchPoC.BLServices
{
    public class SketchSaveService : ISaveService
    {
        private readonly IDalService _dalService;
        private readonly IEventAggregator _eventAggregator;

        public SketchSaveService(IDalService dalService, IEventAggregator eventAggregator)
        {
            _dalService = dalService;
            _eventAggregator = eventAggregator;
        }

        public string FileId { get; set; }

        public bool Save(Stream stream)
        {

            var response = _dalService.Save(stream);
            if (response.Length <= "file id = ".Length) return false;

            var fileId = response.Substring("file id = ".Length);
            FileId = fileId;
            return true;
        }
    }
}

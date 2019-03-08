using SketchPoC.Dal;
using System;
using System.IO;

namespace SketchPoC.BLServices
{
    public class SketchSaveService : ISaveService
    {
        private readonly IDalService _dalService;

        public SketchSaveService(IDalService dalService)
        {
            _dalService = dalService;
        }

        public bool Save(Stream stream)
        {

            _dalService.Save(stream);

            return true;
        }
    }
}

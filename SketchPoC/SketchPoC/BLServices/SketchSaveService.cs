using System;

namespace SketchPoC.BLServices
{
    public class SketchSaveService : ISaveService
    {
        public bool Save()
        {
            Console.WriteLine("SAVED FILE");
            return true;
        }
    }
}

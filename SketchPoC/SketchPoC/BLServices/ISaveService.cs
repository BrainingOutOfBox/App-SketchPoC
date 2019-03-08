using System.IO;

namespace SketchPoC.BLServices
{
    public interface ISaveService
    {
        bool Save(Stream stream);
    }
}
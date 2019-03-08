using System.IO;

namespace SketchPoC.BLServices
{
    public interface ISaveService
    {
        bool Save(Stream stream);
        string FileId { get; set; }
    }
}
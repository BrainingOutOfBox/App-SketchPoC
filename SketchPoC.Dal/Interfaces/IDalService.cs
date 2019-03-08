using System.IO;

namespace SketchPoC.Dal
{
    public interface IDalService
    {
        void Save(Stream stream);
    }
}

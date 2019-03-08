using System.IO;
using System.Threading.Tasks;

namespace SketchPoC.Dal
{
    public interface IDalService
    {
        string Save(Stream stream);
        Task<Stream> Download(string fileId);
    }
}

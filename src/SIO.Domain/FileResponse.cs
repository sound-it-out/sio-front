using System.IO;

namespace SIO.Domain
{
    public record FileResponse(Stream Stream, string FileName);
}

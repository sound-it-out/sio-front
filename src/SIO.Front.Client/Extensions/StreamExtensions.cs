using System.IO;
using System.Threading.Tasks;

namespace SIO.Front.Client.Extensions
{
    public static class StreamExtensions
    {
        public async static Task<byte[]> ToArrayAsync(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}

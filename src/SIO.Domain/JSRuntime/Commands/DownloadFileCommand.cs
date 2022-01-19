using System.IO;
using SIO.Infrastructure;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.JSRuntime.Commands
{
    public sealed class DownloadFileCommand : Command
    {
        public Stream Stream { get; set; }
        public DownloadFileCommand(string subject, Stream stream) : base(subject, null, 0, Actor.Unknown)
        {
            Stream = stream;
        }
    }
}

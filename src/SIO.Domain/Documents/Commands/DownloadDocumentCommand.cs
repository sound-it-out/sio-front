using SIO.Infrastructure;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.Commands
{
    public class DownloadDocumentCommand : Command
    {
        public DownloadDocumentCommand(string subject) : base(subject, null, 0, Actor.Unknown)
        {
        }
    }
}

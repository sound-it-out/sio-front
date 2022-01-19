using SIO.Infrastructure;
using SIO.Infrastructure.Commands;

namespace SIO.Domain.Documents.Commands
{
    public class LoadUserDocumentsStateCommand : Command
    {
        public LoadUserDocumentsStateCommand() : base("", null, 0, Actor.Unknown)
        {
        }
    }
}

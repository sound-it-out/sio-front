using System.Collections.Generic;
using SIO.Domain.Documents.Api.Responses;
using SIO.Domain.States;

namespace SIO.Domain.Documents.States
{
    internal sealed class UserDocumentsState : State<IEnumerable<UserDocumentResponse>>, IUserDocumentsState
    {
    }
}

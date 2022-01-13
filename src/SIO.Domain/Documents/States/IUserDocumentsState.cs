using System.Collections.Generic;
using SIO.Domain.Documents.Api.Responses;
using SIO.Domain.States;

namespace SIO.Domain.Documents.States
{
    public interface IUserDocumentsState :IState<IEnumerable<UserDocumentResponse>>
    {
    }
}

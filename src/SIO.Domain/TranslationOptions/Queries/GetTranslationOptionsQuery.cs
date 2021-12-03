using SIO.Infrastructure;
using SIO.Infrastructure.Queries;

namespace SIO.Domain.TranslationOptions.Queries
{
    public class GetTranslationOptionsQuery : Query<GetTranslationOptionsQueryResult>
    {
        public GetTranslationOptionsQuery() : base(null, Actor.Unknown)
        {
        }
    }
}

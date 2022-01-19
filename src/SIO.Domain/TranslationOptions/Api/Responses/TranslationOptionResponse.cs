namespace SIO.Domain.TranslationOptions.Api.Responses
{
    public class TranslationOptionResponse
    {
        public readonly string Id;
        public readonly string Subject;
        public readonly TranslationType TranslationType;

        public TranslationOptionResponse(string id, string subject, TranslationType translationType)
        {
            Id = id;
            Subject = subject;
            TranslationType = translationType;
        }
    }
}

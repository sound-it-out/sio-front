using SIO.Domain.TranslationOptions.Api.Responses;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class TranslationOptionViewModel : TranslationOptionResponse
    {
        public TranslationOptionViewModel(TranslationOptionResponse option) : base(option.Id, option.Subject, option.TranslationType)
        {
        }

        public bool Selected { get; set; }
        public string TranslationTypeName => GetTranslationTypeName(TranslationType);

        public static string GetTranslationTypeName(TranslationType? translationType) => translationType switch
        {
            SIO.Domain.TranslationOptions.Api.Responses.TranslationType.Google => "Google",
            SIO.Domain.TranslationOptions.Api.Responses.TranslationType.AWS => "AWS",
            _ => ""
        };
    }
}

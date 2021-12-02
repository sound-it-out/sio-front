using System;
using SIO.Api.Client.Model;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class TranslationOptionViewModel
    {
        public readonly Guid Id;
        public readonly string Subject;
        public readonly TranslationType? TranslationType;

        public TranslationOptionViewModel(TranslationOption option)
        {
            Id = Guid.NewGuid();
            Subject = option.Subject;
            TranslationType = option.TranslationType;
        }

        public bool Selected { get; set; }
        public string TranslationTypeName => GetTranslationTypeName(TranslationType);

        public static string GetTranslationTypeName(TranslationType? translationType) => translationType switch
        {
            SIO.Api.Client.Model.TranslationType.NUMBER_0 => "Google",
            SIO.Api.Client.Model.TranslationType.NUMBER_1 => "AWS",
            _ => ""
        };
    }
}

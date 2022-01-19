using System.Collections.Generic;
using System.Linq;
using SIO.Domain.TranslationOptions.Api.Responses;
using SIO.Front.Client.Components;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class SelectTranslationModel : WizardStepViewModel
    {
        public SelectTranslationModel() : base("Select translation", "fa-microphone") { }

        public IEnumerable<TranslationOptionViewModel> TranslationOptions { get; set; }
        public TranslationOptionViewModel[] AvaliableTranslationOptions { get; set; }

        public string SearchKeyword { get; set; }

        public TranslationOptionResponse SelectedTranslation => AvaliableTranslationOptions.FirstOrDefault(o => o.Selected);
        public bool HasSelection => AvaliableTranslationOptions.Any(o => o.Selected);
    }
}

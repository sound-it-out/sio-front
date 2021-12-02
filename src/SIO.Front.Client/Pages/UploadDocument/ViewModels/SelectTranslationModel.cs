using System.Collections.Generic;
using System.Linq;
using SIO.Api.Client.Model;
using SIO.Front.Client.Components;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class SelectTranslationModel : WizardStepViewModel
    {
        public SelectTranslationModel() : base("Select translation", "fa-microphone") { }

        public IEnumerable<TranslationOptionViewModel> TranslationOptions { get; set; }
        public TranslationOptionViewModel[] AvaliableTranslationOptions { get; set; }

        public string SearchKeyword { get; set; }
        public TranslationType? SelectedSearchTranslationType { get; set; }

        public TranslationType? SelectedTranslationType => AvaliableTranslationOptions.FirstOrDefault(o => o.Selected)?.TranslationType;
    }
}

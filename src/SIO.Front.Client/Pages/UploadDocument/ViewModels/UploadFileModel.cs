using SIO.Api.Client.Client;
using SIO.Front.Client.Components;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class UploadFileModel : WizardStepViewModel
    {
        public UploadFileModel() : base("Upload file", "fa-file-upload") { }

        public FileParameter File { get; set; }
    }
}

using Microsoft.AspNetCore.Components.Forms;
using SIO.Front.Client.Components;

namespace SIO.Front.Client.Pages.UploadDocument.ViewModels
{
    public class UploadFileModel : WizardStepViewModel
    {
        public UploadFileModel() : base("Upload file", "fa-file-upload") { }

        public byte[] File { get; set; }
        public string FileName { get; set; }
        public string FileContentType { get; set; }
    }
}

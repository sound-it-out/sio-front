namespace SIO.Front.Client.Components
{
    public class WizardStepViewModel : IWizardStepViewModel
    {
        public WizardStepViewModel(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }

        public string Name { get; }
        public string Icon { get; }
        public bool IsValid { get; set; }
    }
}

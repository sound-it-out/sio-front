namespace SIO.Front.Client.Components
{
    public interface IWizardStepViewModel
    {
        string Name { get; }
        string Icon { get; }
        bool IsValid { get; }
    }
}

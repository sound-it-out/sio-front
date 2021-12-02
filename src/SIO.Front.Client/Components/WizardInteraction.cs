using System;
using System.Threading.Tasks;

namespace SIO.Front.Client.Components
{
    internal sealed class WizardInteraction : IWizardInteraction
    {
        private event Action OnChange;
        private event Func<Task> OnNextStep;
        private event Func<Task> OnPreviousStep;

        public void TriggerChange() => OnChange?.Invoke();
        public void RegisterChange(Action action) => OnChange += action;
        public async Task NextStepAsync() => await OnNextStep?.Invoke();
        public void RegisterOnNextStepAsync(Func<Task> action) => OnNextStep += action;
        public async Task PreviousStepAsync() => await OnPreviousStep?.Invoke();
        public void RegisterOnPreviousStepAsync(Func<Task> action) => OnPreviousStep += action;
    }
}

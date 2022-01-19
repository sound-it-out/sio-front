using System;
using System.Threading.Tasks;

namespace SIO.Front.Client.Components
{
    public interface IWizardInteraction
    {
        public void TriggerChange();
        public void RegisterChange(Action action);
        public Task NextStepAsync();
        public void RegisterOnNextStepAsync(Func<Task> action);
        public Task PreviousStepAsync();
        public void RegisterOnPreviousStepAsync(Func<Task> action);
    }
}

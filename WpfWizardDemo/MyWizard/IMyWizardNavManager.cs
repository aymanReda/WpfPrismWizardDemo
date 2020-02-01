using WpfWizardDemo.MyWizard.EventsArgs;

namespace WpfWizardDemo.MyWizard.Events
{
    public interface IMyWizardNavManager
    {
        void Back();
        void Next(MyWizardNavEventArgs args);
        void Start(string regionName);
    }
}
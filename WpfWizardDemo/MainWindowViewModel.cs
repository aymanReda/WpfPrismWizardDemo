using CommonServiceLocator;
using System.Windows.Input;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IMyWizardNavManager _myWizardNavManager;
        public MainWindowViewModel()
        {
            _myWizardNavManager = ServiceLocator.Current.GetInstance<IMyWizardNavManager>();
        }

        private ICommand _startWizardCommand;

        public ICommand StartWizardCommand
        {
            get
            {
                return (_startWizardCommand ?? (_startWizardCommand = new Command(StartWizard)));
            }
        }

        private void StartWizard()
        {
            _myWizardNavManager.Start(Regions.MainRegion);
        }
    }
}
using Prism.Events;
using System.Windows.Input;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo.MyWizard.ViewModels
{
    public class WizardThreeViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _completedToken;

        public WizardThreeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _completedToken = _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Subscribe(Completed);
        }

        private string _helloText;
        public string HelloText
        {
            get { return _helloText; }
            set
            {
                _helloText = value;
                OnPropertyChanged(nameof(HelloText));
            }
        }

        private ICommand _prevCommand;
        public ICommand PrevCommand
        {
            get
            {
                return _prevCommand ?? (_prevCommand = new Command(Prev));
            }
        }

        private ICommand _doneCommand;
        public ICommand DoneCommand
        {
            get
            {
                return _doneCommand ?? (_doneCommand = new Command(End));
            }
        }

        public void Prev()
        {
            _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Publish();
        }

        public void Completed(MyWizardNavEventArgs args)
        {
            HelloText = $"Hello {args.Person.Name}, Working in {args.Person.Company} as {args.Person.Position}"; 
            _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Unsubscribe(_completedToken);
        }

        public void End()
        {
            _eventAggregator.GetEvent<MyWizardNavNextEvent>().Publish(null);
        }
    }
}

using Prism.Events;
using System.Windows.Input;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
using WpfWizardDemo.MyWizard.Models;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo.MyWizard.ViewModels
{
    public class WizardTwoViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private SubscriptionToken _nextCompletedEventToken;

        public WizardTwoViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _nextCompletedEventToken = _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Subscribe(NextCompleted);
        }

        private Person _person;
        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
                OnPropertyChanged(nameof(Person));
            }
        }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return (_nextCommand ?? (_nextCommand = new Command(Next)));
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

        public void Next()
        {
            _eventAggregator.GetEvent<MyWizardNavNextEvent>().Publish(new MyWizardNavEventArgs { Person = Person });
        }

        public void Prev()
        {
            _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Publish();
        }

        private void NextCompleted(MyWizardNavEventArgs args)
        {
            Person = args.Person;
            var completedEvent = _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>();
            completedEvent.Unsubscribe(_nextCompletedEventToken);

        }
    }
}

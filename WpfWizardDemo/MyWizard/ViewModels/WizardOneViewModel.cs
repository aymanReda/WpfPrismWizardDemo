using Prism.Events;
using System;
using System.Windows.Input;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
using WpfWizardDemo.MyWizard.Models;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo.MyWizard.ViewModels
{
    public class WizardOneViewModel : BaseViewModel
    {
        private readonly SubscriptionToken _prevToken;
        private readonly IEventAggregator _eventAggregator;

        public WizardOneViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _prevToken = _eventAggregator.GetEvent<MyWizardNavPrevCompletedEvent>().Subscribe(PrevCompleted);
        }

        private Person _person = new Person();
        public Person Person
        {
            get { return _person; }
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

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                return (_backCommand ?? (_backCommand = new Command(End)));
            }
        }

        public void Next()
        {
            _eventAggregator.GetEvent<MyWizardNavNextEvent>().Publish(new EventsArgs.MyWizardNavEventArgs { Person = Person });
        }

        public void End()
        {
            _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Publish(null);
        }

        private void PrevCompleted(MyWizardNavEventArgs args)
        {
            Person = args?.Person;
            _eventAggregator.GetEvent<MyWizardNavPrevCompletedEvent>().Unsubscribe(_prevToken);
        }
    }
}

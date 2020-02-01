using Prism.Events;
using System;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
using WpfWizardDemo.MyWizard.Models;

namespace WpfWizardDemo.MyWizard.ViewModels
{
    public class WizardTwoChildOneViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private SubscriptionToken _nextCompletedEventToken;
        public WizardTwoChildOneViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
             _nextCompletedEventToken = _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Subscribe(NextCompleted);
        }

        private string _helloText;
        public string HelloText
        {
            get { return _helloText; }
            set
            {
                _helloText = value;
                OnPropertyChanged(nameof(_helloText));
            }
        }

        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                HelloText = $"Hello {_person.Name}";
                OnPropertyChanged(nameof(Person));
            }
        }

        private void NextCompleted(MyWizardNavEventArgs args)
        {
            Person = args.Person;
            _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Unsubscribe(_nextCompletedEventToken);
        }
    }
}

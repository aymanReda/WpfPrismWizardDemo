using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
using WpfWizardDemo.MyWizard.Views;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo.MyWizard
{
    public class MyWizardNavManager : IMyWizardNavManager, IDisposable
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private SubscriptionToken _nextToken;
        private SubscriptionToken _prevToken;
        
        private string _regionName;
        private int _currentPage;

        private readonly Dictionary<int, string> _sequence = new Dictionary<int, string>
        {
            { 1, nameof(WizardOne) },
            { 2, nameof(WizardTwo) },
            { 3, nameof(WizardThree) }
        };

        private readonly Dictionary<int, Tuple<string, string>[]> _subSequences = new Dictionary<int, Tuple<string, string>[]>
        {
            { 
                2, 
                new [] 
                {
                    new Tuple<string, string>(Regions.MyWizardTwoChildOne, nameof(WizardTwoChildOne)),
                    new Tuple<string, string>(Regions.MyWizardTwoChildTwo, nameof(WizardTwoChildTwo))
                }
            }
        };

        public MyWizardNavManager(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        public void Start(string regionName)
        {
            _regionName = regionName;

            _currentPage = 1;

            _regionManager.RequestNavigate(_regionName, _sequence[_currentPage]);

            _nextToken = _eventAggregator.GetEvent<MyWizardNavNextEvent>().Subscribe(Next);
            _prevToken = _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Subscribe(Back);
        }

        public void Next(MyWizardNavEventArgs args)
        {
            if (_currentPage == _sequence.Max(s => s.Key))
            {
                CloseWizard();
                return;
            }

            _currentPage++;

            _regionManager.RequestNavigate(_regionName, _sequence[_currentPage], n => {

                if (_subSequences.ContainsKey(_currentPage))
                {
                    foreach (var subView in _subSequences[_currentPage])
                    {
                        _regionManager.RequestNavigate(subView.Item1, subView.Item2);
                    }
                }

                _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Publish(args);
            });
        }

        public void Back()
        {
            if (_currentPage == _sequence.Min(s => s.Key))
            {
                CloseWizard();
                return;
            }

            _currentPage--;

            _regionManager.RequestNavigate(_regionName, _sequence[_currentPage]);
        }

        public void CloseWizard()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            _eventAggregator.GetEvent<MyWizardNavNextEvent>().Unsubscribe(_nextToken);
            _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Unsubscribe(_prevToken);

            _regionManager.Regions[_regionName].RemoveAll();
        }
    }
}

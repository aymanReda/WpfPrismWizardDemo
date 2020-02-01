using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.EventsArgs;
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
            { 1, ViewsDefinitions.WizardOne },
            { 2, ViewsDefinitions.WizardTwo },
            { 3, ViewsDefinitions.WizardThree }
        };

        private readonly Dictionary<int, Tuple<string, string>[]> _subSequences = new Dictionary<int, Tuple<string, string>[]>
        {
            { 
                2, 
                new [] 
                {
                    new Tuple<string, string>(Regions.MyWizardTwoChildOne, ViewsDefinitions.WizardTwoChildOne),
                    new Tuple<string, string>(Regions.MyWizardTwoChildTwo, ViewsDefinitions.WizardTwoChildTwo)
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
            ResetWizard();

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
                ResetWizard();
                return;
            }

            _currentPage++;

            Navigate(args, () => _eventAggregator.GetEvent<MyWizardNavNextCompletedEvent>().Publish(args));
        }

        public void Back(MyWizardNavEventArgs args)
        {
            if (_currentPage == _sequence.Min(s => s.Key))
            {
                ResetWizard();
                return;
            }

            _currentPage--;

            Navigate(args, () => _eventAggregator.GetEvent<MyWizardNavPrevCompletedEvent>().Publish(args));
        }

        public void ResetWizard()
        {
            this.Dispose();
        }

        private void Navigate(MyWizardNavEventArgs args, Action callback)
        {
            var region = _regionManager.Regions[_regionName];

            _regionManager.Regions[_regionName].RemoveAll();

            _regionManager.RequestNavigate(_regionName, _sequence[_currentPage], _ => {

                if (_subSequences.ContainsKey(_currentPage))
                {
                    foreach (var subView in _subSequences[_currentPage])
                    {
                        _regionManager.RequestNavigate(subView.Item1, subView.Item2);
                    }
                }

                callback();
            });
        }

        public void Dispose()
        {
            _eventAggregator.GetEvent<MyWizardNavNextEvent>().Unsubscribe(_nextToken);
            _eventAggregator.GetEvent<MyWizardNavPrevEvent>().Unsubscribe(_prevToken);

            if (_regionManager.Regions.Any(r => r.Name == _regionName))
            {
                _regionManager.Regions[_regionName].RemoveAll();
            }
        }
    }
}

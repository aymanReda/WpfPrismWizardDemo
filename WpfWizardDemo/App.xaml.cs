using CommonServiceLocator;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using Unity;
using WpfWizardDemo.MyWizard;
using WpfWizardDemo.MyWizard.Events;
using WpfWizardDemo.MyWizard.ViewModels;
using WpfWizardDemo.MyWizard.Views;
using WpfWizardDemo.Utilities;

namespace WpfWizardDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private IUnityContainer _container;

        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _container = containerRegistry.GetContainer();

            containerRegistry.Register<MainWindowViewModel>();

            containerRegistry.Register<WizardOneViewModel>();
            containerRegistry.Register<WizardTwoViewModel>();
            containerRegistry.Register<WizardThreeViewModel>();
            containerRegistry.Register<WizardTwoChildOneViewModel>();
            containerRegistry.Register<WizardTwoChildTwoViewModel>();
            containerRegistry.Register<IMyWizardNavManager, MyWizardNavManager>();

            containerRegistry.RegisterForNavigation<WizardOne>(nameof(WizardOne));
            containerRegistry.RegisterForNavigation<WizardTwo>(nameof(WizardTwo));
            containerRegistry.RegisterForNavigation<WizardThree>(nameof(WizardThree));
            containerRegistry.RegisterForNavigation<WizardTwoChildOne>(nameof(WizardTwoChildOne));
            containerRegistry.RegisterForNavigation<WizardTwoChildTwo>(nameof(WizardTwoChildTwo));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}

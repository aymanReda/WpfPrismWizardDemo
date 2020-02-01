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
        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<WizardOneViewModel>();
            containerRegistry.Register<WizardTwoViewModel>();
            containerRegistry.Register<WizardThreeViewModel>();
            containerRegistry.Register<WizardTwoChildOneViewModel>();
            containerRegistry.Register<WizardTwoChildTwoViewModel>();
            containerRegistry.Register<IMyWizardNavManager, MyWizardNavManager>();

            containerRegistry.RegisterForNavigation<WizardOne>(ViewsDefinitions.WizardOne);
            containerRegistry.RegisterForNavigation<WizardTwo>(ViewsDefinitions.WizardTwo);
            containerRegistry.RegisterForNavigation<WizardThree>(ViewsDefinitions.WizardThree);
            containerRegistry.RegisterForNavigation<WizardTwoChildOne>(ViewsDefinitions.WizardTwoChildOne);
            containerRegistry.RegisterForNavigation<WizardTwoChildTwo>(ViewsDefinitions.WizardTwoChildTwo);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}

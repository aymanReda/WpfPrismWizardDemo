using System.Windows.Controls;
using WpfWizardDemo.MyWizard.ViewModels;

namespace WpfWizardDemo.MyWizard.Views
{
    /// <summary>
    /// Interaction logic for WizardTwo.xaml
    /// </summary>
    public partial class WizardTwo : UserControl
    {
        public WizardTwo(WizardTwoViewModel vm)
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                DataContext = vm;
            };
        }
    }
}

using System.Windows.Controls;
using WpfWizardDemo.MyWizard.ViewModels;

namespace WpfWizardDemo.MyWizard.Views
{
    /// <summary>
    /// Interaction logic for WizardThree.xaml
    /// </summary>
    public partial class WizardThree : UserControl
    {
        public WizardThree(WizardThreeViewModel vm)
        {
            InitializeComponent();
            Loaded += (_, __) =>
            {
                DataContext = vm;
            };
        }
    }
}

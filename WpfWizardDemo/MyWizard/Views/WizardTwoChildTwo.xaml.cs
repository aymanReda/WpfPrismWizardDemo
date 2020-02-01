using System.Windows.Controls;
using WpfWizardDemo.MyWizard.ViewModels;

namespace WpfWizardDemo.MyWizard.Views
{
    /// <summary>
    /// Interaction logic for WizardTwoChildTwo.xaml
    /// </summary>
    public partial class WizardTwoChildTwo : UserControl
    {
        public WizardTwoChildTwo(WizardTwoChildTwoViewModel vm)
        {
            InitializeComponent();
            Loaded += (_, __) =>
            {
                DataContext = vm;
            };
        }
    }
}

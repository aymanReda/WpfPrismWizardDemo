using System.Windows.Controls;
using WpfWizardDemo.MyWizard.ViewModels;

namespace WpfWizardDemo.MyWizard.Views
{
    /// <summary>
    /// Interaction logic for WizardTwoChildOne.xaml
    /// </summary>
    public partial class WizardTwoChildOne : UserControl
    {
        public WizardTwoChildOne(WizardTwoChildOneViewModel vm)
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                DataContext = vm;
            };
        }
    }
}

using System.Windows.Controls;
using WpfWizardDemo.MyWizard.ViewModels;

namespace WpfWizardDemo.MyWizard.Views
{
    /// <summary>
    /// Interaction logic for WizardOne.xaml
    /// </summary>
    public partial class WizardOne : UserControl
    {
        public WizardOne(WizardOneViewModel vm)
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                DataContext = vm;
            };
        }
    }
}

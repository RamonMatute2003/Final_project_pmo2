using Final_project.Screens;
using PdfSharpCore.Fonts;

namespace Final_project {
    public partial class App:Application {
        public App() {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc3NjI0M0AzMjMzMmUzMDJlMzBsVFQ2bGYwYzBIeUp0ajhHNkowOFUyZFVCaUFhU01RZENjb0VFcGlESk5JPQ==");
            MainPage=new NavigationPage(new MainPage());
        }
    }
}
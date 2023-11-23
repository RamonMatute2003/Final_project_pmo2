using Final_project.Screens;
using PdfSharpCore.Fonts;

namespace Final_project {
    public partial class App:Application {
        public App() {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg3NzM3OUAzMjMzMmUzMDJlMzBlekJ4LzROWlk5UVluOTVsVTBKMGtacEdCQ3BLektkbnJZNWtiUG83TnBBPQ==");
            MainPage=new NavigationPage(new MainPage());
        }
    }
}
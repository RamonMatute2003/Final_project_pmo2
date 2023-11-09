namespace Final_project {
    public partial class AppShell:Shell {
        public AppShell() {
            InitializeComponent();
        }

        private void OnCloseMenuTapped(object sender,EventArgs e) {
            Current.FlyoutIsPresented=false;
        }
    }
}
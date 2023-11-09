namespace Final_project.Settings {
    public class Logout_page:ContentPage {
        protected override void OnAppearing() {
            base.OnAppearing();

            Application.Current.MainPage=new NavigationPage(new MainPage());
        }
    }
}

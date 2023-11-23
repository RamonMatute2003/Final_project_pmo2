namespace Final_project.Settings {
    public class Logout_page:ContentPage {
        protected override void OnAppearing() {
            base.OnAppearing();

            Temporary_data.page=null;
            Temporary_data.names=null;
            Temporary_data.surnames=null;
            Temporary_data.birthdate=null;
            Temporary_data.dni=null;
            Temporary_data.email=null;
            Temporary_data.email2=null;
            Temporary_data.password=null;
            Temporary_data.amount=0;
            Temporary_data.pay_amount=0;
            Temporary_data.account_number=null;
            Temporary_data.access=0;
            Temporary_data.service=null;
            Temporary_data.id_service_category=0;
            Temporary_data.id_sender_user=0;
            Temporary_data.id_receiving_user=0;
            Temporary_data.id_type=0;
            Temporary_data.code_record=null;
            Temporary_data.date=null;
            Temporary_data.description=null;
            Temporary_data.id_service=0;

            Application.Current.MainPage=new NavigationPage(new MainPage());
        }
    }
}

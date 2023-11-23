using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Globalization;
using System.Text.Json;

namespace Final_project.Screens{ 
    public partial class Page_settings:ContentPage {
        public Page_settings(){
            InitializeComponent();

            txt_names.Text=Temporary_data.names+" "+Temporary_data.surnames;
            txt_account_number.Text=Temporary_data.account_number;
            txt_date.Text=Temporary_data.birthdate;
            txt_email.Text=Temporary_data.email;
            txt_user.Text=Temporary_data.user;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            await validate_user();
            txt_email.Text=Temporary_data.email;
        }

        private async Task validate_user() {
            Table_users users = new Table_users("","","","","","","",0,Temporary_data.account_number,0,0);
            string response = "";

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(users,Connection_bd.select_validate_user));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            List<Table_users> list = JsonSerializer.Deserialize<List<Table_users>>(response);

            if(list.Count>0) {
                Temporary_data.account_number=list[0].account_number;
                Temporary_data.password=list[0].password;
                Temporary_data.email=list[0].email;
            }
        }

        private async void change_password(object sender,TappedEventArgs e){
            Temporary_data.page="";
            await Navigation.PushModalAsync(new Password_verification_modal());
        }

        private async void change_email(object sender,TappedEventArgs e){
            Temporary_data.page="Settings";
            await Navigation.PushModalAsync(new Password_verification_modal());
        }
    }
}
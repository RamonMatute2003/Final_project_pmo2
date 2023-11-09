using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Screens;
using Final_project.Settings;
using System.Text.Json;

namespace Final_project {
    public partial class MainPage:ContentPage {

        public MainPage() {
            InitializeComponent();

            Design.remove_stripe();
        }

        private async void get_into(object sender,EventArgs e) {
            if(!(string.IsNullOrEmpty(txt_user.Text)||string.IsNullOrEmpty(txt_password.Text))) {
                if(Validations.validate_user(txt_user.Text)) {
                    if(Validations.validate_password(txt_password.Text)) {
                        if(await select_user()) {
                            if(Temporary_data.access==1) {
                                App.Current.MainPage=new AppShell();
                            } else {
                                await DisplayAlert("Advertencia","Sin acceso, espere a que se revise su cuenta","OK");
                            }
                        } else {
                            await DisplayAlert("Advertencia","Usuario o contraseña incorrecto","OK");
                        }
                    } else {
                        await DisplayAlert("Advertencia","Revisa bien tu contraseña que ingresaste","OK");
                    }
                } else {
                    await DisplayAlert("Advertencia","Revisa bien tu correo electronico que ingresaste","OK");
                }
            } else {
                await DisplayAlert("Advertencia","No dejar campos vacios","OK");
            }
        }

        private async Task<bool> select_user() {
            Table_users users = new Table_users("","","","","",txt_password.Text,txt_user.Text,0,"",0,0);
            string response = "";

            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(users,Connection_bd.select_user));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            List<Table_users> list = JsonSerializer.Deserialize<List<Table_users>>(response);

            if(list.Count>0) {
                Temporary_data.user=list[0].user;
                Temporary_data.names=list[0].names;
                Temporary_data.surnames=list[0].surnames;
                Temporary_data.birthdate=list[0].birthdate;
                Temporary_data.dni=list[0].dni;
                Temporary_data.email=list[0].email;
                Temporary_data.password=list[0].password;
                Temporary_data.amount=list[0].amount;
                Temporary_data.account_number=list[0].account_number;
                Temporary_data.access=list[0].access;
                Temporary_data.id_sender_user=list[0].id_user;
                return true;
            }

            return false;
        }

        private async void link_sign_up(object sender,TappedEventArgs e) {
            await Navigation.PushAsync(new Page_sign_up());
        }

        private async void link_recover_password(object sender,TappedEventArgs e) {
            await Navigation.PushModalAsync(new Email_verification_modal());
        }
    }
}
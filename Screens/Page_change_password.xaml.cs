using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Settings;

namespace Final_project.Screens {
    public partial class Page_change_password:ContentPage {
        public Page_change_password() {
            InitializeComponent();

            Design.remove_stripe();
        }

        private async Task change_password() {
            Table_users users = new Table_users("","","","",Temporary_data.email,txt_repeat_password.Text,"",0,"",0,0);
            string response = "";
            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.insert_update_async(users,Connection_bd.update_password));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            if(response=="exitoso") {
                await DisplayAlert("Exitoso","Tu contraseña se ha cambiado exitosamente","OK");
                Temporary_data.password=response;
                await Navigation.PopAsync();
                await Navigation.PopModalAsync();
                await Navigation.PopModalAsync();
            } else {
                await DisplayAlert("Advertencia","No se modifico contraseña: "+response,"OK");
            }
        }

        private async void validate_field(object sender,EventArgs e) {

            if(string.IsNullOrEmpty(txt_new_password.Text)||string.IsNullOrEmpty(txt_repeat_password.Text)) {
                await DisplayAlert("Advertencia","No dejar campos vacios","OK");
            } else {
                if(Validations.validate_password(txt_new_password.Text)) {
                    if(Validations.validate_password(txt_repeat_password.Text)) {
                        if(txt_new_password.Text==txt_repeat_password.Text) {
                            await change_password();
                        } else {
                            await DisplayAlert("Advertencia","Las contraseñas no son iguales","OK");
                        }
                    } else {
                        await DisplayAlert("Advertencia","Revisa la contraseña repetida, esta mal escrita","OK");
                    }
                } else {
                    await DisplayAlert("Advertencia","Revisa la contraseña, esta mal escrita","OK");
                }
            }
        }
    }
}
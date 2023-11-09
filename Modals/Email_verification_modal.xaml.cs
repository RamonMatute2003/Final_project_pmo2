using Final_project.Rest_api;
using Final_project.Screens;

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-maccatalyst)'
Antes:
using Microsoft.Maui;
using Final_project.Rest_api;
Después:
using Final_project.Settings;
using Microsoft.Maui;
*/

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-ios)'
Antes:
using Microsoft.Maui;
using Final_project.Rest_api;
Después:
using Final_project.Settings;
using Microsoft.Maui;
*/

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-windows10.0.19041.0)'
Antes:
using Microsoft.Maui;
using Final_project.Rest_api;
Después:
using Final_project.Settings;
using Microsoft.Maui;
*/
using Final_project.Settings;
using System.Text.Json;

namespace Final_project.Modals {
    public partial class Email_verification_modal:ContentPage {
        public Email_verification_modal() {
            InitializeComponent();
        }

        private async void cancel_modal(object sender,EventArgs e) {
            await Navigation.PopModalAsync();
        }

        private async void verify(object sender,EventArgs e) {

            if(string.IsNullOrEmpty(txt_email.Text)) {
                await DisplayAlert("Advertencia","No dejar campos vacios","OK");
            } else {
                if(Validations.validate_email(txt_email.Text)) {
                    if(await email_exists()) {
                        Temporary_data.email=txt_email.Text;
                        Temporary_data.page="MainPage";

                        Page_verification_code page = new Page_verification_code();

                        page.ToolbarItems.Add(new ToolbarItem {
                            Text="Regresar",
                            Command=new Command(async () => await Navigation.PopModalAsync()),
                            Order=ToolbarItemOrder.Primary,
                            Priority=0
                        });

                        page.Title="";

                        await Navigation.PushModalAsync(new NavigationPage(page));
                    } else {
                        await DisplayAlert("Advertencia","Usuario no encontrado con ese correo","OK");
                    }

                } else {
                    await DisplayAlert("Advertencia","Error al escribir el correo electronico","OK");
                }
            }
        }

        private async Task<bool> email_exists() {
            Table_users users = new Table_users("","","","",txt_email.Text,"","",0,"",0,0);
            string response = "";

            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(users,Connection_bd.select_email));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            List<Table_users> list = JsonSerializer.Deserialize<List<Table_users>>(response);

            if(list.Count>0) {
                return true;
            }

            return false;
        }
    }
}
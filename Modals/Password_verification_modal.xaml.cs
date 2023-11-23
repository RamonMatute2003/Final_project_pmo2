using Final_project.Screens;
using Final_project.Settings;

namespace Final_project.Modals;

public partial class Password_verification_modal : ContentPage
{
	public Password_verification_modal(){
		InitializeComponent();
	}

    private async void cancel_modal(object sender,EventArgs e) {
        await Navigation.PopModalAsync();
    }

    private async void verify(object sender,EventArgs e) {

        if(string.IsNullOrEmpty(txt_password.Text)) {
            await DisplayAlert("Advertencia","No dejar campos vacios","OK");
        } else {
            if(Validations.validate_password(txt_password.Text)) {
                if(txt_password.Text==Temporary_data.password){
                    if(Temporary_data.page=="Settings") {
                        await Navigation.PushModalAsync(new Change_email_modal());
                    } else {
                        await Navigation.PushModalAsync(new Page_change_password());
                    }
                } else {
                    await DisplayAlert("Advertencia","Contraseña incorrecta","OK");
                }

            } else {
                await DisplayAlert("Advertencia","Error al escribir la contraseña","OK");
            }
        }
    }
}
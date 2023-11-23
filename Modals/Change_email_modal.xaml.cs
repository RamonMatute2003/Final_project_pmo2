using Final_project.Rest_api;
using Final_project.Screens;
using Final_project.Settings;
using System.Text.Json;

namespace Final_project.Modals;

public partial class Change_email_modal : ContentPage
{
	public Change_email_modal()
	{
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
                if(await repeated_email()) {
                    Temporary_data.email2=txt_email.Text;
                    Page_verification_code page = new Page_verification_code();

                    page.ToolbarItems.Add(new ToolbarItem {
                        Text="Regresar",
                        Command=new Command(async () => await Navigation.PopModalAsync()),
                        Order=ToolbarItemOrder.Primary,
                        Priority=0
                    });

                    page.Title="";

                    Temporary_data.page="Page_settings_email";
                    await Navigation.PushModalAsync(new NavigationPage(page));
                } else {
                    await DisplayAlert("Advertencia","Correo ya en uso","OK");
                }

            } else {
                await DisplayAlert("Advertencia","Error al escribir la contraseña","OK");
            }
        }
    }

    private async Task<bool> repeated_email() {
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
            return false;
        }

        return true;
    }
}
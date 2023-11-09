using Final_project.Rest_api;
using Final_project.Settings;
using System.Globalization;
using System.Text.Json;

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-maccatalyst)'
Antes:
using Final_project.Rest_api;
using System.Text.Json;
Después:
using System.Text.Json;
using static System.Text.Json;
*/

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-ios)'
Antes:
using Final_project.Rest_api;
using System.Text.Json;
Después:
using System.Text.Json;
using static System.Text.Json;
*/

/* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-windows10.0.19041.0)'
Antes:
using Final_project.Rest_api;
using System.Text.Json;
Después:
using System.Text.Json;
using static System.Text.Json;
*/

namespace Final_project.Screens;

public partial class Page_home:ContentPage {
    public Page_home() {
        InitializeComponent();
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        await select_user();
        lbl_name.Text="Cuenta bancaria: "+Temporary_data.account_number;
        string formatoMoneda = Temporary_data.amount.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));
        lbl_amount.Text=formatoMoneda;
    }

    private async Task select_user() {
        Table_users users = new Table_users("","","","","",Temporary_data.password,Temporary_data.user,0,"",0,0);
        string response = "";

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.select_async(users,Connection_bd.select_user));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        List<Table_users> list = JsonSerializer.Deserialize<List<Table_users>>(response);

        if(list.Count>0) {
            Temporary_data.amount=list[0].amount;
            Temporary_data.names=list[0].names;
            Temporary_data.surnames=list[0].surnames;
            Temporary_data.account_number=list[0].account_number;
        }
    }

    private async void page_transfer(object sender,EventArgs e) {
        await Shell.Current.GoToAsync("//Page_transfer");
    }

    private async void page_service_categories(object sender,EventArgs e) {
        await Shell.Current.GoToAsync("//Page_service_categories");
    }

    private async void page_account_manager(object sender,EventArgs e) {
        await Shell.Current.GoToAsync("//Page_account_manager");
    }

    private void page_main_page(object sender,EventArgs e) {
        App.Current.MainPage=new MainPage();
    }

    private async void page_finance(object sender,EventArgs e) {
        await Shell.Current.GoToAsync("//Page_finance");
    }

    private async void page_settings(object sender,EventArgs e) {
        await Shell.Current.GoToAsync("//Page_settings");
    }
}
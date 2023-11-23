using Final_project.Rest_api;
using Final_project.Screens;
using Final_project.Settings;
using System.Globalization;
using System.Text.Json;

namespace Final_project.Modals;

public partial class Transfer_modal:ContentPage {
    double amount_user;
    string desciption="";

    public Transfer_modal() {
        InitializeComponent();
        amount_user=Temporary_data.amount;
        lbl_description.Text=Temporary_data.description;
        desciption=Temporary_data.description;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        lbl_account_name.Text="#"+Temporary_data.account_number+" "+Temporary_data.names+" "+Temporary_data.surnames;
        lbl_amount.Text=Temporary_data.pay_amount.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));
        await select_user();
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

    private async void transfer(object sender,EventArgs e) {
        bool answer = await DisplayAlert("Alerta","¿Desea hacer la tamsferencia?","OK","Cancelar");

        if(answer) {
            if(await insert_record()&&await change_amount()) {
                await DisplayAlert("Exitoso","Transferencia realizada con exito","OK");
                await change_amount_user();

                await Navigation.PushModalAsync(new Page_transfer_or_payment_result());

            } else {
                await DisplayAlert("Alerta","Transferencia erroneo","OK");
            }
        }
    }

    private async Task<bool> insert_record() {
        Table_records record = new Table_records(Temporary_data.id_sender_user,2,Temporary_data.pay_amount,generate_code(),generate_date(),Temporary_data.description,1);
        string response = "";
        var loadingModal = new Loading_modal();
        await Navigation.PushModalAsync(loadingModal);

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.insert_update_async(record,Connection_bd.insert_record));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        await Navigation.PopModalAsync();

        if(response=="exitoso") {
            return true;
        }

        return false;
    }

    private async Task<bool> change_amount() {
        Table_users user = new Table_users("","","","","","","",amount_user+Temporary_data.pay_amount,"",0,Temporary_data.id_receiving_user);

        string response = "";
        var loadingModal = new Loading_modal();
        await Navigation.PushModalAsync(loadingModal);

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.insert_update_async(user,Connection_bd.update_amount));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        await Navigation.PopModalAsync();

        if(response=="exitoso") {
            return true;
        }

        return false;
    }

    private async Task change_amount_user() {
        Table_users user = new Table_users("","","","","","","",Temporary_data.amount-Temporary_data.pay_amount,"",0,Temporary_data.id_sender_user);

        string response = "";
        var loadingModal = new Loading_modal();
        await Navigation.PushModalAsync(loadingModal);

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.insert_update_async(user,Connection_bd.update_amount));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        await Navigation.PopModalAsync();
    }

    private string generate_date() {
        DateTime date = DateTime.Now;

        return date.Year+"-"+date.Month+"-"+date.Day;
    }

    private string generate_code() {
        DateTime date = DateTime.Now;
        Random random = new Random();

        Temporary_data.code_record=date.Second.ToString()+random.Next(100,1000).ToString()+date.Minute.ToString()+random.Next(1000,10000).ToString();
        return Temporary_data.code_record;
    }

    private async void cancel_modal(object sender,EventArgs e) {
        await Navigation.PopModalAsync();
    }
}
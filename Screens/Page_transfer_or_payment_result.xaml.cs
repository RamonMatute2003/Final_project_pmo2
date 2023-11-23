using Final_project.Rest_api;
using Final_project.Settings;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Final_project.Screens;

public partial class Page_transfer_or_payment_result:ContentPage {
    string email_reciving;
    public Page_transfer_or_payment_result() {
        InitializeComponent();
        select_user_send();
        select_user_reciving();
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        await select_record();
    }

    private async Task select_user_send(){
        Table_users users=new Table_users("","","","","",Temporary_data.password,Temporary_data.user,0,"",0,0);
        string response="";

        try{
            Methods insert=new Methods();
            response=await Task.Run(()=>insert.select_async(users,Connection_bd.select_user));
        }catch(Exception ex){
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        List<Table_users> list = JsonSerializer.Deserialize<List<Table_users>>(response);

        if(list.Count>0){
            lbl_name_send.Text=list[0].names+" "+list[0].surnames;
            lbl_account_number_send.Text="#"+list[0].account_number;
        }
    }

    private async Task select_user_reciving(){
        Table_users users=new Table_users("","","","","","","",0,"",0,Temporary_data.id_receiving_user);
        string response="";

        try{
            Methods insert=new Methods();
            response=await Task.Run(()=>insert.select_async(users,Connection_bd.select_user_record));
        }catch(Exception ex){
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        List<Table_users> list=JsonSerializer.Deserialize<List<Table_users>>(response);

        if(list.Count>0) {
            lbl_name_receiving.Text=list[0].names+" "+list[0].surnames;
            lbl_account_number_receiving.Text="#"+list[0].account_number;
            email_reciving=list[0].email;
        }
    }

    private async Task select_record(){
        Table_records users=new Table_records(0,0,0,Temporary_data.code_record,"","",0);
        string response="";

        try{
            Methods insert=new Methods();
            response=await Task.Run(() => insert.select_async(users,Connection_bd.select_record));
        }catch(Exception ex){
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        List<Table_records> list=JsonSerializer.Deserialize<List<Table_records>>(response);

        if(list.Count>0){
            lbl_description.Text="Descripción: "+list[0].description;
            lbl_date.Text="Fecha: "+list[0].date;
            lbl_amount.Text="Monto: "+list[0].amount.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));

            if(list[0].id_type==1){
                lbl_type.Text="Tipo: Servicio";
            } else{
                lbl_type.Text="Tipo: Transferencia";
            }
        }
        await send_email();
        await send_email1();
    }

    private async void cancel_modal(object sender,EventArgs e){
        Temporary_data.code_record=null;
        await Navigation.PopModalAsync();
        await Shell.Current.Navigation.PopModalAsync();
    }

    private async Task send_email1() {
        Table_join_data users = new Table_join_data();
        users.email=email_reciving;
        users.names=lbl_name_send.Text;
        users.account_number=lbl_account_number_send.Text;
        users.surnames=lbl_name_receiving.Text;
        users.dni=lbl_account_number_receiving.Text;
        users.description=lbl_description.Text.Substring(13);
        users.date=lbl_date.Text.Substring(7);
        users.amount_format=lbl_amount.Text.Substring(7);
        users.type=lbl_type.Text.Substring(6);
        users.code=lbl_code.Text.Substring(16);

        string response = "";

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.select_async(users,Connection_bd.send_receipt));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }
    }

    private async Task send_email() {
        Table_join_data users = new Table_join_data();
        users.email=Temporary_data.email;
        users.names=lbl_name_send.Text;
        users.account_number=lbl_account_number_send.Text;
        users.surnames=lbl_name_receiving.Text;
        users.dni=lbl_account_number_receiving.Text;
        users.description=lbl_description.Text.Substring(13);
        users.date=lbl_date.Text.Substring(7);
        users.amount_format=lbl_amount.Text.Substring(7);
        users.type=lbl_type.Text.Substring(6);
        users.code=lbl_code.Text.Substring(16);

        string response = "";

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.select_async(users,Connection_bd.send_receipt));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }
    }
}
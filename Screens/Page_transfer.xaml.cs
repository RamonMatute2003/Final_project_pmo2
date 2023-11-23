using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Text.Json;

namespace Final_project.Screens;

public partial class Page_transfer:ContentPage {
    public Page_transfer() {
        InitializeComponent();

        Design.remove_stripe();
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        if(Temporary_data.code_record==null){
            txt_account_number.Text="";
            txt_amount.Text="";
            txt_description.Text="";
        }
    }

    private async void verification(object sender,EventArgs e) {
        if(!(string.IsNullOrEmpty(txt_account_number.Text)||string.IsNullOrEmpty(txt_amount.Text))) {
            if(Validations.validate_account_number(txt_account_number.Text)) {
                if(Validations.validate_amount(txt_amount.Text)) {
                    string desc = "";

                    if(string.IsNullOrEmpty(txt_description.Text)) {
                        desc="sin descripcion";
                    } else {
                        desc=txt_description.Text;
                    }

                    if(Validations.validate_description_service(desc)) {
                        Temporary_data.description = desc;
                        if(Temporary_data.account_number!=txt_account_number.Text){
                            if(await validate_user()) {
                                if(Temporary_data.amount>=Convert.ToDouble(txt_amount.Text)) {
                                    await Navigation.PushModalAsync(new Transfer_modal());
                                } else {
                                    await DisplayAlert("Alerta","Tu dinero no alcanza para hacer la transferencia","OK");
                                }
                            } else {
                                await DisplayAlert("Alerta","Numero de cuenta no existente","OK");
                            }
                        } else{
                            await DisplayAlert("Alerta","No puedes hacerte una trasnferencia tu mismo","OK");
                        }
                        
                    } else {
                        await DisplayAlert("Alerta","Revisa la descripcion que ingresaste","OK");
                    }
                } else {
                    await DisplayAlert("Alerta","Revisa el monto que ingresaste","OK");
                }
            } else {
                await DisplayAlert("Alerta","Revisa el numero de cuenta","OK");
            }
        } else {
            await DisplayAlert("Alerta","No dejar campos vacios","OK");
        }
    }

    private async Task<bool> validate_user() {
        Table_users users = new Table_users("","","","","","","",0,txt_account_number.Text,0,0);
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
            Temporary_data.names=list[0].names;
            Temporary_data.surnames=list[0].surnames;
            Temporary_data.amount=list[0].amount;
            Temporary_data.pay_amount=Convert.ToDouble(txt_amount.Text);
            Temporary_data.description=txt_description.Text;
            Temporary_data.id_receiving_user=list[0].id_user;
            return true;
        }

        return false;
    }
}
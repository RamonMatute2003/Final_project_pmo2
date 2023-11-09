using Final_project.Rest_api;
using Final_project.Screens;
using Final_project.Settings;
using System.Globalization;
using System.Text.Json;

namespace Final_project.Modals {
    public partial class Pay_service_modal:ContentPage {
        Table_users user = new Table_users();
        double amount_user, amount_user_send, pay_amount;
        public Pay_service_modal() {
            InitializeComponent();
            load_data();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            await select_user_sender();
        }

        private async Task select_user_sender() {
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
            }
        }

        private async Task load_data() {
            Table_join_data join_data = new Table_join_data();
            join_data.id_user=Temporary_data.id_receiving_user;
            join_data.id_service=Temporary_data.id_service;
            string response = "";

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(join_data,Connection_bd.select_data_payment));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            List<Table_join_data> list = JsonSerializer.Deserialize<List<Table_join_data>>(response);

            if(list.Count>0) {
                user.names=list[0].names;
                user.surnames=list[0].surnames;
                user.amount=list[0].pay_amount;
                pay_amount=user.amount;
                amount_user=list[0].amount;
                amount_user_send=user.amount;
                amount_user=amount_user+user.amount;
                user.account_number=list[0].account_number;
                lbl_account_name.Text="#"+list[0].account_number+" "+list[0].names+" "+list[0].surnames;
                lbl_service.Text=list[0].service;
                lbl_amount.Text=list[0].pay_amount.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));
            }
        }

        private async void pay(object sender,EventArgs e) {
            bool answer = await DisplayAlert("Alerta","¿Desea pagar este servicio?","OK","Cancelar");

            if(answer) {
                await select_user();
                if(Temporary_data.amount>=user.amount) {
                    string desc = "";

                    if(string.IsNullOrEmpty(txt_description.Text)) {
                        desc="Sin descripcion";
                    } else {
                        desc=txt_description.Text;
                    }

                    if(Validations.validate_description_service(desc)) {
                        if(await insert_record()&&await change_amount()) {
                            await DisplayAlert("Exitoso","Pago de servicio realizado con exito","OK");
                            await change_amount_user();
                          
                            await Navigation.PushModalAsync(new Page_transfer_or_payment_result());
                        } else {
                            await DisplayAlert("Alerta","Pago de servicio erroneo","OK");
                        }
                    } else {
                        await DisplayAlert("Alerta","Descripcion mal escrita","OK");
                    }
                } else {
                    await DisplayAlert("Alerta","Dinero insuficiente para pagar el servicio","OK");
                }
            }
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
            }
        }

        private async Task<bool> insert_record() {
            string description;
            if(string.IsNullOrEmpty(txt_description.Text)) {
                description="Sin descripcion";
            } else {
                description=txt_description.Text;
            }

            Table_records record = new Table_records(Temporary_data.id_sender_user,1,pay_amount,generate_code(),generate_date(),description,Temporary_data.id_service);
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
            user.id_user=Temporary_data.id_receiving_user;
            user.amount=amount_user;

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
            user.id_user=Temporary_data.id_sender_user;
            user.amount=Temporary_data.amount;
            amount_user_send=Temporary_data.amount-amount_user_send;
            user.amount=amount_user_send;

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
}
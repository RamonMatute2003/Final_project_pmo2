using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Text.Json;

namespace Final_project.Screens {
    public partial class Page_verification_code:ContentPage {
        string code = null;
        private bool isRunning = false;
        private CancellationTokenSource cts;

        public Page_verification_code() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(code==null) {
                code="";
                await generate_code();
                StartTimer();
            }

            if(Temporary_data.password=="exitoso") {
                await validate_user();
                await Navigation.PopModalAsync();
            }
        }

        private async Task<bool> validate_user() {
            Table_users users = new Table_users("","","","","","","",0,Temporary_data.account_number,0,0);
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
                Temporary_data.id_sender_user=list[0].id_user;
                Temporary_data.names=list[0].names;
                Temporary_data.birthdate=list[0].birthdate;
                Temporary_data.email=list[0].email;
                Temporary_data.user=list[0].user;
                Temporary_data.amount=list[0].amount;
                Temporary_data.surnames=list[0].surnames;
                Temporary_data.password=list[0].password;
                Temporary_data.amount=list[0].amount;
                Temporary_data.id_receiving_user=list[0].id_user;
                return true;
            }

            return false;
        }

        private async Task generate_code() {

            Table_users users = new Table_users("","","","",Temporary_data.email,"","",0,"",0,0);

            if(Temporary_data.page=="Page_settings_email") {
                users=new Table_users("","","","",Temporary_data.email2,"","",0,"",0,0);
            }

            string response = "";

            if(Temporary_data.page!="Page_settings_email") {
                var loadingModal = new Loading_modal();
                await Navigation.PushModalAsync(loadingModal);
            }
            

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(users,Connection_bd.send_code));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            if(Temporary_data.page!="Page_settings_email") {
                await Navigation.PopModalAsync();
            }

            List<Temporary_data> list = JsonSerializer.Deserialize<List<Temporary_data>>(response);

            foreach(var data in list) {
                code=data.code;
                break;
            }
        }

        private void on_text_changed(object sender,TextChangedEventArgs e) {
            var entry = (Entry) sender;

            if(string.IsNullOrEmpty(e.NewTextValue)) {
                move_to_previous_entry(entry);
            } else if(e.NewTextValue.Length==entry.MaxLength) {
                move_to_next_entry(entry);
            }
        }

        private void move_to_next_entry(Entry currentEntry) {
            if(currentEntry==txt_code1) {
                txt_code2.Focus();
            } else {
                if(currentEntry==txt_code2) {
                    txt_code3.Focus();
                } else {
                    if(currentEntry==txt_code3) {
                        txt_code4.Focus();
                    } else {
                        if(currentEntry==txt_code4) {
                            txt_code5.Focus();
                        } else {
                            if(currentEntry==txt_code5) {
                                txt_code6.Focus();
                            }
                        }
                    }
                }
            }

            verification_code();
        }

        private void move_to_previous_entry(Entry currentEntry) {
            if(currentEntry==txt_code2) {
                txt_code1.Focus();
            } else {
                if(currentEntry==txt_code3) {
                    txt_code2.Focus();
                } else {
                    if(currentEntry==txt_code4) {
                        txt_code3.Focus();
                    } else {
                        if(currentEntry==txt_code5) {
                            txt_code4.Focus();
                        } else {
                            if(currentEntry==txt_code6) {
                                txt_code5.Focus();
                            }
                        }
                    }
                }
            }

            verification_code();
        }

        private async Task generate_request_user() {
            await validate_user();
            Table_users users = new Table_users(Temporary_data.names+" "+Temporary_data.surnames,"",Temporary_data.birthdate,Temporary_data.dni,Temporary_data.email,"",Temporary_data.user,0,"",0,0);

            string response = "";

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(users,Connection_bd.send_email_verification));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }
        }

        private async void verification_code() {

            if(code==txt_code1.Text+txt_code2.Text+txt_code3.Text+txt_code4.Text+txt_code5.Text+txt_code6.Text) {
                if(isRunning) {
                    if(Temporary_data.page=="Page_sign_up") {
                        await insert_user();
                        await generate_request_user();
                    } else {
                        if(Temporary_data.page=="MainPage") {
                            await Navigation.PushAsync(new Page_change_password());
                        } else {
                            if(Temporary_data.page=="Page_settings") {
                                await Navigation.PushAsync(new Page_change_password());
                            }else{
                                if(Temporary_data.page=="Page_settings_email") {
                                    await change_email();
                                    await DisplayAlert("Advertencia","Correo electronico cambaido con exito","OK");
                                }
                            }
                        }
                    }


                } else {
                    await DisplayAlert("Advertencia","El tiempo se ha acabado, por favar dele a reenviar codigo","OK");
                }
            }
        }

        private async Task change_email() {

            Table_users users = new Table_users("","","","",Temporary_data.email2,"",Temporary_data.user,0,"",0,0);
            string response = "";
            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.insert_update_async(users,Connection_bd.update_email));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            if(response=="exitoso") {
                await DisplayAlert("Exitoso","Tu email se ha cambiado exitosamente","OK");
                Temporary_data.email=Temporary_data.email2;
                await Navigation.PushAsync(new AppShell());
            } else {
                await DisplayAlert("Advertencia","No se modifico contraseña: "+response,"OK");
            }
        }

        private async Task insert_user() {
            Table_users users = new Table_users(Temporary_data.names,Temporary_data.surnames,Temporary_data.date,Temporary_data.dni,Temporary_data.email,Temporary_data.password,Temporary_data.user,Temporary_data.amount,Temporary_data.account_number,0,0);
            string response = "";
            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.insert_update_async(users,Connection_bd.insert_user));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            if(response=="exitoso") {
                await DisplayAlert("Exitoso","Tu cuenta se ha creado exitosamente, debes esperar unos minutos o horas para darte acceso, por favor estar atento a tu correo electronico","OK");
                App.Current.MainPage=new MainPage();
            } else {
                await DisplayAlert("Advertencia","No se inserto usuario: "+response,"OK");
            }
        }

        private async void send_again(object sender,EventArgs e) {
            if(!isRunning) {
                StartTimer();
                await generate_code();
            } else {
                await DisplayAlert("Advertencia","Espera a que se acabe el tiempo para volver a enviar el codigo","OK");
            }
        }

        private async void StartTimer() {
            cts=new CancellationTokenSource();
            isRunning=true;

            try {

                int totalSeconds = (2*60*500)/1000;
                for(int i = 0;i<totalSeconds;i++) {
                    UpdateTextTime(totalSeconds-i);
                    await Task.Delay(1000,cts.Token);
                }

                UpdateTextTime(0);
            } finally {
                isRunning=false;
            }
        }

        private void UpdateTextTime(int secondsElapsed) {
            int minutes = secondsElapsed/60;
            int seconds = secondsElapsed%60;
            string timeString = string.Format("{0:D2}:{1:D2}",minutes,seconds);
            txt_time.Text=timeString;
        }
    }
}
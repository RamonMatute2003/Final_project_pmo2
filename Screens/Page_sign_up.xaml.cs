using Final_project.Modals;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Text.Json;

namespace Final_project.Screens {
    public partial class Page_sign_up:ContentPage {
        public Page_sign_up() {
            InitializeComponent();

            Design.remove_stripe();
        }

        private async void insert_user(object sender,EventArgs e) {
            if(empty()) {
                await DisplayAlert("Advertencia","No dejar campos vacios","OK");
            } else {
                if(Validations.validate_name(txt_names.Text)) {
                    if(Validations.validate_surname(txt_surnames.Text)) {
                        if(!Validations.validation_birthdate(dp_birthdate.Date.Year)) {
                            if(Validations.validate_dni(txt_dni.Text)) {
                                if(Validations.validate_email(txt_email.Text)) {
                                    if(Validations.validate_password(txt_password.Text)) {
                                        if(await repeated_email()) {
                                            Temporary_data.page="Page_sign_up";
                                            Temporary_data.names=txt_names.Text;
                                            Temporary_data.surnames=txt_surnames.Text;
                                            Temporary_data.date=get_date();
                                            Temporary_data.dni=txt_dni.Text;
                                            Temporary_data.email=txt_email.Text;
                                            Temporary_data.password=txt_password.Text;
                                            Temporary_data.user=generate_user();
                                            Temporary_data.amount=2000;
                                            Temporary_data.account_number=generate_account_number();
                                            Temporary_data.access=0;
                                            await Navigation.PushAsync(new Page_verification_code());
                                        } else {
                                            await DisplayAlert("Advertencia","Este correo ya esta en uso","OK");
                                        }
                                    } else {
                                        await DisplayAlert("Advertencia","Revisa bien tu contraseña que ingresaste","OK");
                                    }
                                } else {
                                    await DisplayAlert("Advertencia","Revisa bien tu correo electronico que ingresaste","OK");
                                }
                            } else {
                                await DisplayAlert("Advertencia","Revisa bien tu dni que ingresaste","OK");
                            }
                        } else {
                            await DisplayAlert("Advertencia","Revisa bien tu fecha de nacimiento, se permiite solo personas que hallan nacido en 2010 o antes","OK");
                        }
                    }else{
                        await DisplayAlert("Advertencia","Apellidos inválido. Por favor, introduce un apellido que tenga al menos 3 caracteres y esté compuesto solo por letras. Se permiten un espacio entre apellidos adicionales. Ejemplo: Matute Aguilar","OK");
                    }
                }else{
                    await DisplayAlert("Advertencia","Nombre inválido. Por favor, introduce un nombre que tenga al menos 3 caracteres y esté compuesto solo por letras. Se permiten un espacio entre nombres adicionales. Ejemplo: Juan Ramón Ariel","OK");
                }
            }
        }

        private string get_date() {
            return dp_birthdate.Date.Year+"-"+dp_birthdate.Date.Month+"-"+dp_birthdate.Date.Day;
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

        private bool empty() {
            return string.IsNullOrEmpty(txt_names.Text)||string.IsNullOrEmpty(txt_surnames.Text)||string.IsNullOrEmpty(txt_dni.Text)||string.IsNullOrEmpty(txt_email.Text)||string.IsNullOrEmpty(txt_password.Text);
        }

        private string generate_account_number() {
            Random random = new Random();

            return random.Next(10000,100000).ToString()+get_second();
        }

        private int get_second() {
            return DateTime.Now.Second;
        }

        private string generate_user() {
            return txt_names.Text.Substring(0,3)+txt_surnames.Text.Substring(0,2)+get_second()+DateTime.Now.Day;
        }
    }
}
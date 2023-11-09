using Final_project.Rest_api;
using Final_project.Settings;

namespace Final_project.Modals {
    public partial class Add_service_modal:ContentPage {
        int selectedIndex = 0;

        public Add_service_modal() {
            InitializeComponent();

            pk_categories_services.SelectedIndexChanged+=MiPicker_SelectedIndexChanged;
        }

        private void MiPicker_SelectedIndexChanged(object sender,EventArgs e) {
            var picker = (Picker) sender;
            selectedIndex=picker.SelectedIndex;
        }

        private async void verify(object sender,EventArgs e) {

            if(string.IsNullOrEmpty(txt_amount.Text)||string.IsNullOrEmpty(txt_name_service.Text)) {
                await DisplayAlert("Advertencia","No dejar campos vacios","OK");
            } else {
                if(Validations.validate_amount(txt_amount.Text)) {
                    if(Validations.validate_description_service(txt_name_service.Text)) {
                        if(selectedIndex!=0) {
                            await insert_service();
                        } else {
                            await DisplayAlert("Advertencia","Por favor elige una categoria","OK");
                        }
                    } else {
                        await DisplayAlert("Advertencia","error en el nombre de servicio","OK");
                    }
                } else {
                    await DisplayAlert("Advertencia","Error en el monto","OK");
                }
            }
        }

        private async Task insert_service() {
            Table_services service = new Table_services(txt_name_service.Text,selectedIndex,Convert.ToDouble(txt_amount.Text),Temporary_data.id_sender_user);
            string response = "";
            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.insert_update_async(service,Connection_bd.insert_service));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            if(response=="exitoso") {
                await DisplayAlert("Exitoso","Servicio agregado con exito","OK");
                await Navigation.PopModalAsync();
            } else {
                await DisplayAlert("Advertencia","No se inserto servicio: "+response,"OK");
            }
        }

        private async void cancel_modal(object sender,EventArgs e) {
            await Navigation.PopModalAsync();
        }
    }
}


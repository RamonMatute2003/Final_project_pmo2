using Final_project.Modals;
using Final_project.Models;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Final_project.Screens {
    public partial class Page_services:ContentPage {
        public ObservableCollection<Associations> services
        {
            get; set;
        }

        public Page_services() {
            InitializeComponent();

            services=new ObservableCollection<Associations>();
            this.BindingContext=this;
            load_services();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

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
            }
        }

        private async void load_services() {
            try {
                var services_bd = await get_data_services();

                if(services_bd==null) {
                    await DisplayAlert("Advertencia","No hay datos","OK");
                } else {
                    foreach(var data in services_bd) {
                        services.Add(data);
                    }
                }


            } catch(Exception ex) {
                await DisplayAlert("Advertencia","error: "+ex.ToString(),"OK");
            }
        }

        private async Task<List<Associations>> get_data_services() {
            Table_services data = new Table_services("",Temporary_data.id_service_category,0,0);
            string response = "";

            var loadingModal = new Loading_modal();
            await Navigation.PushModalAsync(loadingModal);

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(data,Connection_bd.select_services));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            await Navigation.PopModalAsync();

            List<Associations> list = JsonSerializer.Deserialize<List<Associations>>(response);

            if(list.Count>0) {
                return list;
            }

            return null;
        }

        private async void action(object sender,TappedEventArgs e) {
            var stackLayout = (StackLayout) sender;

            var item = (Associations) stackLayout.BindingContext;

            if(item!=null) {
                Temporary_data.id_service=item.id_service;
                Temporary_data.amount=item.amount;
                Temporary_data.service=item.service;
                Temporary_data.id_receiving_user=item.id_user;

                if(Temporary_data.id_receiving_user==Temporary_data.id_sender_user) {
                    await DisplayAlert("Advertencia","Este servicio es tuyo, no puedes pagarlo","OK");
                } else {
                    await Navigation.PushModalAsync(new Pay_service_modal());
                }
            }
        }
    }
}
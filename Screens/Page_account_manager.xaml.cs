using Final_project.Modals;
using Final_project.Models;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;

namespace Final_project.Screens {
    public partial class Page_account_manager:ContentPage {
        public ObservableCollection<Table_join_data> Items{
            get; set;
        }

        public ObservableCollection<Table_join_data> records
        {
            get; set;
        }

        public Page_account_manager() {
            InitializeComponent();

            records=new ObservableCollection<Table_join_data>();
            Items=new ObservableCollection<Table_join_data>();
            
            this.BindingContext=this;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            records.Clear();
            Items.Clear();

            await load_records();
            await load_record_date();
            await select_user();
            
            lbl_name.Text="Cuenta personal "+Temporary_data.names+" "+Temporary_data.surnames;
            lbl_account_number.Text="Cuenta bancaria: "+Temporary_data.account_number;
            string formatoMoneda = Temporary_data.amount.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));
            lbl_amount.Text=formatoMoneda;
        }

        private async Task load_records(){
            try {
                var services_bd = await get_data_records();

                if(services_bd!=null) {
                    foreach(var data in services_bd) {
                        records.Add(data);
                    }
                }

            } catch(Exception ex) {
                await DisplayAlert("Advertencia","error: "+ex.ToString(),"OK");
            }
        }

        private async Task load_record_date() {
            try {
                var services_bd = await get_date_records();

                if(services_bd!=null) {
                    foreach(var data in services_bd) {
                        Items.Add(data);
                    }
                }
                
            } catch(Exception ex) {
                await DisplayAlert("Advertencia","error: "+ex.ToString(),"OK");
            }
        }

        private async Task<List<Table_join_data>> get_data_records() {
            Table_users data = new Table_users("","","","","","","",0,"",0,Temporary_data.id_sender_user);
            string response = "";

            try {
                Methods insert = new Methods();
                response=await Task.Run(() => insert.select_async(data,Connection_bd.select_records_data));
            } catch(Exception ex) {
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            if(response!=""&&response!=null) {
                List<Table_join_data> list = JsonSerializer.Deserialize<List<Table_join_data>>(response);

                if(list.Count>0) {
                    return list;
                }
            }
            
            return null;
        }

        private async Task<List<Table_join_data>> get_date_records(){
            Table_records data = new Table_records(Temporary_data.id_sender_user,0,0,"",dp_date.Date.Year+"-"+dp_date.Date.Month+"-"+dp_date.Date.Day,"",0);
            string response = "";

            var loadingModal = new Loading_modal();

            try{
                Methods insert=new Methods();
                response=await Task.Run(() => insert.select_async(data,Connection_bd.select_records_date));
            }catch(Exception ex){
                await DisplayAlert("Advertencia",""+ex,"OK");
            }

            if(response!="" && response!=null){
                List<Table_join_data> list = JsonSerializer.Deserialize<List<Table_join_data>>(response);
                
                if(list.Count>0) {
                    return list;
                }
            }

            return null;
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

        private async void dp_date_DateSelected(object sender,DateChangedEventArgs e){
            Items.Clear();
            await load_record_date();
        }
    }
}


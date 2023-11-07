using Final_project.Models;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.Json;

namespace Final_project.Screens {
    public partial class Page_account_manager:ContentPage {
        public ObservableCollection<Historic> Items
        {
            get; set;
        }

        public Page_account_manager() {
            InitializeComponent();

            Items=new ObservableCollection<Historic>{
            new Historic { image = "listo.svg", name = "Nombre 1", money = "$100", code = "345", date = "2032/12/11" },
            };

            this.BindingContext=this;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            await select_user();
            lbl_name.Text="Cuenta personal "+Temporary_data.names+" "+Temporary_data.surnames;
            lbl_account_number.Text="Cuenta bancaria: "+Temporary_data.account_number;
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
    }
}


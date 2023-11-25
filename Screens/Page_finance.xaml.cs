using Final_project.Models;
using Final_project.Rest_api;
using Final_project.Settings;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Final_project.Screens;

public partial class Page_finance:ContentPage {

    public ObservableCollection<Expense_chart> expense_chart{
        get; set;
    }

    public ObservableCollection<Expense_chart> income_chart
    {
        get; set;
    }

    public Page_finance() {
        InitializeComponent();
        expense_chart=new ObservableCollection<Expense_chart>();
        income_chart=new ObservableCollection<Expense_chart>();
        this.BindingContext=this;
    }

    protected override async void OnAppearing(){
        base.OnAppearing();
        expense_chart.Clear();
        income_chart.Clear();
        await load_record_date();
        await load_record_date1();
    }

    private async Task load_record_date() {
        try {
            var services_bd = await get_data();

            if(services_bd!=null) {
                foreach(var data in services_bd) {
                    expense_chart.Add(data);
                }
            }

        } catch(Exception ex) {
            await DisplayAlert("Advertencia","error: "+ex.ToString(),"OK");
        }
    }

    private async Task<List<Expense_chart>> get_data() {
        Table_users data = new Table_users("","","","","","","",0,"",0,Temporary_data.id_sender_user);
        string response = "";

        try {
            Methods insert = new Methods();
            response=await Task.Run(() => insert.select_async(data,Connection_bd.select_expense_chart));
        } catch(Exception ex) {
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        if(response!=""&&response!=null) {
            List<Expense_chart> list = JsonSerializer.Deserialize<List<Expense_chart>>(response);

            if(list.Count>0) {
                return list;
            }
        }

        return null;
    }

    private async Task load_record_date1() {
        try {
            var services_bd = await get_data1();

            if(services_bd!=null) {
                foreach(var data in services_bd) {
                    income_chart.Add(data);
                }
            }

        } catch(Exception ex) {
            await DisplayAlert("Advertencia","error: "+ex.ToString(),"OK");
        }
    }

    private async Task<List<Expense_chart>> get_data1() {
        Table_users data = new Table_users("","","","","","","",0,"",0,Temporary_data.id_sender_user);
        string response = "";

        try{
            Methods insert=new Methods();
            response=await Task.Run(() => insert.select_async(data,Connection_bd.select_income_chart));
        }catch(Exception ex){
            await DisplayAlert("Advertencia",""+ex,"OK");
        }

        if(response!=""&&response!=null) {
            List<Expense_chart> list = JsonSerializer.Deserialize<List<Expense_chart>>(response);

            if(list.Count>0) {
                return list;
            }
        }

        return null;
    }
}
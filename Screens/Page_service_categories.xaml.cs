using Final_project.Modals;
using Final_project.Settings;

namespace Final_project.Screens;

public partial class Page_service_categories:ContentPage {
    public Page_service_categories() {
        InitializeComponent();
    }

    protected override async void OnAppearing() {
        base.OnAppearing();

        /*if(Temporary_data.code_record!=null && Temporary_data.code_record!=""){
            await Navigation.PushAsync(new Page_transfer_or_payment_result());
        }*/
    }

    private void link_associations(object sender,TappedEventArgs e) {
        load_services(1);
    }

    private void link_car(object sender,TappedEventArgs e) {
        load_services(2);
    }

    private void link_fees_credits(object sender,TappedEventArgs e) {
        load_services(3);
    }

    private void link_educational_institutions(object sender,TappedEventArgs e) {
        load_services(4);
    }

    private void link_funeral_homes(object sender,TappedEventArgs e) {
        load_services(5);
    }

    private void link_public_services(object sender,TappedEventArgs e) {
        load_services(6);
    }

    private void link_wifi(object sender,TappedEventArgs e) {
        load_services(7);
    }

    private void link_others(object sender,TappedEventArgs e) {
        load_services(8);
    }

    private void load_services(int id) {
        Temporary_data.id_service_category=id;
        Navigation.PushAsync(new Page_services());
    }

    private void add_service(object sender,EventArgs e) {
        Navigation.PushModalAsync(new Add_service_modal());
    }
}
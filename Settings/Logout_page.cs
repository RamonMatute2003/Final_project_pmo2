using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Settings {
    public class Logout_page:ContentPage{
        protected override void OnAppearing() {
            base.OnAppearing();


            //time list time 0:11:11
            Application.Current.MainPage=new NavigationPage(new MainPage());
        }
    }
}

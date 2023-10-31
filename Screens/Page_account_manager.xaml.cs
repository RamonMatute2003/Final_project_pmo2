using Final_project.Models;
using System.Collections.ObjectModel;

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
    }
}


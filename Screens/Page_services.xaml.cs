using Final_project.Models;
using System.Collections.ObjectModel;

namespace Final_project.Screens;

public partial class Page_services:ContentPage {
    public ObservableCollection<Associations> association
    {
        get; set;
    }

    public Page_services() {
        InitializeComponent();

        association=new ObservableCollection<Associations>{
            new Associations { name = "Nombre 1"},
            new Associations { name = "Nombre 2"},
            new Associations { name = "Nombre 3"},
            new Associations { name = "Nombre 4"},
            };

        this.BindingContext=this;
    }
}
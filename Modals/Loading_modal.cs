using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Modals
{
    public class Loading_modal : ContentPage
    {
        public Loading_modal()
        {
            var contentLayout = new StackLayout
            {
                WidthRequest = 200,
                HeightRequest = 200,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(255, 255, 255),
                Padding = 20,
                Children =
                {
                    new ActivityIndicator { IsRunning = true },
                    new Label { Text = "Cargando...", VerticalOptions = LayoutOptions.CenterAndExpand }
                }
            };

            BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);

            Content = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Content = contentLayout,
                CornerRadius = 10,
                Padding = 0,
                HasShadow = true
            };
        }
    }
}

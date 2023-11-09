namespace Final_project.Modals {

    /* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-maccatalyst)'
    Antes:
        public class Loading_modal : ContentPage
        {
    Después:
        public class Loading_modal :ContentPage {
    */

    /* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-ios)'
    Antes:
        public class Loading_modal : ContentPage
        {
    Después:
        public class Loading_modal :ContentPage {
    */

    /* Cambio no fusionado mediante combinación del proyecto 'Final_project (net7.0-windows10.0.19041.0)'
    Antes:
        public class Loading_modal : ContentPage
        {
    Después:
        public class Loading_modal :ContentPage {
    */
    public class Loading_modal:ContentPage {
        public Loading_modal() {
            var contentLayout = new StackLayout {
                WidthRequest=200,
                HeightRequest=200,
                VerticalOptions=LayoutOptions.Center,
                HorizontalOptions=LayoutOptions.Center,
                BackgroundColor=Color.FromRgb(255,255,255),
                Padding=20,
                Children=
                {
                    new ActivityIndicator { IsRunning = true },
                    new Label { Text = "Cargando...", VerticalOptions = LayoutOptions.CenterAndExpand }
                }
            };

            BackgroundColor=Color.FromRgba(0,0,0,0.5);

            Content=new Frame {
                VerticalOptions=LayoutOptions.Center,
                HorizontalOptions=LayoutOptions.Center,
                Content=contentLayout,
                CornerRadius=10,
                Padding=0,
                HasShadow=true
            };
        }
    }
}

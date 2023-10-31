namespace Final_project.Settings {
    class Design {
        public static void remove_stripe() {//remove_stripe=eliminar raya
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.Entry),(handler,view) => {
#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
        }
    }
}
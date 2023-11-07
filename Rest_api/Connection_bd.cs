namespace Final_project.Rest_api {
    class Connection_bd {
        public const string url_bd = "https://phpclusters-152049-0.cloudclusters.net";
        public const string insert_user = url_bd+"/insert_user.php";
        public const string insert_service = url_bd+"/insert_service.php";
        public const string insert_record = url_bd+"/insert_record.php";

        public const string select_email = url_bd+"/select_email.php";
        public const string select_user = url_bd+"/select_user.php";
        public const string select_services = url_bd+"/select_services.php";
        public const string select_data_payment = url_bd+"/select_data_payment.php";

        public const string send_code = url_bd+"/send_code.php";

        public const string update_password = url_bd+"/update_password.php";
        public const string update_amount = url_bd+"/update_amount.php";
    }
}
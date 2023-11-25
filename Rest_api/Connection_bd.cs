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
        public const string select_validate_user = url_bd+"/select_validate_user.php";
        public const string select_user_record = url_bd+"/select_user_record.php";
        public const string select_record = url_bd+"/select_record.php";
        public const string select_records_data = url_bd+"/select_records_data.php";
        public const string select_records_date = url_bd+"/select_records_date.php";
        public const string select_expense_chart = url_bd+"/select_expense_chart.php";
        public const string select_income_chart = url_bd+"/select_income_chart.php";

        public const string send_receipt = url_bd+"/send_receipt.php";
        public const string send_email_verification = url_bd+"/send_email_verification.php";
        public const string send_code = url_bd+"/send_code.php";

        public const string update_password = url_bd+"/update_password.php";
        public const string update_amount = url_bd+"/update_amount.php";
        public const string update_email = url_bd+"/update_email.php";
    }
}
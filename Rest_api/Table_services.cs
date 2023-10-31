namespace Final_project.Rest_api {
    public class Table_services {

        public Table_services(string service,int id_service_category,double amount) {
            Table_services.service=service;
            Table_services.id_service_category=id_service_category;
            Table_services.amount=amount;
        }

        public static string service
        {
            get; set;
        }

        public static int id_service_category
        {
            get; set;
        }

        public static double amount
        {
            get; set;
        }
    }
}

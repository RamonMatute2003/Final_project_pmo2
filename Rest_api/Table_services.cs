namespace Final_project.Rest_api {
    public class Table_services {

        public Table_services(string service,int id_service_category,double pay_amount,int id_user) {
            this.service=service;
            this.id_service_category=id_service_category;
            this.pay_amount=pay_amount;
            this.id_user=id_user;
        }

        public string service
        {
            get; set;
        }

        public int id_service_category
        {
            get; set;
        }

        public double pay_amount
        {
            get; set;
        }

        public int id_user
        {
            get; set;
        }
    }
}

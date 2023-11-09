namespace Final_project.Rest_api {
    public class Table_records {

        public Table_records(int id_sender_user,int id_type,double amount,string code,string date,string description,int id_service) {
            this.id_sender_user=id_sender_user;
            this.id_type=id_type;
            this.amount=amount;
            this.code=code;
            this.date=date;
            this.description=description;
            this.id_service=id_service;
        }
        public int id_sender_user
        {
            get; set;
        }

        public int id_type
        {
            get; set;
        }

        public double amount
        {
            get; set;
        }

        public string code
        {
            get; set;
        }

        public string date
        {
            get; set;
        }

        public string description
        {
            get; set;
        }

        public int id_service
        {
            get; set;
        }
    }
}

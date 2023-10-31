namespace Final_project.Rest_api {
    public class Table_records {

        public Table_records(int id_sender_user,int id_receiving_user,int id_type,double amount,string code,string date,string description,int id_service) {
            Table_records.id_sender_user=id_sender_user;
            Table_records.id_receiving_user=id_receiving_user;
            Table_records.id_type=id_type;
            Table_records.amount=amount;
            Table_records.code=code;
            Table_records.date=date;
            Table_records.description=description;
            Table_records.id_service=id_service;
        }
        public static int id_sender_user
        {
            get; set;
        }

        public static int id_receiving_user
        {
            get; set;
        }

        public static int id_type
        {
            get; set;
        }

        public static double amount
        {
            get; set;
        }

        public static string code
        {
            get; set;
        }

        public static string date
        {
            get; set;
        }

        public static string description
        {
            get; set;
        }

        public static int id_service
        {
            get; set;
        }
    }
}

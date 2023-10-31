namespace Final_project.Rest_api {
    public class Table_users {

        public Table_users(string names,string surnames,string birthdate,string dni,string email,string password,string user,double amount,string account_number,int acces) {
            this.names=names;
            this.surnames=surnames;
            this.birthdate=birthdate;
            this.dni=dni;
            this.email=email;
            this.password=password;
            this.user=user;
            this.amount=amount;
            this.account_number=account_number;
            this.acces=acces;
        }

        public Table_users() {

        }

        public string names
        {
            get; set;
        }

        public string surnames
        {
            get; set;
        }
        public string birthdate
        {
            get; set;
        }
        public string dni
        {
            get; set;
        }
        public string email
        {
            get; set;
        }
        public string password
        {
            get; set;
        }
        public string user
        {
            get; set;
        }
        public double amount
        {
            get; set;
        }
        public string account_number
        {
            get; set;
        }
        public int acces
        {
            get; set;
        }
    }
}

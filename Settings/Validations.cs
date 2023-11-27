using System.Text.RegularExpressions;

namespace Final_project.Settings {
    public class Validations {
        public static bool validation_birthdate(int year) {//validations=validaciones
            return year>2010;
        }

        public static bool validate_name(string text) {
            var name = new Regex(@"^(?=.{3,})([A-Za-záéíóúÁÉÍÓÚñÑ]+)(?:\s[A-Za-záéíóúÁÉÍÓÚñÑ]+)?(?:\s[A-Za-záéíóúÁÉÍÓÚñÑ]+)?$");

            return name.IsMatch(text);
        }

        public static bool validate_surname(string text) {
            var surname = new Regex(@"^(?=.{3,})([A-Za-záéíóúÁÉÍÓÚñÑ]+)(?:\s[A-Za-záéíóúÁÉÍÓÚñÑ]+)?$");

            return surname.IsMatch(text);
        }

        public static bool validate_email(string text) {
            var email = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            return email.IsMatch(text);
        }

        public static bool validate_dni(string text) {
            var dni = new Regex(@"^\d{13}$");

            return dni.IsMatch(text);
        }

        public static bool validate_password(string text) {
            var password = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^+=()_-]).{8,}$");

            return password.IsMatch(text);
        }

        public static bool validate_user(string text) {
            var user = new Regex(@"^(?![-_. ])(?!.*[_. -]{2})[a-zA-Z0-9._ -]{3,20}(?<![_. ])$");

            return user.IsMatch(text);
        }

        public static bool validate_amount(string text) {
            var amount = new Regex(@"^[-]?[0-9]*(\.[0-9]{2})?$");

            return amount.IsMatch(text);
        }

        public static bool validate_account_number(string text) {
            var account_number = new Regex(@"^\d+$");

            return account_number.IsMatch(text);
        }

        public static bool validate_description_service(string text) {
            var description_service=new Regex(@"^[a-zA-Z0-9\s,.\-()!?:;'""áéíóúÁÉÍÓÚñÑ]+$");

            return description_service.IsMatch(text);
        }
    }
}

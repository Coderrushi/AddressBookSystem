using System.Text.RegularExpressions;

namespace AddressBookSystem
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public override bool Equals(object obj) =>
            obj is Contact contact && FirstName == contact.FirstName && LastName == contact.LastName;
        public override int GetHashCode() => HashCode.Combine(FirstName.ToLower(), LastName.ToLower());
        public override string ToString() => $"{FirstName} {LastName}, {PhoneNumber}, {EmailId}, {City}, {State}";
    }

    internal class UC1_AddNewContact
    {
        public static List<Contact> Contacts = new List<Contact>();
        public static void AddNewContact()
        {
            Contact newContact = new Contact();
            if (ValidateAndPrint("First name", newContact.FirstName, ValidateName) &&
            ValidateAndPrint("Last name", newContact.LastName, ValidateName) &&
            ValidateAndPrint("Phone number", newContact.PhoneNumber, ValidatePhoneNumber) &&
            ValidateAndPrint("Email Id", newContact.EmailId, ValidateEmail) &&
            ValidateAndPrint("City", newContact.City, ValidateCityOrState) &&
            ValidateAndPrint("State", newContact.State, ValidateCityOrState) &&
            ValidateAndPrint("Pincode", newContact.PinCode, ValidatePincode))
            {
                Contacts.Add(newContact);
                Console.WriteLine("Contact added successfully!");
            }
            else
            {
                Console.WriteLine("Contact could not be added due to invalid information.");
            }
        }
        public static void EditDetails()
        {
            Console.WriteLine("Enter your first name: ");
            string inFirstName = Console.ReadLine();
            Console.WriteLine("Enter your last name: ");
            string inLastName = Console.ReadLine();
            if (inFirstName == "Hrushikesh" || inLastName == "Zarekar")
            {
                Console.WriteLine("Enter 1: to edit Phonenumber:");
                Console.WriteLine("Enter 2: to edit Email:");
                Console.WriteLine("Enter 3: to edit City:");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter new Phonenumber: ");
                    string updated_phNum = Console.ReadLine();
                    Regex rePhNum = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9] {2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
                    if (rePhNum.IsMatch(updated_phNum))
                    {
                        Console.WriteLine("Phone number updated successfully!");
                        Console.WriteLine("Update phone number: {0}", updated_phNum);
                    }
                    else
                    {
                        Console.WriteLine("Sorry Could not update phone number (input is not in required format");
                    }
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter new Email: ");
                    string updated_email = Console.ReadLine();
                    Regex reEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                    if (reEmail.IsMatch(updated_email))
                    {
                        Console.WriteLine("Email Id updated successfully!");
                        Console.WriteLine("Udated Email Id: {0}", updated_email);
                    }
                    else
                    {
                        Console.WriteLine("Sorry Could not update Email Id (input is not in required format");
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter new City name: ");
                    string updated_city = Console.ReadLine();
                    Regex reCity = new Regex(@"(^[a-zA-Z\s]+$)");
                    if (reCity.IsMatch(updated_city))
                    {
                        Console.WriteLine("City name updated successfully!");
                        Console.WriteLine("Updated City name: {0}", updated_city);
                    }
                    else
                    {
                        Console.WriteLine("Sorry Could not update City name (input is not in required format");
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("Incorrect data entered..");
            }
        }
        private static bool ValidateAndPrint(string fieldName, string fieldValue, Func<string, bool> validationMethod)
        {
            if (validationMethod(fieldValue))
            {
                Console.WriteLine($"Valid {fieldName}");
                return true;
            }
            else
            {
                Console.WriteLine($"Invalid {fieldName}");
                return false;
            }
        }
        private static bool ValidateName(string name)
        {
            Regex reName = new Regex(@"(^[a-zA-Z]+$)");
            return reName.IsMatch(name);
        }

        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            Regex rePhNumber = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
            return rePhNumber.IsMatch(phoneNumber);
        }

        private static bool ValidateEmail(string email)
        {
            Regex reEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return reEmail.IsMatch(email);
        }

        private static bool ValidateCityOrState(string value)
        {
            Regex regex1 = new Regex(@"(^[a-zA-Z\s]+$)");
            return regex1.IsMatch(value);
        }

        private static bool ValidatePincode(string pinCode)
        {
            Regex regex2 = new Regex(@"(^[0-9]{6}$)");
            return regex2.IsMatch(pinCode);
        }

    }
    internal class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Address Book System");
            Contact newContact = new Contact();
            Console.WriteLine("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter your last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter your email Id: ");
            string emailId = Console.ReadLine();
            Console.WriteLine("Enter your city: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter your state: ");
            string state = Console.ReadLine();
            Console.WriteLine("Enter your pincode: ");
            string pinCode = Console.ReadLine();
            UC1_AddNewContact.AddNewContact();
        }
    }
}

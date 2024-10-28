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
    internal class AddMultipleAddressBook
    {
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public AddMultipleAddressBook(string name)
        {
            Name = name;
            Contacts = new List<Contact>();
        }
        public void EditDetails()
        {
            Console.WriteLine("Enter the first name of the contact to edit: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter the last name of the contact to edit: ");
            string lastName = Console.ReadLine();

            Contact contact = Contacts.Find(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                Console.WriteLine("Select detail to edit:");
                Console.WriteLine("1. Phone Number");
                Console.WriteLine("2. Email");
                Console.WriteLine("3. City");
                Console.WriteLine("4. Exit");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter new phone number: ");
                        string newPhone = Console.ReadLine();
                        if (Regex.IsMatch(newPhone, @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)"))
                        {
                            contact.PhoneNumber = newPhone;
                            Console.WriteLine("Phone number updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid phone number format.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter new email: ");
                        string newEmail = Console.ReadLine();
                        if (Regex.IsMatch(newEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                        {
                            contact.EmailId = newEmail;
                            Console.WriteLine("Email updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid email format.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter new city: ");
                        string newCity = Console.ReadLine();
                        if (Regex.IsMatch(newCity, @"(^[a-zA-Z\s]+$)"))
                        {
                            contact.City = newCity;
                            Console.WriteLine("City updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid city format.");
                        }
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public void DeleteContact()
        {
            Console.WriteLine("Enter the first name of the contact to delete: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter the last name of the contact to delete: ");
            string lastName = Console.ReadLine();

            Contact contact = Contacts.Find(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                                 c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                Console.WriteLine("Are you sure you want to delete this contact? (yes/no)");
                string choice = Console.ReadLine().ToLower();

                if (choice == "yes")
                {
                    Contacts.Remove(contact);
                    Console.WriteLine("Contact deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Contact deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public void DisplayContacts()
        {
            Console.WriteLine("\nContacts in Address Book '{0}':", Name);
            foreach (var contact in Contacts)
            {
                Console.WriteLine("First Name: {0}, Last Name: {1}, Phone: {2}, Email: {3}, City: {4}, State: {5}, Pincode: {6}",
                    contact.FirstName, contact.LastName, contact.PhoneNumber, contact.EmailId, contact.City, contact.State, contact.PinCode);
            }
        }
        public void NoDuplicateEntryInAddressBook()
        {
            Console.WriteLine("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter your last name: ");
            string lastName = Console.ReadLine();
            if (Contacts.Any(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                              c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("This contact already exists in the address book. Duplicate entry is not allowed");
                return;
            }
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

            Regex nameRegex = new Regex(@"(^[a-zA-Z]+$)");
            Regex phoneRegex = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
            Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            Regex cityStateRegex = new Regex(@"(^[a-zA-Z\s]+$)");
            Regex pinRegex = new Regex(@"(^[0-9]{6}$)");

            if (nameRegex.IsMatch(firstName) && nameRegex.IsMatch(lastName) && phoneRegex.IsMatch(phoneNumber) &&
            emailRegex.IsMatch(emailId) && cityStateRegex.IsMatch(city) && cityStateRegex.IsMatch(state) &&
            pinRegex.IsMatch(pinCode))
            {
                Contact newContact = new Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    EmailId = emailId,
                    City = city,
                    State = state,
                    PinCode = pinCode
                };
                Contacts.Add(newContact);
                Console.WriteLine("Contact added successfully!");
            }
            else
            {
                Console.WriteLine("Error: Invalid input format, please try again");
            }
        }
        public void AddContact(Contact contact)
        {
            if (!Contacts.Contains(contact))
            {
                Contacts.Add(contact);
                Console.WriteLine($"Contact {contact.FirstName} {contact.LastName} added successfully");
            }
            else
            {
                Console.WriteLine($"Contact {contact.FirstName} {contact.LastName} already exists");
            }
        }
        public IEnumerable<Contact> SearchByCityOrState(string location)
        {
            return Contacts.Where(c => c.City.Equals(location, StringComparison.OrdinalIgnoreCase) ||
                          c.State.Equals(location, StringComparison.OrdinalIgnoreCase));
        }
    }
    internal class AddressBookManager
    {
        private static Dictionary<string, AddMultipleAddressBook> addressBooks = new Dictionary<string, AddMultipleAddressBook>();
        public static void CreateAddressBook()
        {
            Console.WriteLine("Enter the name of the new Address Book: ");
            string bookName = Console.ReadLine();
            if (!addressBooks.ContainsKey(bookName))
            {
                AddMultipleAddressBook newBook = new AddMultipleAddressBook(bookName);
                addressBooks.Add(bookName, newBook);
                Console.WriteLine("Address Book '{0}' created successfully!", bookName);
            }
            else
            {
                Console.WriteLine("An address book with this name is already exists, please try differnet name");
            }
        }
        public static void AddContactToAddressBook(string bookName)
        {
            if (addressBooks.ContainsKey(bookName))
            {
                Console.WriteLine("Adding a contact to Addess Book '{0}'", bookName);
                Console.WriteLine("Enter your first name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter your last name: ");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter your phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Enter your email Id: ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter your city: ");
                string city = Console.ReadLine();
                Console.WriteLine("Enter your state: ");
                string state = Console.ReadLine();
                Console.WriteLine("Enter your pincode: ");
                string pinCode = Console.ReadLine();
                Regex nameRegex = new Regex(@"(^[a-zA-Z]+$)");
                Regex phoneRegex = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
                Regex emailRegex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                Regex cityStateRegex = new Regex(@"(^[a-zA-Z\s]+$)");
                Regex pinRegex = new Regex(@"(^[0-9]{6}$)");
                if (nameRegex.IsMatch(firstName) && nameRegex.IsMatch(lastName) && phoneRegex.IsMatch(phoneNumber)
                    && emailRegex.IsMatch(email) && cityStateRegex.IsMatch(city) && cityStateRegex.IsMatch(state) && pinRegex.IsMatch(pinCode))
                {
                    Contact newContact = new Contact()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        EmailId = email,
                        City = city,
                        State = state,
                        PinCode = pinCode
                    };
                    addressBooks[bookName].AddContact(newContact);
                }
                else
                {
                    Console.WriteLine("Entered information is not in required format**");
                }
            }
            else
            {
                Console.WriteLine("No address book found with the name '{0}'", bookName);
            }
        }
        public static void EditContactInAddressBook()
        {
            Console.WriteLine("Enter the name of the Address Book to edit a contact: ");
            string addressBookName = Console.ReadLine();

            if (addressBooks.ContainsKey(addressBookName))
            {
                addressBooks[addressBookName].EditDetails();
            }
            else
            {
                Console.WriteLine("Address Book '{0}' does not exist.", addressBookName);
            }
        }
        public static void DeleteContactInAddressBook()
        {
            Console.WriteLine("Enter the name of the Address Book to delete a contact from: ");
            string addressBookName = Console.ReadLine();

            if (addressBooks.ContainsKey(addressBookName))
            {
                addressBooks[addressBookName].DeleteContact();
            }
            else
            {
                Console.WriteLine("Address Book '{0}' does not exist.", addressBookName);
            }
        }
        public static void DisplayAddressBooks()
        {
            Console.WriteLine("\nAvailable Address Books:");
            foreach (var bookName in addressBooks.Keys)
            {
                Console.WriteLine("Address Book: " + bookName);
            }
        }
        public static void DisplayContactsInAddressBook(string bookName)
        {
            Console.WriteLine("Enter the name of the Address Book to display contacts: ");
            string addressBookName = Console.ReadLine();

            if (addressBooks.ContainsKey(addressBookName))
            {
                addressBooks[addressBookName].DisplayContacts();
            }
            else
            {
                Console.WriteLine("Address Book '{0}' does not exist.", addressBookName);
            }
        }
        public void AddAddressBook(string name, AddMultipleAddressBook addressBook)
        {
            if (!addressBooks.ContainsKey(name))
            {
                addressBooks[name] = addressBook;
                Console.WriteLine($"AddressBook '{name}' added successfully");
            }
            else
            {
                Console.WriteLine($"Address Book '{name}' already exists");
            }
        }
        public static void SearchAcrossaddressBooks(string location)
        {
            Console.WriteLine($"Searching for contacts in '{location}' across all address books: ");
            foreach (var addressBookEntry in addressBooks)
            {
                var results = addressBookEntry.Value.SearchByCityOrState(location).ToList();
                if (results.Any())
                {
                    Console.WriteLine($"\nAddress Book: {addressBookEntry.Key}");
                    foreach (var contact in results)
                    {
                        Console.WriteLine(contact);
                    }
                }
            }
        }
    }

    internal class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Address Book System");
            while (true)
            {
                Console.WriteLine("1: Create new address book");
                Console.WriteLine("2: Add contact to address book");
                Console.WriteLine("3: Edit contact in address book");
                Console.WriteLine("4: Delete conatct from address book");
                Console.WriteLine("5: Display address books");
                Console.WriteLine("6: Display contacts in address book");
                Console.WriteLine("7: Search contacts by city or state");
                Console.WriteLine("8: Exit");
                Console.WriteLine("Enter your choice");
                int choice = int.Parse(Console.ReadLine()); 
                switch(choice)
                {
                    case 1:
                        AddressBookManager.CreateAddressBook();
                        break;
                    case 2:
                        AddressBookManager.AddContactToAddressBook("Family");
                        break;
                    case 3:
                        AddressBookManager.EditContactInAddressBook();
                        break;
                    case 4:
                        AddressBookManager.DeleteContactInAddressBook();
                        break;
                    case 5:
                        AddressBookManager.DisplayAddressBooks();
                        break;
                    case 6:
                        AddressBookManager.DisplayContactsInAddressBook("Family");
                        break;
                    case 7:
                        Console.WriteLine("\nEnter a city or state to search across all address books:");
                        string location = Console.ReadLine();
                        AddressBookManager.SearchAcrossaddressBooks(location);
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Enter valid input");
                        break;
                }

            }
            
        }
    }
}

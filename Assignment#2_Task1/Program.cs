using System;
using System.Collections.Generic;

namespace ElectricianApp
{
    // Customer class represents one job/customer.
    // Class names and method names use Pascal Case.
    public class Customer
    {
        // Properties for the customer's job information.
        public string BuildingType { get; set; }      // "House", "Barn", or "Garage"
        public int Size { get; set; }                 // Square feet (between 1000 and 50000)
        public int NumberOfBulbs { get; set; }        // Up to 20 bulbs
        public int NumberOfOutlets { get; set; }      // Up to 50 outlets
        public string CreditCardNumber { get; set; }  // 16-digit credit card number

        // Constructor that initializes a Customer instance.
        public Customer(string buildingType, int size, int numberOfBulbs, int numberOfOutlets, string creditCardNumber)
        {
            BuildingType = buildingType;
            Size = size;
            NumberOfBulbs = numberOfBulbs;
            NumberOfOutlets = numberOfOutlets;
            CreditCardNumber = creditCardNumber;
        }

        // Method to mask the credit card number as "first4 XXXX XXXX last4"
        public string GetMaskedCreditCard()
        {
            // Assuming creditCardNumber is exactly 16 characters long.
            return CreditCardNumber.Substring(0, 4) + " XXXX XXXX " + CreditCardNumber.Substring(12, 4);
        }

        // Common task: Create wiring schema.
        public void CreateWiringSchema()
        {
            Console.WriteLine("Creating wiring schema for " + BuildingType + "...");
        }

        // Common task: Purchase necessary parts.
        public void PurchaseParts()
        {
            Console.WriteLine("Purchasing parts for " + BuildingType + " project...");
        }

        // Specialized task based on building type.
        public void PerformSpecialTask()
        {
            // Depending on the type of building, call the appropriate method.
            if (BuildingType.Equals("House", StringComparison.OrdinalIgnoreCase))
            {
                InstallFireAlarms();
            }
            else if (BuildingType.Equals("Barn", StringComparison.OrdinalIgnoreCase))
            {
                WireMilkingEquipment();
            }
            else if (BuildingType.Equals("Garage", StringComparison.OrdinalIgnoreCase))
            {
                InstallAutomaticDoors();
            }
            else
            {
                Console.WriteLine("No special task for this building type.");
            }
        }

        // House-specific: Install fire alarms.
        public void InstallFireAlarms()
        {
            Console.WriteLine("Installing fire alarms in the house...");
        }

        // Barn-specific: Wire milking equipment.
        public void WireMilkingEquipment()
        {
            Console.WriteLine("Wiring milking equipment in the barn...");
        }

        // Garage-specific: Install automatic doors.
        public void InstallAutomaticDoors()
        {
            Console.WriteLine("Installing automatic doors in the garage...");
        }

        // Returns a one-line string with all customer information.
        public string GetCustomerSummary()
        {
            return $"Building: {BuildingType}, Size: {Size} sq.ft, Bulbs: {NumberOfBulbs}, Outlets: {NumberOfOutlets}, Credit Card: {GetMaskedCreditCard()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List to store all customer objects.
            List<Customer> customerList = new List<Customer>();

            Console.WriteLine("Electrician Job Scheduler");
            Console.WriteLine("Enter customer details. Type 'exit' when done.");

            while (true)
            {
                // Prompt for building type.
                Console.Write("Enter Building Type (House/Barn/Garage) or 'exit' to finish: ");
                string buildingType = Console.ReadLine();
                if (buildingType.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Prompt for building size.
                int size;
                while (true)
                {
                    Console.Write("Enter size in sq.ft (between 1000 and 50000): ");
                    string sizeInput = Console.ReadLine();
                    if (int.TryParse(sizeInput, out size) && size >= 1000 && size <= 50000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid size. Please enter a value between 1000 and 50000.");
                    }
                }

                // Prompt for number of bulbs.
                int numberOfBulbs;
                while (true)
                {
                    Console.Write("Enter number of light bulbs (up to 20): ");
                    string bulbsInput = Console.ReadLine();
                    if (int.TryParse(bulbsInput, out numberOfBulbs) && numberOfBulbs >= 0 && numberOfBulbs <= 20)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of bulbs. Please enter a number between 0 and 20.");
                    }
                }

                // Prompt for number of outlets.
                int numberOfOutlets;
                while (true)
                {
                    Console.Write("Enter number of outlets (up to 50): ");
                    string outletsInput = Console.ReadLine();
                    if (int.TryParse(outletsInput, out numberOfOutlets) && numberOfOutlets >= 0 && numberOfOutlets <= 50)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of outlets. Please enter a number between 0 and 50.");
                    }
                }

                // Prompt for credit card number.
                string creditCardNumber;
                while (true)
                {
                    Console.Write("Enter 16-digit credit card number: ");
                    creditCardNumber = Console.ReadLine();
                    // Validate that input is exactly 16 digits.
                    if (creditCardNumber.Length == 16 && long.TryParse(creditCardNumber, out _))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid credit card number. Please enter exactly 16 digits.");
                    }
                }

                // Create a new customer using Camel Case for the variable name.
                Customer newCustomer = new Customer(buildingType, size, numberOfBulbs, numberOfOutlets, creditCardNumber);
                customerList.Add(newCustomer);
                Console.WriteLine("Customer added.\n");
            }

            // Display information for each customer and call all methods.
            Console.WriteLine("\n----- Customer Schedule -----");
            foreach (Customer customer in customerList)
            {
                // Call common tasks and specialized task.
                customer.CreateWiringSchema();
                customer.PurchaseParts();
                customer.PerformSpecialTask();

                // Display the summary in one line.
                Console.WriteLine(customer.GetCustomerSummary());
                Console.WriteLine(); // Blank line for readability.
            }

            Console.WriteLine("Schedule complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}


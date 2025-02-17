using System;
using System.Collections.Generic;

namespace ElectricianJobScheduler
{
    // Interface that declares the functionality for any electrician job.
    public interface IElectricianJob
    {
        // Properties for the job details.
        string BuildingType { get; set; }
        int Size { get; set; }
        int NumberOfBulbs { get; set; }
        int NumberOfOutlets { get; set; }
        string CreditCardNumber { get; set; }

        // Methods to be implemented.
        string GetMaskedCreditCard();
        void CreateWiringSchema();
        void PurchaseParts();
        void PerformSupplementaryTask(); // Building-specific task.
        string GetJobSummary();
    }

    // Abstract base class implementing IElectricianJob.
    // Common functionality for all building jobs is provided here.
    public abstract class BuildingJob : IElectricianJob
    {
        // Properties (using Pascal Case).
        public string BuildingType { get; set; }
        public int Size { get; set; }
        public int NumberOfBulbs { get; set; }
        public int NumberOfOutlets { get; set; }
        public string CreditCardNumber { get; set; }

        // Constructor initializes common properties.
        public BuildingJob(string buildingType, int size, int numberOfBulbs, int numberOfOutlets, string creditCardNumber)
        {
            BuildingType = buildingType;
            Size = size;
            NumberOfBulbs = numberOfBulbs;
            NumberOfOutlets = numberOfOutlets;
            CreditCardNumber = creditCardNumber;
        }

        // Method to mask credit card number in the form "4511 XXXX XXXX 1111".
        public string GetMaskedCreditCard()
        {
            // Assumes CreditCardNumber is exactly 16 digits.
            return CreditCardNumber.Substring(0, 4) + " XXXX XXXX " + CreditCardNumber.Substring(12, 4);
        }

        // Common task: create wiring schema.
        // This method can be overridden by derived classes if needed.
        public virtual void CreateWiringSchema()
        {
            Console.WriteLine("Creating wiring schema for " + BuildingType + "...");
        }

        // Overloaded version with a flag for a detailed plan.
        public virtual void CreateWiringSchema(bool includeDetailedPlan)
        {
            if (includeDetailedPlan)
            {
                Console.WriteLine("Creating a detailed wiring schema for " + BuildingType + "...");
            }
            else
            {
                CreateWiringSchema();
            }
        }

        // Common task: purchase necessary parts.
        public virtual void PurchaseParts()
        {
            Console.WriteLine("Purchasing parts for " + BuildingType + " project...");
        }

        // Abstract method for the building-specific supplementary task.
        // Must be overridden in derived classes.
        public abstract void PerformSupplementaryTask();

        // Returns a one-line summary of the job.
        public virtual string GetJobSummary()
        {
            return $"Building: {BuildingType}, Size: {Size} sq.ft, Bulbs: {NumberOfBulbs}, Outlets: {NumberOfOutlets}, Credit Card: {GetMaskedCreditCard()}";
        }
    }

    // Derived class for House jobs.
    public class HouseJob : BuildingJob
    {
        public HouseJob(int size, int numberOfBulbs, int numberOfOutlets, string creditCardNumber)
            : base("House", size, numberOfBulbs, numberOfOutlets, creditCardNumber)
        {
        }

        // Override the supplementary task: install fire alarms.
        public override void PerformSupplementaryTask()
        {
            Console.WriteLine("Installing fire alarms in the house...");
        }

        // Example of overriding a common method if needed.
        public override string GetJobSummary()
        {
            // You could add house-specific information here.
            return base.GetJobSummary() + " (House specific task: Fire Alarms installed)";
        }
    }

    // Derived class for Barn jobs.
    public class BarnJob : BuildingJob
    {
        public BarnJob(int size, int numberOfBulbs, int numberOfOutlets, string creditCardNumber)
            : base("Barn", size, numberOfBulbs, numberOfOutlets, creditCardNumber)
        {
        }

        // Override the supplementary task: wire milking equipment.
        public override void PerformSupplementaryTask()
        {
            Console.WriteLine("Wiring milking equipment in the barn...");
        }

        public override string GetJobSummary()
        {
            return base.GetJobSummary() + " (Barn specific task: Milking Equipment wired)";
        }
    }

    // Derived class for Garage jobs.
    public class GarageJob : BuildingJob
    {
        public GarageJob(int size, int numberOfBulbs, int numberOfOutlets, string creditCardNumber)
            : base("Garage", size, numberOfBulbs, numberOfOutlets, creditCardNumber)
        {
        }

        // Override the supplementary task: install automatic doors.
        public override void PerformSupplementaryTask()
        {
            Console.WriteLine("Installing automatic doors in the garage...");
        }

        public override string GetJobSummary()
        {
            return base.GetJobSummary() + " (Garage specific task: Automatic Doors installed)";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List to store jobs. Using the interface type allows storage of any derived BuildingJob.
            List<IElectricianJob> jobList = new List<IElectricianJob>();

            Console.WriteLine("Electrician Job Scheduler with Abstract Classes and Interfaces");
            Console.WriteLine("Enter job details. Type 'exit' for Building Type to finish.\n");

            while (true)
            {
                // Get building type.
                Console.Write("Enter Building Type (House/Barn/Garage) or 'exit' to finish: ");
                string buildingTypeInput = Console.ReadLine();
                if (buildingTypeInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Validate building type.
                if (!buildingTypeInput.Equals("House", StringComparison.OrdinalIgnoreCase) &&
                    !buildingTypeInput.Equals("Barn", StringComparison.OrdinalIgnoreCase) &&
                    !buildingTypeInput.Equals("Garage", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Invalid building type. Please enter House, Barn, or Garage.");
                    continue;
                }

                // Prompt for size.
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
                        Console.WriteLine("Invalid number. Please enter a value between 0 and 20.");
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
                        Console.WriteLine("Invalid number. Please enter a value between 0 and 50.");
                    }
                }

                // Prompt for credit card number.
                string creditCardNumber;
                while (true)
                {
                    Console.Write("Enter 16-digit credit card number: ");
                    creditCardNumber = Console.ReadLine();
                    if (creditCardNumber.Length == 16 && long.TryParse(creditCardNumber, out _))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid credit card number. Please enter exactly 16 digits.");
                    }
                }

                // Create the appropriate job based on the building type.
                IElectricianJob job = null;
                if (buildingTypeInput.Equals("House", StringComparison.OrdinalIgnoreCase))
                {
                    job = new HouseJob(size, numberOfBulbs, numberOfOutlets, creditCardNumber);
                }
                else if (buildingTypeInput.Equals("Barn", StringComparison.OrdinalIgnoreCase))
                {
                    job = new BarnJob(size, numberOfBulbs, numberOfOutlets, creditCardNumber);
                }
                else if (buildingTypeInput.Equals("Garage", StringComparison.OrdinalIgnoreCase))
                {
                    job = new GarageJob(size, numberOfBulbs, numberOfOutlets, creditCardNumber);
                }

                // Add the job to the list.
                jobList.Add(job);
                Console.WriteLine("Job added successfully.\n");
            }

            // Process each job: call common tasks and the specialized task.
            Console.WriteLine("\n----- Job Schedule -----");
            foreach (IElectricianJob job in jobList)
            {
                // Call common tasks.
                job.CreateWiringSchema();
                job.PurchaseParts();
                // Call the overloaded method (example with detailed plan set to false).
                // job.CreateWiringSchema(false);

                // Call the building-specific supplementary task.
                job.PerformSupplementaryTask();

                // Display the one-line summary.
                Console.WriteLine(job.GetJobSummary());
                Console.WriteLine(); // Blank line for clarity.
            }

            Console.WriteLine("Job schedule complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}


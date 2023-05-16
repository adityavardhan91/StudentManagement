using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Services;

namespace StudentManagement
{
    public class Student
    {
        public void SearchStudent()
        {
            Console.WriteLine("Choose the search type");
            Console.WriteLine("\n1.Search by Name\n2.Search by Id\n3.Search by Phone number");
            int searchPick = int.Parse(Console.ReadLine());
            switch (searchPick)
            {
                case 1:
                    {
                        Console.WriteLine("Enter the Student Name to search:");
                        string SearchTerm = Console.ReadLine();
                        SearchTerm = SearchTerm.ToUpper();
                        SearchFor(SearchTerm, 1);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter the Student Id to search:");
                        string SearchTerm = Console.ReadLine();
                        
                        SearchFor(SearchTerm, 0);
                        break;

                    }
                case 3:
                    {
                        Console.WriteLine("Enter the Phone no: ");
                        string SearchTerm = Console.ReadLine();
                        SearchFor(SearchTerm, 3);
                        break;

                    }
                default:
                    {
                        Console.WriteLine("Select a proper option");
                        break;
                    }
            }
        }
        public void SearchFor(string search, int index)
        {
            using (StreamReader reader = new StreamReader("students.txt"))
            {
                string line;
                bool StudentFound = false;

                // Read each line in the file
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into its fields
                    string[] fields = line.Split(',');

                    // Check if the ID matches the search ID
                    if (fields[index] == search)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("Student ID: " + fields[0]);
                        Console.WriteLine("Student Name: " + fields[1]);
                        Console.WriteLine("Date of Birth: " + fields[2]);
                        Console.WriteLine("Phone no: " + fields[3]);
                        Console.WriteLine("Gender:" + fields[4]);
                        Console.WriteLine("Balance fee:" + fields[5]);
                        StudentFound = true;
                    }
                    break;
                }
                if (StudentFound == false)
                {
                    Console.WriteLine("Student not found");
                }
            }
        }
        public void AddStudent()
        {
            Console.WriteLine("Student registration\n---------------------");
            Console.WriteLine("Enter student Full name:");
            string fName = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter Date of Birth (MM/DD/YYYY):");
            string input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime date))
            {
                Console.WriteLine("You entered: " + date.ToString("MM/dd/yyyy"));
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
            Console.WriteLine("Enter Gender");
            string gender = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Phone number");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter Address");
            string address = Console.ReadLine();

            string fileName = "students.txt";
            int id = 0;

            if (File.Exists(fileName))
            {
                bool CheckId = CheckForAccount(phone);

                //if account not exists..
                if (!CheckId)
                {



                    // read the last ID from the file
                    string lastLine = File.ReadLines(fileName).LastOrDefault();
                    if (!string.IsNullOrEmpty(lastLine))
                    {
                        int.TryParse(lastLine.Split(',')[0], out id);
                    }


                    Console.WriteLine("Enter user [y] to contiue or [n] to exit:");
                    while (true)
                    {
                        string YesorNo = Console.ReadLine();
                        if (YesorNo.Equals("n", StringComparison.OrdinalIgnoreCase))
                        {
                           
                            break;
                        }
                        

                        // increment the ID and write the new user to the file
                        id++;
                        string user = $"{id.ToString()},{fName},{date},{phone},{gender},{address},{25000}";
                        File.AppendAllText(fileName, $"{user}{Environment.NewLine}");
                        Console.WriteLine($"{fName} is added succesfully!");
                        Console.WriteLine("Press \"n\" to exit");
                    }
                }
                else
                {
                    Console.WriteLine("User is already existeed!");
                }
            }
        }

        public bool CheckForAccount(string userPhone)
        {


            string[] userDetails = File.ReadAllLines("students.txt");

            bool accountExists = false;
            foreach (string line in userDetails)
            {
                List<string> userAccount = new List<string>(line.Split(','));

                if (userAccount[3] == userPhone)
                {
                    accountExists = true;
                    break;
                }
            }
            return accountExists;
        }

        public void CheckStudentBalance()
        {
            Console.WriteLine("Enter student id");
            string search = Console.ReadLine();
            using (StreamReader reader = new StreamReader("students.txt"))
            {
                string line;
                bool StudentFound = false;

                // Read each line in the file
                while ((line = reader.ReadLine()) != null)
                {
                    // Split the line into its fields
                    string[] fields = line.Split(',');

                    // Check if the ID matches the search ID

                    /*       Console.WriteLine(fields[index]);
                           Console.WriteLine(search);*/

                    if (fields[0] == search)
                    {
                        if (int.Parse(fields[6])!= 0)
                        {
                            /*                      Console.WriteLine("\n");
                                                    Console.WriteLine("Student ID: " + fields[0]);
                                                    Console.WriteLine("Student Name: " + fields[1]);
                                                    Console.WriteLine("Date of Birth: " + fields[2]);
                                                    Console.WriteLine("Phone no: " + fields[3]);*/
                            Console.WriteLine($"Balanbce fee of {fields[1]} is {fields[6]}");

                            StudentFound = true;
                        }
                        else
                        {
                            Console.WriteLine($"{fields[1]} fees has been paid!");
                        }

                    }
                    break;
                }


                if (StudentFound == false)
                {
                    Console.WriteLine("Student not found");
                }
            }

            }

        public void PayFee()
        {

  /*          Console.WriteLine("Enter student id");
            string search = Console.ReadLine();

            Console.WriteLine("Enter fee amount to pay");
            int PendingFee = int.Parse(Console.ReadLine());*/

            /*   string[] studentDetails = File.ReadAllLines("students.txt");*/

            if (File.Exists("students.txt"))
            {
                string[] bankDetails = File.ReadAllLines("students.txt");
                Console.WriteLine("Enter student Id number");
                string StudentId = Console.ReadLine();

/*                Console.WriteLine("Enter fee amount");
                int Pin = int.Parse(Console.ReadLine());*/


                int recordIndex = -1;
                bool recordFound = false;
                string[] withdrawBankAccount = null;


                foreach (string line in bankDetails)
                {
                    recordIndex++;
                    withdrawBankAccount = line.Split(',');
                    if (withdrawBankAccount[0] == StudentId && recordFound == false)
                    {
                        Console.WriteLine("Enter amount");
                        string amountWithdraw = Console.ReadLine();

                        recordFound = true;
                        if(int.Parse(amountWithdraw) <= int.Parse(withdrawBankAccount[6]))
                        {
                            int balance = int.Parse(withdrawBankAccount[6]);
                            balance -= int.Parse(amountWithdraw);
                            withdrawBankAccount[6] = balance.ToString();

                            if (recordFound && withdrawBankAccount != null)
                            {
                                bankDetails[recordIndex] = string.Join(",", withdrawBankAccount);
                            }
                            File.WriteAllLines("students.txt", bankDetails);
                            Console.WriteLine($"\nyour fee balance is : {withdrawBankAccount[6]}");
                        }
                        else
                        {
                            Console.WriteLine("Enter a valid amount");
                        }
                       

                     

                    }
                }

               
            }
        }
    }
}

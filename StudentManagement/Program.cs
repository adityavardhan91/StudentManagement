using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{

    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessStudent stu = new ProcessStudent();
            stu.RegistrationCompleted += stu_RegistrationCompleted;
            stu.StartRegistration();
            Console.WriteLine("Enter your choice");
            Console.WriteLine("1.Register Student\n2.View student details\n3.Check student balance\n4.Pay fees");
            int Choice = int.Parse(Console.ReadLine());
            var student = new Student();


            switch (Choice)
            {
                case 1:
                    {
                        
                        student.AddStudent();
                        stu.StartRegistration();

                        break;
                    }

                case 2:
                    {
                        student.SearchStudent();
                        break;
                    }
                case 3:
                    {
                        student.CheckStudentBalance();
                        break;
                    }
                case 4:
                    {
                        student.PayFee();
                        break;
                    }
                    // Handle other choices...
            }
        }
        public static void stu_RegistrationCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Student Registration Complete");
        }
    }

    


    public class ProcessStudent
    {

        public event EventHandler RegistrationCompleted;
        
        public void StartRegistration()
        {
            OnProcessCompleted(EventArgs.Empty);
        }

        protected virtual void OnProcessCompleted(EventArgs e)
        {
            RegistrationCompleted?.Invoke(this, e); 
        }
    }


 }

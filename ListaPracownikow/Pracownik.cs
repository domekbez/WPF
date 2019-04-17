using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laby
{
    public enum Currency { PLN, USD, EUR, GBP, NOK }
    public enum Role { Worker, SeniorWorker, IT, Manager, Director, CEO }
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public int Salary { get; set; }
        public Currency SalaryCurrency { get; set; }
        public Role CompanyRole { get; set; }

        public Employee(string firstName, string lastName, string sex, DateTime birthDate, string birthCountry, int salary, Currency salaryCurrency, Role companyRole)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            BirthDate = birthDate;
            BirthCountry = birthCountry;
            Salary = salary;
            SalaryCurrency = salaryCurrency;
            CompanyRole = companyRole;
            
        }
        public Employee()
        {
            Salary = 5000;

            BirthDate = DateTime.Now.AddYears(-30);
        }
        

        public string ZrobWiersz()
        {
            string data1 = BirthDate.Day.ToString();
            string data2 = BirthDate.Month.ToString();
            string data3 = BirthDate.Year.ToString();
            string data = data1 + "." + data2 + "." + data3;
            string a=FirstName+";"+LastName+";"+Sex+";"+ data + ";" + BirthCountry + ";" + Salary + ";" + (int)SalaryCurrency + ";" + (int)CompanyRole;
            return a; 
        }
    }

}

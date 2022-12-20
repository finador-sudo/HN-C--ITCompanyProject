using System.Xml.Serialization;

namespace PrjProCompany
{
    public class Programmer : IEmployee
    {
        public String? LastName { get; set; }
        public String? FirstName { get; set; }
        public String? Activity { get; set; }
        [XmlIgnore]
        public DateOnly DateStart { get; set; }
        [XmlIgnore]
        public DateOnly DateEnd { get; set; }
        public float DaySalary { get; set; }
        [XmlElement("DateStart")]
        public string DateStartString
        {
            get { return this.DateStart.ToString(); }
            set { this.DateStart = DateOnly.Parse(value); }
        }
        [XmlElement("DateEnd")]
        public string DateEndString
        {
            get { return this.DateEnd.ToString(); }
            set { this.DateEnd = DateOnly.Parse(value); }
        }

        public Programmer(string LastName, string FirstName, string activity,
            DateOnly DateStart, DateOnly DateEnd, float daySalary)
        {
            this.LastName = LastName ;
            this.FirstName = FirstName;
            this.Activity = activity;
            this.DateStart = DateStart;
            this.DateEnd = DateEnd;
            this.DaySalary = daySalary;
        }
        public Programmer()
        {
        }

        public int getDayThisMonth()
        {
           DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            int daysThisMonth = 0;
            if (today.CompareTo(DateStart) == -1) //Before activity
            {
                daysThisMonth = 0;
            }
            else if (today.CompareTo(DateStart) == 1) //During or after activity
            {
                if (today.CompareTo(DateEnd) == 1) //After, no longer on this activity
                {
                    if ((DateEnd.Year == today.Year) && (DateEnd.Month == today.Month))
                    {
                        if (DateEnd.Month == DateStart.Month) // started and ended on same month
                        {
                            if (DateStart.Day == 1) { daysThisMonth = DateEnd.Day; } //started day 1
                            else { daysThisMonth = DateEnd.Day - DateStart.Day; }
                        }
                        else // started and ended on diferent months
                        {
                            daysThisMonth = DateEnd.Day;
                        }
                    }
                    else //Activity ended, no active activity
                    {
                        daysThisMonth = 0;
                    }
                }
                else // During activity
                {
                    if ((today.Year == this.DateStart.Year) && (today.Month == this.DateStart.Month))
                    {
                        daysThisMonth = today.Day - this.DateStart.Day;
                    }
                    else
                    { daysThisMonth = today.Day; }
                }
            }
            return daysThisMonth;
        }
        void IEmployee.GetInfo()
        {
            Console.WriteLine("Name: "+this.FirstName+" "+this.LastName);
            Console.WriteLine("Salary: " + this.DaySalary);
        }
        public override String ToString()
        {
           
            return this.LastName + ", " + this.FirstName + " in charge of " + Activity + " from " + this.DateStart + " to " + DateEnd + ". Days this month: "+ this.getDayThisMonth()+" days.";
        }
    }

}

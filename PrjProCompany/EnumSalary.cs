

namespace PrjProCompany
{
    public class EnumSalary
    {
        private float value = -1;
        public const float _Fifty = 0.5f;
        public static readonly EnumSalary Fifty = new EnumSalary(_Fifty);
        public  const float _One_hundred = 1.0f;
        public static readonly EnumSalary One_hundred = new EnumSalary(_One_hundred);
        public float Value { get => value; set => this.value = value; }

        public EnumSalary(float i)
        {
            value = i;
        }
        public EnumSalary()
        {

        }

        public static EnumSalary fromFloat(float salaryPercentage)
        {
            switch (salaryPercentage)
            {
                case _Fifty : return Fifty;
                case _One_hundred : return One_hundred;
                default: throw new ArgumentException("Bad parameter");
            }
        }

        public String toString()
        {
            switch(value)
            {
                case _One_hundred:return "One_hundred";
                case _Fifty:return "Fifty";
                default: throw new ArgumentException("Bad parameter");

            }
        }

        public void Main(string[] args)
        {
            Console.WriteLine(EnumSalary.One_hundred);
        }
    }
    
}



namespace PrjProCompany
{
     public class ProjectTeam
    {
        public String? teamName { get; set; }
        public List<Programmer> programmersList { get; set; } = new List<Programmer>() ;
        public EnumSalary? SalaryType { get; set; }

        public ProjectTeam(String teamName, List<Programmer> programmersList, EnumSalary SalaryType)
        {
            this.teamName = teamName;
            this.programmersList = programmersList;
            this.SalaryType = SalaryType;
        }
        public ProjectTeam() { }
      

        public override String ToString()
        {
            String ret = "Project team: "+this.teamName +"\n";
            foreach (Programmer p in programmersList){
                ret +=p.ToString() + " Total cost: " + this.SalaryType.Value * p.DaySalary * p.getDayThisMonth() +"\n";
            }
            return ret;
        }
    }
}



namespace PrjProCompany
{
    public class ItCompany
    {
        public List<ProjectTeam> projects { get; set; }
        public ItCompany() { this.projects  = new List<ProjectTeam>();}
        public ItCompany(List<ProjectTeam> projects) {this.projects = projects; }
        static void Main(String[] args)
        {
            ItCompany myCompany = null;
            initializeSystem(ref myCompany);

            myCompany = loadSystem(myCompany);

            myCompany = updateSystem(myCompany);

            printReport(myCompany);


        }

        private static void CheckConfigFile()
        {
            if (!File.Exists("./company.xml"))
            {
                throw new Exception("Archive file 'company.xml' doesnt exist.");
            }
            Console.WriteLine("Archive file 'company.xml' exist.");
        }
        private static void initializeSystem(ref ItCompany company)
        {
            Console.WriteLine("Initializing system");
            company = new ItCompany();

            try {
                CheckConfigFile();
            }catch (Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine("Proceeding to create one");
                Programmer p1 = new Programmer("Cardoza", "Roberto", "Airdef",
                new DateOnly(2022, 12, 16), new DateOnly(2023, 1, 28), 105);
                Programmer p2 = new Programmer("Durant", "Kevin", "Airdef",
                    new DateOnly(2022, 12, 16), new DateOnly(2023, 1, 28), 110);
                Programmer p3 = new Programmer("Irving", "Kyrie", "Ground-Control",
                    new DateOnly(2022, 12, 26), new DateOnly(2023, 4, 12), 100);
                Programmer p4 = new Programmer("Kerr", "Steve", "Ground-Control",
                    new DateOnly(2022, 12, 26), new DateOnly(2023, 4, 12), 100);

                ProjectTeam airdef = new ProjectTeam("Airdef", new List<Programmer> { p1, p2 }, EnumSalary.One_hundred);
                ProjectTeam groundctlr = new ProjectTeam("Ground-Control", new List<Programmer> { p3, p4 }, EnumSalary.Fifty);
                company.projects.Add(airdef);
                company.projects.Add(groundctlr);
                saveSystem(company);
            }
        }
        private static ItCompany loadSystem(ItCompany company)
        {
            Console.WriteLine("Loading System");
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItCompany));
            FileStream fileStream = new FileStream("./company.xml", FileMode.Open);
            company = (ItCompany) serializer.Deserialize(fileStream);
            return company;
        }
        private static ItCompany updateSystem(ItCompany company)
        {
            foreach (ProjectTeam pt in company.projects)
            {
                foreach(Programmer p in pt.programmersList)
                {
                    p.DateEnd = p.DateEnd.AddDays(1);
                }
            }
            return company;
        }
        private static void saveSystem(ItCompany company)
        {      
            String LoadFromXMLString = company.ToXML();
            File.WriteAllText("./company.xml", LoadFromXMLString);
        }
        private static void printReport(ItCompany company)
        {
            int numberOfTeams = company.projects.Count;
            int numberOfProgrammers = 0;
            int totalDays = 0;
            int activeProgrammers = 0;
            int daysLeft = 0;

            foreach (ProjectTeam pt in company.projects)
            {
                foreach(IEmployee p in pt.programmersList)
                {
                    numberOfProgrammers++;
                    totalDays += ((Programmer) p).getDayThisMonth();
                    if (((Programmer)p).getDayThisMonth() != 0){
                        activeProgrammers++;
                        DateTime ds = ((Programmer)p).DateEnd.ToDateTime(new TimeOnly(0, 0));
                        DateTime de = ((Programmer)p).DateStart.ToDateTime(new TimeOnly(0, 0));
                        daysLeft = (ds - de).Days;
                    }
                }
            }
            
            
            Console.WriteLine("------------ Printing report --------------");
            
            Console.WriteLine("IT Company - Report\n\n" +
                "IT Company is actually composed of " +numberOfTeams+" project teams, and "+numberOfProgrammers+" programmers.\n" +
                "This month "+totalDays+" days have been consumed by "+activeProgrammers+" programmers, and "+daysLeft+" days remaining for projects.");
            Console.WriteLine("Project teams details\n");
            foreach (ProjectTeam pt in company.projects) 
            {
                Console.WriteLine(pt.ToString());
            }
        }

        /*
         * For XML Serilaization and Deserializa
         */
        public string ToXML()
        {
                var serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);
                serializer.Serialize(streamWriter, this);

                byte[] utf8EncodedXml = memoryStream.ToArray();
                var str = System.Text.Encoding.UTF8.GetString(utf8EncodedXml);
        
                return str.ToString();
        }

        public static ItCompany LoadFromXMLString(string xmlText)
        {
            using (var stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ItCompany));
                return serializer.Deserialize(stringReader) as ItCompany;
            }
        }
    }
}

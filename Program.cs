namespace CSharpAssignments;
using System;

    public struct ColumnData
    {
        public string DisplayName;
        public string FirstName;
        public string LastName;
        public string WorkEmail;
        public string cloudLifecycleState;
        public string IdentityId;
        public string   IsManager;
        public string Department;
        public string JobTitle;
        public string Uid;
        public string AccessType;
        public string AccessSourceName;
        public string AccessDisplayName;
        public string AccessDescription;

        public ColumnData(
            string displayName, string firstName, string lastName, string workEmail,
            string cloudLifecycleState, string identityId, string isManager,
            string department, string jobTitle, string uid, string accessType,
            string accessSourceName, string accessDisplayName, string accessDescription)
        {
            DisplayName = displayName;
            FirstName = firstName;
            LastName = lastName;
            WorkEmail = workEmail;
            this.cloudLifecycleState = cloudLifecycleState;
            IdentityId = identityId;
            IsManager = isManager;
            Department = department;
            JobTitle = jobTitle;
            Uid = uid;
            AccessType = accessType;
            AccessSourceName = accessSourceName;
            AccessDisplayName = accessDisplayName;
            AccessDescription = accessDescription;
        }
    }

    class Program
    {
        private List<ColumnData> oge_data =  new List<ColumnData>();
        static void Main(string[] args)
        {
            Program p = new Program();
            p.ReadCSV("Francis Tuttle Identities_Basic.csv");
            Console.WriteLine(p.oge_data.Count);

            var inactiveUsers = p.oge_data.Count(x => x.cloudLifecycleState == "inactive");
            Console.WriteLine($"Number of inactive users: {inactiveUsers}");

        }
        void ReadCSV(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string header = reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length < 14)
                    {
                        Console.WriteLine("Skipping invalid line: " + line);
                        continue;
                    }

                    ColumnData data_line = new ColumnData(
                        parts[0],
                        parts[1],
                        parts[2],
                        parts[3],
                        parts[4],
                        parts[5],
                        parts[6],
                        parts[7],
                        parts[8],
                        parts[9],
                        parts[10],
                        parts[11],
                        parts[12],
                        parts[13]
                    );

                    oge_data.Add(data_line);
                }
            }
        }
    }


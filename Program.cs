namespace CSharpAssignments;
using System;
using System.Text.RegularExpressions;

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

            var inactiveGroups =from employee in p.oge_data
                                where employee.cloudLifecycleState.ToLower() == "inactive"
                                group employee by employee.DisplayName into userGroup
                                select new 
                                {
                                    DisplayName = userGroup.Key,
                                    Records = userGroup.ToList()
                                };

            Console.WriteLine($"Employees with inactive status: ");
            foreach (var user in inactiveGroups)
            {
                Console.WriteLine($"User: {user.DisplayName}");
                foreach (var record in user.Records)
                {
                    if(!string.IsNullOrWhiteSpace(record.AccessDisplayName) && !string.IsNullOrWhiteSpace(record.AccessSourceName)){
                        Console.WriteLine($"   Access: {record.AccessDisplayName} ({record.AccessSourceName})");
                    }
                }
            }

            var departments =from employee in p.oge_data
                            where !string.IsNullOrWhiteSpace(employee.Department)
                            group employee by employee.Department into deptGroup 
                            orderby deptGroup.Key   
                            select deptGroup.Key;

            Console.WriteLine("Departments in the data:");
            foreach (var dept in departments)
            {
                Console.WriteLine(dept);
            }

            var inactiveByDept =  from employee in p.oge_data
                                    where !string.IsNullOrWhiteSpace(employee.Department)
                                    group employee by employee.Department into deptGroup 
                                    select new
                                    {
                                        DepartmentName = deptGroup.Key,
                                        inactiveCount = (
                                                        from e in deptGroup
                                                        where e.cloudLifecycleState == "inactive" && !string.IsNullOrWhiteSpace(e.AccessType)
                                                        group e by e.DisplayName into empGroup
                                                        select empGroup.Key
                                                        ).Count()
                                    };

            Console.WriteLine("Departments and inactive employees with access:");
            foreach (var dept in inactiveByDept)
            {
                Console.WriteLine($"{dept.DepartmentName}: {dept.inactiveCount} inactive employee with access");
            }

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


namespace CSharpAssignments;
using System;

    public struct ColumnData
    {
        public string DisplayName;
        public string FirstName;
        public string LastName;
        public string WorkEmail;
        public string IdentityId;
        public string AccessDisplayName;
        public string Uid;
        public string AccessDescription;
        public bool   IsManager;
        public string Department;
        public string JobTitle;
        public string AccessType;
        public string AccessSourceName;
    }


    class Program
    {
        private Dictionary<string, List<ColumnData>> students = new Dictionary<string, List<ColumnData>>();


        static void Main(string[] args)
        {
            Program p = new Program();
            p.ReadCSV("Francis Tuttle Identities_Basic.xlsx");
        }


        void ReadCSV(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    
                }
            }
        }
    }


namespace ACCOUNTING.CLIENT.Pages.Accounts.Pages
{
    partial class ManagementAccounts
    {
        public record Employee(string MainAgent, string DuplicateAgent, double Percent, string IPs);
        public IEnumerable<Employee> employees;

        protected override void OnInitialized()
        {
            employees = new List<Employee>
             {
            new Employee("Alice", "Alicet", 0.8, "10.8.126.35"),
            new Employee("Irak", "Iraks", 0.6, "10.8.126.31"),
            new Employee("John", "Johns", 0.95, "10.8.126.55,10.8.136.25"),
            };
        }
    }
}

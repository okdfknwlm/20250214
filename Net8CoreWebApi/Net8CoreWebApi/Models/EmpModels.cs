using Newtonsoft.Json;

namespace Net8CoreWebApi.Models
{
    public class EmpModels
    {
        private readonly Common _com;
        public EmpModels(Common comm)
        {
            _com = comm;
        }
        public ApiResponse ListEmp() => _com.DoAPI("List");
        public ApiResponse CreateEmp(CreateEmpClass _class) => _com.DoAPI("Create", 0, JsonConvert.SerializeObject(_class));
        public ApiResponse GetEmp(int EmpID) => _com.DoAPI("GetEmp", EmpID);
        public ApiResponse UpdateEmp(int EmpID, UpdateEmpClass _class) => _com.DoAPI("Update", EmpID, JsonConvert.SerializeObject(_class));
        public ApiResponse DeleteEmp(int EmpID) => _com.DoAPI("Delete", EmpID);

        #region 參數
        public class CreateEmpClass
        {            
            public string LastName { get; set; }

            public string FirstName { get; set; }

            public string Title { get; set; }

            public string? TitleOfCourtesy { get; set; }

            public DateTime? BirthDate { get; set; }

            public DateTime? HireDate { get; set; }

            public string? Address { get; set; }

            public string? City { get; set; }

            public string? Region { get; set; }

            public string? PostalCode { get; set; }

            public string? Country { get; set; }

            public string? HomePhone { get; set; }

            public string? Extension { get; set; }

            public int? ReportsTo { get; set; }
        }

        public class UpdateEmpClass
        {
            public int EmployeeID { get; set; }

            public string LastName { get; set; }

            public string FirstName { get; set; }

            public string Title { get; set; }

            public string? TitleOfCourtesy { get; set; }

            public DateTime? BirthDate { get; set; }

            public DateTime? HireDate { get; set; }

            public string? Address { get; set; }

            public string? City { get; set; }

            public string? Region { get; set; }

            public string? PostalCode { get; set; }

            public string? Country { get; set; }

            public string? HomePhone { get; set; }

            public string? Extension { get; set; }

            public int? ReportsTo { get; set; }
        }
        #endregion

    }
}

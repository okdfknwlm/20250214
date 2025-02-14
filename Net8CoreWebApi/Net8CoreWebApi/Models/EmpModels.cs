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
        public string ListEmp() => (string)_com.doAPI("List");
        public string CreateEmp(CreateEmpClass _class) => (string)_com.doAPI("Create", JsonConvert.SerializeObject(_class));
        public string UpdateGetEmp(UpdateGetEmpClass _class) => (string)_com.doAPI("UpdateGet", JsonConvert.SerializeObject(_class));
        public string UpdatePostEmp(UpdatePostEmpClass _class) => (string)_com.doAPI("UpdatePost", JsonConvert.SerializeObject(_class));
        public string DeleteEmp(DeleteEmpClass _class) => (string)_com.doAPI("Delete", JsonConvert.SerializeObject(_class));

        #region 參數
        public class CreateEmpClass
        {
            public string? SelectEmp { get; set; }
        }

        public class UpdateGetEmpClass
        {
            public string? SelectEmp { get; set; }
        }

        public class UpdatePostEmpClass
        {
            public string? SelectEmp { get; set; }
        }

        public class DeleteEmpClass
        {
            public string? SelectEmp { get; set; }
        }
        #endregion

    }
}

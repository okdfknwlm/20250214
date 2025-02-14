namespace Net8CoreWebApi.Models
{
    public class EmpModels
    {
        private readonly Common _com;
        public EmpModels(Common comm)
        {
            _com = comm;
        }
        public string GetList() 
        {
            return (string)_com.Common_Usp_Function(typeof(string), "usp_CommonProcedure",);
        }


    }
}

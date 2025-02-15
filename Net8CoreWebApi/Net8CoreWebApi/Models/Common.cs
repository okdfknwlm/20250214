using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

namespace Net8CoreWebApi.Models
{
    public class Common
    {
        private readonly string _connectionString;
        private readonly ApiResponse _res;
        public Common(IConfiguration configuration, ApiResponse res)
        {
            _connectionString = configuration.GetConnectionString("DBconnectonString");
            _res = res;
        }

        public ApiResponse DoAPI(string TypeX, int EmpID = 0, string JsonData = "")
        {
            string str_json = string.Empty, _Result_Code = string.Empty, _Result = string.Empty;
            try
            {
                if (!VaildJson(JsonData) && JsonData != "")
                {
                    _res.Result_Code = "-4";
                    _res.Result = "JsonErr";
                    return _res;
                }
                using SqlConnection conn = new(_connectionString);
                conn.Open();
                using SqlCommand cmd = new("usp_CommonProcedure", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TypeX", SqlDbType.VarChar, 20).Value = TypeX;
                cmd.Parameters.Add("@JsonX", SqlDbType.NVarChar, -1).Value = JsonData;
                if (EmpID != 0)
                {
                    cmd.Parameters.Add("@EmpId", SqlDbType.Int).Value = EmpID;
                }
                cmd.Parameters.Add("@Result_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                if (Convert.IsDBNull(cmd.Parameters["@Result_Code"].Value))
                {
                    _res.Result_Code = "-2";
                    _res.Result = "查無資料";
                }
                else
                {
                    _res.Result_Code = cmd.Parameters["@Result_Code"].Value.ToString();
                    _res.Result = cmd.Parameters["@Result"].Value.ToString();
                }
            }
            catch (Exception)
            {
                _res.Result_Code = "-3";
                _res.Result = "查無資料";
            }          
            return _res;
        }

        private static bool VaildJson(string Json)
        {
            if (string.IsNullOrWhiteSpace(Json))
                return false;

            try
            {
                using JsonDocument doc = JsonDocument.Parse(Json);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}

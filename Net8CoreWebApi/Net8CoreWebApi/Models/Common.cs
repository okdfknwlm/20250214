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

        public string doAPI(string TypeX, string JsonData)
        {
            string str_json = string.Empty, _Result_Code = string.Empty, _Result = string.Empty;
            try
            {
                if (!vaildJson(JsonData))
                {
                    _Result_Code = "-4";
                    _Result = "JsonErr";
                    str_json = JsonConvert.SerializeObject(_res);
                    return str_json;
                } 
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_CommonProcedure", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TypeX", SqlDbType.VarChar, 20).Value = TypeX;
                        cmd.Parameters.Add("@json", SqlDbType.NVarChar, -1).Value = JsonData;
                        cmd.Parameters.Add("@Result_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Result", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;

                        cmd.CommandTimeout = 0;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                _Result_Code = "-1";
                                _Result = "查無資料";
                            }
                            else
                            {
                                System.Text.StringBuilder jsonResult = new System.Text.StringBuilder();
                                while (reader.Read())
                                {
                                    jsonResult.Append(reader.GetValue(0).ToString());
                                }
                                str_json = jsonResult.ToString();

                                if (Convert.IsDBNull(cmd.Parameters["@Result_Code"].Value))
                                {
                                    _Result_Code = "-2";
                                    _Result = "查無資料";
                                }
                                else
                                {
                                    _Result_Code = cmd.Parameters["@Result_Code"].Value.ToString();
                                    _Result = cmd.Parameters["@Result"].Value.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Result_Code = "-3";
                _Result = "查無資料";
            }

            if (_Result_Code != "0000")
            {
                _res.Result_Code = _Result_Code;
                _res.Result = _Result;                
            }
            str_json = JsonConvert.SerializeObject(_res);
            return str_json;
        }

        private bool vaildJson(string Json)
        {
            if (string.IsNullOrWhiteSpace(Json))
                return false;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(Json))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}

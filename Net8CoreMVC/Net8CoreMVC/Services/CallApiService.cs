using Net8CoreMVC.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace Net8CoreMVC.Services
{
    public class CallApiService
    {
        private readonly HttpClient _httpClient;

        public CallApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class ApiResponse
        {
            public string Result_Code { get; set; }
            public string Result { get; set; }
        }

        private T CallApi<T>(string url, HttpMethod method, object data = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, url);

                if (data != null)
                {
                    request.Content = JsonContent.Create(data);
                }

                var response = _httpClient.Send(request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API error: {response.StatusCode}");

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                if (apiResponse.Result_Code != "0000")
                    throw new Exception(apiResponse?.Result ?? "Unknown error");

                var result = JsonConvert.DeserializeObject<T>(apiResponse.Result);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        private ApiResponse CallApiVoid(string url, HttpMethod method, object data = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, url);

                if (data != null)
                {
                    request.Content = JsonContent.Create(data);
                }

                var response = _httpClient.Send(request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API error: {response.StatusCode}");

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                if (apiResponse.Result_Code != "0000")
                    throw new Exception(apiResponse?.Result ?? "Unknown error");

                return apiResponse;
            }
            catch (Exception)
            {
                ApiResponse a = new();
                return a;
            }
           
        }


        public IEnumerable<EmpModel> GetEmpList() => CallApi<IEnumerable<EmpModel>>("Emp", HttpMethod.Get);

        public EmpModel GetEmp(int EmpID) => CallApi<EmpModel>($"Emp/{EmpID}", HttpMethod.Get);

        public ApiResponse CreateEmp(EmpModel model) => CallApiVoid("Emp", HttpMethod.Post, model);
        
        public ApiResponse UpdateEmp(int EmpID, EmpModel model) => CallApiVoid($"Emp/{EmpID}", HttpMethod.Put, model);

        public ApiResponse DeleteEmp(int EmpID) => CallApiVoid($"Emp/{EmpID}", HttpMethod.Delete);
    }
}

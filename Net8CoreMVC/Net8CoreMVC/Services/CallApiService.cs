using Net8CoreMVC.Models;
using Newtonsoft.Json;

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

        private async Task<T> CallApiAsync<T>(string url, HttpMethod method, object data = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            
            if (data != null)
            {
                request.Content = JsonContent.Create(data);
            }
            
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("API_error");
            
            var responseContent = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (apiResponse.Result_Code != "0000")
                throw new Exception(apiResponse?.Result ?? "Unknown error");
                        
            var result = JsonConvert.DeserializeObject<T>(apiResponse.Result);
            return result;
        }

        public Task<IEnumerable<EmpModel>> GetEmpListAsync()
        {
            return CallApiAsync<IEnumerable<EmpModel>>("Emp", HttpMethod.Get);
        }

        public Task<EmpModel> GetEmpAsync(int EmpID)
        {
            return CallApiAsync<EmpModel>($"Emp/{EmpID}", HttpMethod.Get);
        }

        public Task<string> CreateEmpAsync(EmpModel model)
        {
            return CallApiAsync<string>("Emp", HttpMethod.Post, model);
        }

        public Task<string> UpdateEmpAsync(int EmpID, EmpModel model)
        {
            return CallApiAsync<string>($"Emp/{EmpID}", HttpMethod.Put, model);
        }

        public Task<string> DeleteEmpAsync(int EmpID)
        {
            return CallApiAsync<string>($"Emp/{EmpID}", HttpMethod.Delete);
        }
    }
}

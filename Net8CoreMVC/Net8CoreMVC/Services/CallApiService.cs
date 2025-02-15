using Net8CoreMVC.Models;

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

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

            if (apiResponse == null || apiResponse.Result_Code != "0000")
                throw new Exception(apiResponse?.Result ?? "Unknown error");

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public Task<IEnumerable<EmpModel>> GetEmpListAsync()
        {
            return CallApiAsync<IEnumerable<EmpModel>>("http://localhost:5217/api/Emp", HttpMethod.Get);
        }

        public Task<EmpModel> GetEmpAsync(int EmpID)
        {
            return CallApiAsync<EmpModel>($"http://localhost:5217/api/Emp/{EmpID}", HttpMethod.Get);
        }

        public Task<string> CreateEmpAsync(EmpModel model)
        {
            return CallApiAsync<string>("http://localhost:5217/api/Emp", HttpMethod.Post, model);
        }

        public Task<string> UpdateEmpAsync(int EmpID, EmpModel model)
        {
            return CallApiAsync<string>($"http://localhost:5217/api/Emp/{EmpID}", HttpMethod.Put, model);
        }

        public Task<string> DeleteEmpAsync(int EmpID)
        {
            return CallApiAsync<string>($"http://localhost:5217/api/Emp/{EmpID}", HttpMethod.Delete);
        }
    }
}

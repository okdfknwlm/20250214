using Microsoft.AspNetCore.Mvc;
using Net8CoreWebApi.Models;


namespace Net8CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly EmpModels _model;
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _webHost;
        public EmpController(IHttpContextAccessor http, IWebHostEnvironment webHost ,EmpModels model)
        {
            _http = http;
            _webHost = webHost;
            _model = model;
        }

        [HttpGet]
        public ActionResult GetEmpList() => Ok(_model.ListEmp());        

        [HttpPost]
        public ActionResult CreateEmp(EmpModels.CreateEmpClass _class) => Ok(_model.CreateEmp(_class));

        [HttpGet("{id}")]
        public ActionResult GetEmp(int id) => Ok(_model.GetEmp(id));

        [HttpPut("{id}")]
        public ActionResult UpdateEmp(int id, EmpModels.UpdateEmpClass _class)
        {
            _class.EmployeeID = id;
            return Ok(_model.UpdateEmp(id,_class));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmp(int id) => Ok(_model.DeleteEmp(id));
    }
}

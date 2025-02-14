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

        // POST api/<EmpController>
        [HttpPost]
        public ActionResult CreateEmp(EmpModels.CreateEmpClass _class) => Ok(_model.CreateEmp(_class));

        // GET api/emp/5
        [HttpGet("{id}")]
        public ActionResult UpdateGetEmp(EmpModels.UpdateGetEmpClass _class) => Ok(_model.UpdateGetEmp(_class));

        // PUT api/<EmpController>/5
        [HttpPut("{id}")]
        public ActionResult UpdatePostEmp(EmpModels.UpdatePostEmpClass _class) => Ok(_model.UpdatePostEmp(_class));

        // DELETE api/<EmpController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmp(EmpModels.DeleteEmpClass _class) => Ok(_model.DeleteEmp(_class));
    }
}

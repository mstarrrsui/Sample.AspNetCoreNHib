using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSUI.Common.Data.Utils;
using SampleApp1.Entities;

namespace SampleApp1.Controllers
{
    [Produces("application/json")]
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {

        public NHibernate.ISession CurrentSession { get { return HttpContext.Items["RSUINHibSession"] as NHibernate.ISession; } }

        [HttpGet]
        public JsonResult Get()
        {

                var departmentList = CurrentSession.QueryOver<Department>()
                    .List()
                    .Select(d => new { id = d.Id, description = d.Description })
                    .ToList();

                return Json(departmentList);
        }
    }
}
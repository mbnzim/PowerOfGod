using PowerOfGod.Business.ShoppingLogic;
using PowerOfGod.ViewModel.ShoppingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PowerOfGod.Web.Controllers.Shopping
{
    public class apiDepartmentsController : ApiController
    {
        Department_Service department_Service;
        public apiDepartmentsController()
        {
            this.department_Service = new Department_Service();
        }
        // GET: api/apiDepartments
        public List<DepartmentModel> GetDepartments()
        {
            return department_Service.allDepartments().Select(x => new DepartmentModel()
            {
                Department_Name = x.Department_Name,
                Description = x.Description,
                Department_ID = x.Department_ID
            }).ToList();
        }

    }
}

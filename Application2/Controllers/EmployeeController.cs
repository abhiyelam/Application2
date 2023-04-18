using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using System.IO;

namespace Application2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration configuration;
        BusinessLogic businessLogic;
        public EmployeeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            businessLogic = new BusinessLogic(this.configuration);
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            SearchModel searchmodel = new SearchModel();
             
            searchmodel.ListStates = businessLogic.GetState();
            searchmodel.districts = businessLogic.GetDistrict(searchmodel.StateId);
            List<Employee> emplist = businessLogic.SearchEmployee(searchmodel.Search,searchmodel.StateId).ToList();
            searchmodel.List=emplist.ToList();
            return View(searchmodel);
        }
        [HttpPost]
        public ActionResult Index(SearchModel t,int id)
        {
            SearchModel searchmodel = new SearchModel();
            searchmodel.ListStates = businessLogic.GetState();
            searchmodel.districts = businessLogic.GetDistrict(t.StateId);
            searchmodel.Search = t.Search;
           searchmodel.StateId = t.StateId;
            var list = businessLogic.SearchEmployee(t.Search,t.StateId);
            searchmodel.List=list.ToList();
            return View(searchmodel);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var emp = businessLogic.GetAllEmployee().Single(x => x.Id == id);
            return View(emp);
        }
        public JsonResult City(int id)
        {

            List<District> districts = new List<District>();
            districts=businessLogic.GetDistrict(id);

            return Json(districts);
        }
        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            Employee emp = new Employee();
            emp.ListStates = businessLogic.GetState().ToList();
            emp.districts = businessLogic.GetDistrict(emp.StateId).ToList();
            return View(emp);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp, IFormFile Imagepath)
        {
          //Employee emp = new Employee();
            if (ModelState.IsValid)
            {
                if (Imagepath!= null)
                {
                    string filename = Imagepath.FileName;
                        filename=Path.GetFileName(filename);
                    string uploadfilepath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Image", filename);
                    // file is uploaded
                    var stream = new FileStream(uploadfilepath, FileMode.Create);
                    Imagepath.CopyTo(stream);
                }
                var result = businessLogic.CheckEmail(emp.Email, emp.Id);
                if (result > 0)
                {
                    ModelState.AddModelError("Email", "alread exist");
                  emp.ListStates = businessLogic.GetState().ToList();
                    return View(emp);
                }
                businessLogic.AddEmployee(emp);
                //emp.ListStates = businessLogic.GetState().ToList();
                emp.districts = businessLogic.GetDistrict(emp.StateId);
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // GET: EmployeeController/Edit/
        public ActionResult Edit(int id)
        {
             

            var emp = businessLogic.GetAllEmployeeDetails(id);
            if (emp == null)
            {
                return NotFound();
            }
            if (emp.ListStates == null)
            {
                emp.ListStates = new List<States>();
            }
            emp.ListStates = businessLogic.GetState().ToList();
            emp.districts = businessLogic.GetDistrict(emp.StateId);
            //emp.districts =
            



            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp,IFormFile UploadImage)
        {
            string filename = "";
            try
            {
               
                if (ModelState.IsValid)
                {
                    if (UploadImage != null )
                    {
                         filename = UploadImage.FileName;
                        filename = DateTime.Now.ToString("YYMMddhhmmss")+  Path.GetFileName(filename);
                        string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Image", filename);
                        // file is uploaded
                        var stream = new FileStream(uploadfilepath, FileMode.Create);
                        UploadImage.CopyTo(stream);
                        emp.Imagepath = filename;


                    }
                    else if (string.IsNullOrEmpty(emp.Imagepath))
                    {
                        emp.Imagepath = "";
                    }

                    emp.ListStates = businessLogic.GetState().ToList();
                        emp.districts = businessLogic.GetDistrict(emp.StateId).ToList();
                        var result = businessLogic.CheckEmail(emp.Email, emp.Id);
                    if (result > 0)
                    {
                        ModelState.AddModelError("Email", "alread exist");
                        emp.ListStates = businessLogic.GetState().ToList();
                        return View(emp);
                    }


                   
                        result = businessLogic.UpdateEmployee(emp);
                        //emp.Imagepath = "";


                        // result = businessLogic.UpdateEmployee(emp);
                        if (result == 1)
                            return RedirectToAction(nameof(Index));
                        else
                            return View();
                }
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
           return View();
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {

            var emp = businessLogic.GetAllEmployee().Single(x => x.Id == id);

            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var result = businessLogic.DeleteEmployee(id);
                if (result == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        public JsonResult Deleteemp(int id)
        {

            

            return Json(true);
        }
    }
}

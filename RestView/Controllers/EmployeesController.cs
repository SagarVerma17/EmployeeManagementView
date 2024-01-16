using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestView.Data;
using RestView.Models;

namespace RestView.Controllers
{
    public class EmployeesController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:8080/api");
        private readonly HttpClient _httpClient;


        public EmployeesController(EmployeeContext context)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        // GET: Employees
        public IActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Employee").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<Employee>>(data);
            }

            return View(employeeList);
        }


        //// GET: Employees/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            string jsonEmployee = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Employee", content).Result;
            if (response.IsSuccessStatusCode)
            {
                // Show success message in dialog box using JavaScript
                ViewBag.SuccessMessage = "Employee data saved successfully!";
            }
            else
            {
                // Show error message in dialog box using JavaScript
                ViewBag.ErrorMessage = "Error saving Employee data. Please try again.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Employee employee)
        {
            string jsonEmployee = JsonConvert.SerializeObject(employee);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Employee/" + employee.EmpId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                // Show success message in dialog box using JavaScript
                ViewBag.SuccessMessage = "Employee data saved successfully!";
            }
            else
            {
                // Show error message in dialog box using JavaScript
                ViewBag.ErrorMessage = "Error saving Employee data. Please try again.";
            }
            return RedirectToAction("Index");
        }

        //// GET: Employees/Edit/5
        public async Task<IActionResult> EditView(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = getEmployeebyId(id);

            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpGet("{id}")]
        public Employee getEmployeebyId(int? id)
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Employee/" +id ).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Employee employeeList = JsonConvert.DeserializeObject<Employee>(data);
                return employeeList;
            }

            return null;

        }

       
        //// GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Employee/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

    }
}

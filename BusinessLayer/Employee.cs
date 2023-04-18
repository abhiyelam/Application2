using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }
        public string Hobbie { get; set; }
        public int StateId { get; set; }
        public List<States> ListStates { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public List<District> districts { get; set; }
        public string Imagepath { get; set; }
        public IFormFile UploadImage { get; set; }
        // public string Search { get; set; }
        // public List<Employee> List { get; set; }
    }
}

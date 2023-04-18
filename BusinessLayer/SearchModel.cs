using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class SearchModel
    {
        public string Search { get; set; }
        public List<Employee> List { get; set; }
        public int StateId { get; set; }
        public List<States> ListStates { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public List<District> districts { get; set; }
       // public string Imagepath { get; set; }

    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BusinessLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class BusinessLogic
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public BusinessLogic(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection("Data Source=192.168.10.102;Initial Catalog=invitra;Persist Security Info=True;User ID=sa;Password=ITPL#2020@Inv");
        }
        public List<Employee> GetAllEmployee()
        {
            List<Employee> emplist = new List<Employee>();
            string qry = "sp_read_employee";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
           dr= cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.MobileNo = dr["MobileNo"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Hobbie = dr["Hobbie"].ToString();
                   emp.StateName = dr["StateName"].ToString();
                    //emp.DistrictName = dr["DistrictName"].ToString();
                    emplist.Add(emp);
                }
            }
            con.Close();
            return emplist;
        }

        public Employee GetAllEmployeeDetails(int empid)
        {
            Employee emp = new Employee();
            string qry = "sp_employee_Details";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", empid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.MobileNo = dr["MobileNo"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Hobbie = dr["Hobbie"].ToString();
                    emp.StateId = Convert.ToInt32(dr["StateId"]);
                    emp.DistrictId = Convert.ToInt32(dr["DistrictId"]);
                    emp.Imagepath = dr["Imagepath"].ToString();

                }
            }
            con.Close();
            return emp;
        }
        public List<States> GetState()
        {
            List<States> slist = new List<States>();
            string qry = "sp_getstate";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //Employee emp = new Employee();
                    States s = new States();
                    /*emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.MobileNo = dr["MobileNo"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Hobbie = dr["Hobbie"].ToString();*/
                    s.StateId = Convert.ToInt32(dr["StateId"]);
                    s.StateName = dr["StateName"].ToString();
                    slist.Add(s);
                }
            }
            con.Close();
            return slist;
        }
        public List<District> GetDistrict(int StateId)
        {
            List<District> dlist = new List<District>();
            string qry = "sp_getdistrict";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("StateId", StateId);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    District d = new District();
                   
                    d.DistrictId= Convert.ToInt32(dr["DistrictId"]);
                    d.DistrictName= dr["DistrictName"].ToString();
                    dlist.Add(d);
                }
            }
            con.Close();
            return dlist;
        }
        public List<Employee> SearchEmployee(string Search,int StateId)
        {
            List<Employee> list = new List<Employee>();
            string qry = "sp_search_employee";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (string.IsNullOrEmpty(Search))   
                Search = "";
            cmd.Parameters.AddWithValue("@Search",Search);
            cmd.Parameters.AddWithValue("@StateId", StateId);
            con.Open();
            

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.MobileNo = dr["MobileNo"].ToString();
                    emp.Gender = dr["Gender"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    emp.Hobbie = dr["Hobbie"].ToString();
                    emp.StateName = dr["StateName"].ToString();
                    emp.DistrictName = dr["DistrictName"].ToString();
                    emp.Imagepath = dr["Imagepath"].ToString();
                    list.Add(emp);
                }
            }
            con.Close();
            return list;
        }
        public int CheckEmail(string Email, int Id)
        {
            int result = 0;
            string qry = "sp_checkemail";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.AddWithValue("@Id", emp.Id);
            
            cmd.Parameters.AddWithValue("@Email",Email);
            cmd.Parameters.AddWithValue("@Id", Id);


            con.Open();
            result = (int)cmd.ExecuteScalar();
            con.Close();
            if (result > 0)
            {
                return result;
            }
            else
            {
                return result;
            }
            //return result;
        }
        public void AddEmployee(Employee emp)
        {
            int result = 0;
            string qry = "sp_insert_employee";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@MobileNo", emp.MobileNo);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Hobbie", emp.Hobbie);
            cmd.Parameters.AddWithValue("@StateId", emp.StateId);
            cmd.Parameters.AddWithValue("@DistrictId", emp.DistrictId);
            if (string.IsNullOrEmpty(emp.Imagepath))
            {
                cmd.Parameters.AddWithValue("@ImagePath", " ");
                //emp.Imagepath = "";
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", emp.Imagepath);
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
           // return result;
        }
        public int UpdateEmployee(Employee emp)
        {
            int result = 0;
            string qry = "sp_update_employee";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@MobileNo", emp.MobileNo);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Hobbie", emp.Hobbie);
            cmd.Parameters.AddWithValue("@StateId", emp.StateId);
            cmd.Parameters.AddWithValue("@DistrictId", emp.DistrictId);
            if (string.IsNullOrEmpty(emp.Imagepath))
            {
                cmd.Parameters.AddWithValue("@ImagePath", " ");
                //emp.Imagepath = "";
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImagePath", emp.Imagepath);
            }
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteEmployee(int id)
        {
            int result = 0;
            string qry = "sp_delete_employee";
            cmd = new SqlCommand(qry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
           
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}

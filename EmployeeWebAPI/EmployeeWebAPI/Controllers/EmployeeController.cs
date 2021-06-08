using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvir;
        //IWebHostEnvironment for hosting of files in application

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment hostEnvir)
        {
            _configuration = configuration;
            _hostEnvir = hostEnvir;
        }

        //For using sql query we have to install SqlClient from NuGet
        //Link = https://www.youtube.com/watch?v=Dpv6lUKNL9o&t=4438s&ab_channel=ArtofEngineer

        [HttpGet]
        public JsonResult GetAllEmployees()
        {
            string query = @"select EmployeeId, EmployeeName, Department,
                             convert(varchar(10), DateOfJoining, 105) as DateOfJoining, 
                             PhotoFileName from dbo.Employee";

            //-- CONVERT Syntax:  
            // CONVERT(data_type(length), expression , style)

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            string query = @"insert into dbo.Employee 
                             (EmployeeName,Department,DateOfJoining,PhotoFileName)
                             values
                             (
                               '" + employee.EmployeeName + @"',
                               '" + employee.Department + @"',
                               '" + employee.DateOfJoining + @"',
                               '" + employee.PhotoFileName + @"'
                              )";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult UpdateEmployee(Employee employee)
        {
            string query = @"update dbo.Employee set 
                             EmployeeName = '" + employee.EmployeeName + @"', 
                             Department = '" + employee.Department + @"', 
                             DateOfJoining = '" + employee.DateOfJoining + @"', 
                             PhotoFileName = '" + employee.PhotoFileName + @"' 
                             where EmployeeId = '" + employee.EmployeeId + @"' ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteEmployee(int id)
        {
            string query = @"delete from dbo.Employee
                             where EmployeeId = '" + id + @"' ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _hostEnvir.ContentRootPath + "/Photos/" + fileName;

                using(var fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(fileStream);
                }

                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                return new JsonResult("anonymous.jpg");
            }
        }

        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"select DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}

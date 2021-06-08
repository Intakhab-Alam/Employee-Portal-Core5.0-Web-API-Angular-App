using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //For using sql query we have to install SqlClient from NuGet
        //Link = https://www.youtube.com/watch?v=Dpv6lUKNL9o&t=4438s&ab_channel=ArtofEngineer

        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
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
        public JsonResult AddDepartment(Department department)
        {
            string query = @"insert into dbo.Department 
                             values('"+ department.DepartmentName + @"')";

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
        public JsonResult UpdateDepartment(Department department)
        {
            string query = @"update dbo.Department 
                                set DepartmentName = '" + department.DepartmentName + @"' 
                                where DepartmentId = '" + department.DepartmentId + @"' ";

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
        public JsonResult DeleteDepartment(int id)
        {
            string query = @"delete from dbo.Department
                             where DepartmentId = '" + id + @"' ";

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
    }
}

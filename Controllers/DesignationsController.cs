using CompanyPrinters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessLogic;
using BusinessObject;

namespace CompanyPrinters.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationsController : ControllerBase
    {

        BusinessLogic.BusinessLogicClass bl = new BusinessLogic.BusinessLogicClass();
        private readonly IConfiguration _configuration;

        public DesignationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select DesignationID, DesignationName from dbo.Designations";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyPrinters");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet]
        [Route("GetDesignationById")]
        public IActionResult Get(int designationId)
        {
            string query = @"
                    select DesignationID, DesignationName from dbo.Designations where DesignationID = " + designationId;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyPrinters");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Designations des)
        {
            //string query = @"
            //        insert into dbo.Designations values 
            //        ('" + des.DesignationName + @"')
            //        ";
            //DataTable table = new DataTable();
            //string sqlDataSource = _configuration.GetConnectionString("CompanyPrinters");
            //SqlDataReader myReader;
            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader); ;

            //        myReader.Close();
            //        myCon.Close();
            //    }
            //}

            //return new JsonResult("Added Successfully");

            bl.DesiEntry(des);

            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Designations des)
        {
            string query = @"
                    update dbo.Designations set 
                    DesignationName = '" + des.DesignationName + @"'
                    where DesignationID = " + des.DesignationID + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyPrinters");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Designations
                    where DesignationID = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CompanyPrinters");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}

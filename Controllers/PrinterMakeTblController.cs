using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyPrinters.Models;
using BusinessLogic;
using BusinessObject;

namespace CompanyPrinters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrinterMakeTblController : ControllerBase
    {
        BusinessLogic.BusinessLogicClass bl = new BusinessLogic.BusinessLogicClass();
        private readonly IConfiguration _configuration;

        public PrinterMakeTblController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select PrinterMakeID, PrinterMake, convert(varchar(10),CreateDate,120) as CreateDate
                    from dbo.PrinterMakeTbl";
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
        [Route("GetPrinterMakeById")]
        public IActionResult Get(int PrinterMakeID)
        {
            string query = @"
                    select PrinterMakeID, PrinterMake from dbo.PrinterMakeTbl where PrinterMakeID = " + PrinterMakeID;
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
        public JsonResult Post(PrinterMakeTbl printermake)
        {

            bl.PrimEntry(printermake);

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(PrinterMakeTbl printermake)
        {
            string query = @"
                    update dbo.PrinterMakeTbl set 
                    PrinterMake = '" + printermake.PrinterMake + @"',  CreateDate = '" + printermake.CreateDate + @"'
                    where PrinterMakeID = " + printermake.PrinterMakeID + @" 
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
                    delete from dbo.PrinterMakeTbl
                    where PrinterMakeID = " + id + @" 
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

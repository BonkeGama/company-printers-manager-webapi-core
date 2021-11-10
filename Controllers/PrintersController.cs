using Microsoft.AspNetCore.Hosting;
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

namespace CompanyPrinters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PrintersController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select EngenPrintersID, PrinterName, PrinterMakeID,FolderToMonitor, OutputType, FileOutput, Active,
                    convert(varchar(10),CreateTimestamp,120) as CreateTimestamp
                    from dbo.Printers
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

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Printers pr)
        {
            string query = @"
                    insert into dbo.Printers 
                    (PrinterName,PrinterMakeID,FolderToMonitor,OutputType,FileOutput,Active,CreateTimestamp)
                    values 
                    (
                    '" + pr.PrinterName + @"'
                    ,'" + pr.PrinterMakeID + @"'
                    ,'" + pr.FolderToMonitor + @"'
                    ,'" + pr.OutputType + @"'
                    ,'" + pr.FileOutput + @"'
                    ,'" + pr.Active + @"'
                    ,'" + pr.CreateTimestamp + @"'
                    )
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

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Printers pri)
        {
            string query = @"
                    update dbo.Printers set 
                     PrinterName = '" + pri.PrinterName + @"'
                    ,PrinterMakeID = '" + pri.PrinterMakeID + @"'
                    ,FolderToMonitor = '" + pri.FolderToMonitor + @"'
                    ,OutputType = '" + pri.OutputType + @"'
                    ,FileOutput = '" + pri.FileOutput + @"'
                    ,Active = '" + pri.Active + @"'
                    ,CreateTimestamp = '" + pri.CreateTimestamp + @"'
                  
                    where EngenPrintersID = " + pri.EngenPrintersID + @" 
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
                    delete from dbo.Printers
                    where EngenPrintersID = " + id + @" 
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

        [Route("GetAllPrimakeName")]
        public JsonResult GetAllPrimakeName()
        {
            string query = @"
                    select PrinterMake from dbo.PrinterMakeTbl
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

            return new JsonResult(table);
        }
    }
}

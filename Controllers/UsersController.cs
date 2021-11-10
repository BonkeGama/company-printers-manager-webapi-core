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
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public UsersController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Users Get Api
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select UserID, FirstName, LastName, Email, UserName, Password, Address,
                    convert(varchar(10),DOB,120) as DOB,DesignationID
                    from dbo.Users
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

        //UsersPost Api
        [HttpPost]
        public JsonResult Post(Users us)
        {
            string query = @"
                    insert into dbo.Users 
                    (FirstName,LastName,Email,UserName,Password,Address,DOB,DesignationID)
                    values 
                    (
                    '" + us.FirstName + @"'
                    ,'" + us.LastName + @"'
                    ,'" + us.Email + @"'
                    ,'" + us.UserName + @"'
                    ,'" + us.Password + @"'
                    ,'" + us.Address + @"'
                    ,'" + us.DOB + @"'
                    ,'" + us.DesignationID + @"'
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

        //Users Api
        [HttpPut]
        public JsonResult Put(Users use)
        {
            string query = @"
                    update dbo.Users set 
                    FirstName = '" + use.FirstName + @"'
                    ,LastName = '" + use.LastName + @"'
                    ,Email = '" + use.Email + @"'
                    ,UserName = '" + use.UserName + @"'
                    ,Password = '" + use.Password + @"'
                    ,Address = '" + use.Address + @"'
                    ,DOB = '" + use.DOB + @"'
                    ,DesignationID = '" + use.DesignationID + @"'
                    where UserID = " + use.UserID + @" 
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

        //Users Delete Api
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Users
                    where UserID = " + id + @" 
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


        //GetDesignationByID Api
        [Route("GetDesignationByID")]
        public JsonResult GetDesignationByID(int? Desid)
        {
            string query = @"
                    select DesignationName from dbo.Designations  where DesignationID=" + Desid + @" 
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

        //GetAllDesignationName Api
        [Route("GetAllDesignationName")]
        public JsonResult GetAllDesignationNames()
        {
            string query = @"
                    select DesignationID,DesignationName from dbo.Designations
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

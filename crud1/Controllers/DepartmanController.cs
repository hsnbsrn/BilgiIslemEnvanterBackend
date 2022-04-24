using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using crud1.Models;

namespace crud1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmanController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public DepartmanController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select * from departman";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Departman dep)
        {
            string query = @"insert into departman values (@ad)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ad", dep.Ad);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Kaydedildi");
        }


        [HttpPut]
        public JsonResult Put(Departman dep)
        {
            string query = @"update departman set ad = @ad where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ad", dep.Ad);
                    myCommand.Parameters.AddWithValue("@id", dep.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Güncellendi");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete departman where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Silindi");
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using crud1.Models;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace crud1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PersonelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select
                                Personel.id,
                                Personel.adsoyad adsoyad,
                                Personel.email  email,
                                Personel.telefon telefon,
                                Departman.ad departman,
                                Personel.departmanid
                                from personel
                                inner join Departman on Departman.id=Personel.departmanid ";
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
        public JsonResult Post(Personel dep)
        {
            string query = @"insert into personel
                            (adsoyad,email,telefon,departmanid)
                            values (@Adsoyad,@Email,@Telefon,@DepartmanId) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Adsoyad", dep.Adsoyad);
                    myCommand.Parameters.AddWithValue("@Email", dep.Email);
                    myCommand.Parameters.AddWithValue("@Telefon", dep.Telefon);
                    myCommand.Parameters.AddWithValue("@DepartmanId", dep.DepartmanId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Kaydedildi");
        }


        [HttpPut]
        public JsonResult Put(Personel per)
        {
            string query = @"update personel set adsoyad = @Adsoyad,email=@Email,telefon=@Telefon,departmanid=@DepartmanId where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Adsoyad", per.Adsoyad);
                    myCommand.Parameters.AddWithValue("@Email", per.Email);
                    myCommand.Parameters.AddWithValue("@Telefon", per.Telefon);
                    myCommand.Parameters.AddWithValue("@DepartmanId", per.DepartmanId);
                    myCommand.Parameters.AddWithValue("@id", per.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Güncellendi");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string query = @"delete personel where id = @id";
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

        [HttpGet]
        [Route("detail")]

        public ActionResult GetById(int id)
        {
            string query = @"select
                                Personel.id,
                                Personel.adsoyad adsoyad,
                                Personel.email  email,
                                Personel.telefon telefon,
                                Departman.ad departman,
                                Personel.departmanid
                                from personel
                                inner join Departman on Departman.id=Personel.departmanid
                                where personel.id=@id";
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
            return Ok(table);
        }
    }
}


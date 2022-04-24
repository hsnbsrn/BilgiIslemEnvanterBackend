using crud1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace crud1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KonumController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KonumController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select
                            Konum.id,
                            Departman.ad as DepartmanAd,
                            Departman.id as DepId,
                            Konum.OdaNo
                            from konum
                            inner join departman on Departman.id=Konum.Departmanid ";
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
        public JsonResult Post(Konum dep)
        {
            string query = @"insert into konum
                            (Departmanid,OdaNo)
                            values (@Departmanid,@OdaNo) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Departmanid", dep.DepId);
                    myCommand.Parameters.AddWithValue("@OdaNo", dep.OdaNo);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Kaydedildi");
        }


        [HttpPut]
        public JsonResult Put(Konum per)
        {
            string query = @"update Konum set departmanid=@DepartmanId where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmanId", per.DepId);
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
            string query = @"delete konum where id = @id";
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
                            Konum.id,
                            Konum.OdaNo,
                            Departman.ad as Depatman
                            from Konum
                            inner join Departman on Departman.id=Konum.Departmanid
                            where Konum.id=@id";
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
    

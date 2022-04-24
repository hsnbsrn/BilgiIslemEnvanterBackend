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
    public class CihazController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CihazController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select
                            Cihaz.id,
                            Konum.OdaNo as OdaNo,
                            Tur.Ad as Tur,
                            Departman.ad as Departman,
                            Tur.id as TurId,
                            Konum.id as KonumId
                            from Cihaz
                            inner join Konum on Konum.id=Cihaz.Konumid
                            inner join Tur on Tur.id=Cihaz.Turid
                            inner join Departman on Departman.id=Konum.Departmanid";
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
        public JsonResult Post(Cihaz dep)
        {
            string query = @"insert into Cihaz
                            (Konumid,Turid)
                            values (@Konumid,@Turid)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Konumid", dep.Konumid);
                    myCommand.Parameters.AddWithValue("@Turid", dep.Turid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Kaydedildi");
        }


        [HttpPut]
        public JsonResult Put(Cihaz per)
        {
            string query = @"update Cihaz  set Turid=@Turid,Konumid=@Konumid where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Turid", per.Turid);
                    myCommand.Parameters.AddWithValue("@Konumid", per.Konumid);
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


        [HttpGet]
        [Route("son")]
        public JsonResult GetSon()
        {
            string query = @"select top 1 id from Cihaz order by id desc";
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

    }
}

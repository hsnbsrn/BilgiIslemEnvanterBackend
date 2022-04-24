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
    public class KasaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KasaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select
                            Kasa.id,    
                            Kasa.Anakart,
                            Kasa.Cihazid,
                            Kasa.Dvi,
                            Kasa.EkranKarti,
                            Kasa.Hdmi,
                            Kasa.Islemci,
                            Kasa.Vga,
                            Kasa.Ip,
							Konum.OdaNo,
							Departman.ad as Departman
                            from Kasa 
                            inner join konum on konum.id=Kasa.Konumid
                            inner join Departman on Departman.id=Konum.Departmanid ";
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
        public JsonResult Post(Kasa per)
        {
            string query = @"insert into kasa
                            ( Anakart,EkranKarti,Islemci,Dvi,Hdmi,Vga,Ip,Konumid )
                            values ( @Anakart,@EkranKarti,@Islemci,@Dvi,@Hdmi,@Vga,@Ip,@Konumid) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Anakart", per.Anakart);
                    myCommand.Parameters.AddWithValue("@EkranKarti", per.EkranKarti);
                    myCommand.Parameters.AddWithValue("@Islemci", per.Islemci);
                    myCommand.Parameters.AddWithValue("@Dvi", per.Dvi);
                    myCommand.Parameters.AddWithValue("@Hdmi", per.Hdmi);
                    myCommand.Parameters.AddWithValue("@Vga", per.Vga);
                    myCommand.Parameters.AddWithValue("@Ip", per.Ip);
                    myCommand.Parameters.AddWithValue("@Konumid", per.Konumid);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }
            return new JsonResult("Kaydedildi");
        }


        [HttpPut]
        public JsonResult Put(Kasa per)
        {
            string query = @"update Kasa set Anakart=@Anakart,EkranKarti=@EkranKarti,Islemci=@Islemci,Dvi=@Dvi,Hdmi=@Hdmi,Vga=@Vga,Ip=@Ip,Konumid=@Konumid where Cihazid=@Id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Anakart", per.Anakart);
                    myCommand.Parameters.AddWithValue("@EkranKarti", per.EkranKarti);
                    myCommand.Parameters.AddWithValue("@Islemci", per.Islemci);
                    myCommand.Parameters.AddWithValue("@Dvi", per.Dvi);
                    myCommand.Parameters.AddWithValue("@Hdmi", per.Hdmi);
                    myCommand.Parameters.AddWithValue("@Vga", per.Vga);
                    myCommand.Parameters.AddWithValue("@Ip", per.Ip);
                    myCommand.Parameters.AddWithValue("@Id", per.Cihazid);
                    myCommand.Parameters.AddWithValue("@Konumid", per.Konumid);
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
            string query = @"delete Kasa where Kasa.Cihazid = @id";
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
                            Kasa.id,    
                            Kasa.Anakart,
                            Kasa.Cihazid,
                            Kasa.Dvi,
                            Kasa.EkranKarti,
                            Kasa.Hdmi,
                            Kasa.Islemci,
                            Kasa.Vga,
                            Kasa.Ip
                            from Kasa 
                            where Cihazid=@id";
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

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
    public class TurController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TurController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JsonResult Get()
        {
            string query = @"select
                                * 
                             from
                             Tur";
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdPlaceController : ControllerBase
    {
        private readonly IConfiguration _configuration;  // adding dependency injection 

        public ProdPlaceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @" select DepartmentId, DepartmentName from ProdPlace 
            ";
            // Getting the data into a data table obj

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Default-connection");
            MySqlDataReader Reader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query, mycon))
                {
                    Reader = mycommand.ExecuteReader();
                    table.Load(Reader);

                    Reader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);

        }


    }
}

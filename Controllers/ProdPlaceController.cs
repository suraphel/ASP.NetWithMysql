using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using mysqltest.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace mysqltest.Controllers
{
    [Route("[controller]")]
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
            //string query = @" select DepartmentId, DepartmentName from ProdPlace 
            //";
           string query = @"select produksjonsplassid,kommunenummer,gaardsnummer, bruksnummer, bygningsnummer, koordinater, koordinatsystem from Produksjonsplass Where produksjonsplassid ";           
         
            // Getting the data into a data table obj
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
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

        [HttpGet("ListOne/ {id}")]
        public JsonResult Get(int id)
        {
            // string query = @" select DepartmentId, DepartmentName from ProdPlace where  DepartmentId = @DepartmentId "; 
            string query = @" select produksjonsplassid, kommunenummer, gaardsnummer, bruksnummer, bygningsnummer, koordinater, koordinatsystem from Produksjonsplass Where produksjonsplassid = @produksjonsplassid ";


            //string query = @" select produksjonsplassid,kommunenummer,gaardsnummer, bruksnummer, bygningsnummer, koordinater, koordinatsystem from Produksjonsplass Where produksjonsplassid   ";

            // Getting the data into a data table obj
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader Reader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycommand = new MySqlCommand(query, mycon))
                {
                    //mycommand.Parameters.AddWithValue("@Departmentid", id);
                    mycommand.Parameters.AddWithValue("@produksjonsplassid", id);

                    Reader = mycommand.ExecuteReader();
                    table.Load(Reader);

                    Reader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }



        [HttpPost]
        //public JsonResult Post(ProdPlaceModel prod)
        public JsonResult Post (Produksjonsplass prod)
        {
            //string query = @" select DepartmentId, DepartmentName from ProdPlace 
            //";
            //string query = @" insert into prodplace (DepartmentName) values (@DepartmentName)";
                                 
            string query = @" insert into Produksjonsplass (kommunenummer,gaardsnummer,bruksnummer,bygningsnummer, koordinater , koordinatsystem, opprettetdato, lastchanged)
                                                    values (@kommunenummer,@gaardsnummer, @bruksnummer,@bygningsnummer,@koordinater,@koordinatsystem, @opprettetdato, @lastchanged )
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader Reader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    //myCommand.Parameters.AddWithValue("@DepartmentName", prod.DepartmentName);
                    myCommand.Parameters.AddWithValue("@kommunenummer", prod.kommunenummer);
                    myCommand.Parameters.AddWithValue("@gaardsnummer", prod.gaardsnummer);
                    myCommand.Parameters.AddWithValue("@bruksnummer", prod.bruksnummer);
                    myCommand.Parameters.AddWithValue("@bygningsnummer", prod.bygningsnummer);
                    myCommand.Parameters.AddWithValue("@koordinater", prod.koordinater);
                    myCommand.Parameters.AddWithValue("@koordinatsystem", prod.koordinatsystem);
                    myCommand.Parameters.AddWithValue("@opprettetdato", prod.opprettetdato);
                    myCommand.Parameters.AddWithValue("@lastchanged", prod.lastchanged);

                    Reader = myCommand.ExecuteReader();
                    table.Load(Reader);

                    Reader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added succesfully");
        }

        [HttpPut]
        //public JsonResult Put(ProdPlaceModel prod)
        public JsonResult Put(Produksjonsplass prod)

        {
            //string query = @" select DepartmentId, DepartmentName from ProdPlace 
            // ";
            //string query = @" update prodplace set 
            //                   DepartmentName = @DepartmentName
            //                   where DepartmentId = @DepartmentId; 
            //";
            string query = @" update Produksjonsplass set 
                                kommunenummer =   @kommunenummer,
                                gaardsnummer = @gaardsnummer,
                                bruksnummer = @bruksnummer, 
                                bygningsnummer = @bygningsnummer, 
                                koordinater = @koordinater, 
                                koordinatsystem = @koordinatsystem
                                where produksjonsplassid = @produksjonsplassid; 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader Reader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    //myCommand.Parameters.AddWithValue("@DepartmentName", prod.DepartmentName);
                    //myCommand.Parameters.AddWithValue("@DepartmentId", prod.DepartmentId);
                    myCommand.Parameters.AddWithValue("@kommunenummer", prod.kommunenummer);
                    myCommand.Parameters.AddWithValue("@gaardsnummer", prod.gaardsnummer); 
                    myCommand.Parameters.AddWithValue("@bruksnummer", prod.bruksnummer);
                    myCommand.Parameters.AddWithValue("@bygningsnummer", prod.bygningsnummer);
                    myCommand.Parameters.AddWithValue("@koordinater", prod.koordinater);
                    myCommand.Parameters.AddWithValue("@koordinatsystem", prod.koordinatsystem);
                    myCommand.Parameters.AddWithValue("@produksjonsplassid", prod.produksjonsplassid);

                    Reader = myCommand.ExecuteReader();
                    table.Load(Reader);

                    Reader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Changed succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //string query = @" select DepartmentId, DepartmentName from ProdPlace 
            //";
            //string query = @" delete from prodplace where 
            //                    DepartmentId = @DepartmentId; 
            //";

            string query = @" delete from Produksjonsplass where 
                                produksjonsplassid = @produksjonsplassid; 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader Reader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    //myCommand.Parameters.AddWithValue("@DepartmentName", prod.DepartmentName);
                    //myCommand.Parameters.AddWithValue("@DepartmentId", id);
                      myCommand.Parameters.AddWithValue("@produksjonsplassid", id);

                    Reader = myCommand.ExecuteReader();
                    table.Load(Reader);

                    Reader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Deleted succesfully");
        }


    }
}

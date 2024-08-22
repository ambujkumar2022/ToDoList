using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace FullStackPr_ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAppController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetNotes")]     
        public JsonResult GetNotes()
        {          
            string Query = "select * from dbo.Notes";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

        return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddNotes")]

        public JsonResult AddNotes([FromForm] string newNotes)
        {
            string Query = "insert into dbo.Notes values(@newNotes)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@newNotes", newNotes);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpDelete]
        [Route("DeleteNotes")]

        public JsonResult DeleteNotes(int id)
        {
            string Query = "delete from dbo.Notes where id = @id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(Query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}

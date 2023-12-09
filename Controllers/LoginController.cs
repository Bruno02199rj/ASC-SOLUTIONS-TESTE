using ApiAuth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace ApiAuth.Controllers
{
    [Route("v1")]
    [ApiController]
   
    
    public class LoginController : Controller
    {
        
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Logar(string username,string senha)
        {
            SqliteConnection sqLiteConnection = new SqliteConnection("Default");
            await sqLiteConnection.OpenAsync();
            SqliteCommand sqliteCommand = sqLiteConnection.CreateCommand();
            
            

            sqliteCommand.CommandText = $"SELECT * FROM usuarios WHERE username = '{username}'AND senha = '{senha}'";

            SqliteDataReader reader = sqliteCommand.ExecuteReader();

           
                if(await reader.ReadAsync())
                {
                return Json(new { Msg = "Logado" });
            }
                return Json(new { Msg = "Verifique suas credenciais!" });
            }

            
        }



    }


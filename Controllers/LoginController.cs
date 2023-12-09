using ApiAuth.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Security.Claims;

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
        //VERIFICAR SE O USUARIO ESTA LOGADO

   
       

        [HttpPost]
        public async Task<IActionResult> Logar(string username,string senha)
        {
            SqliteConnection sqLiteConnection = new SqliteConnection("DataSource=UsersDB.db;Cache=Shared;");
            await sqLiteConnection.OpenAsync();
            SqliteCommand sqliteCommand = sqLiteConnection.CreateCommand();
            
            

            sqliteCommand.CommandText = $"SELECT * FROM Users WHERE Username = '{username}'AND Password = '{senha}'";

            SqliteDataReader reader = sqliteCommand.ExecuteReader();


            if (await reader.ReadAsync())
            {

                int usuarioId = reader.GetInt32(0);
                string nome = reader.GetString(1);

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,usuarioId.ToString()),
                    new Claim(ClaimTypes.Name,nome)
                };

                var identity = new ClaimsIdentity(direitosAcesso, "Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false
                    });
                return Json(new { Msg = "logado" });
            }
            return Json(new { Msg = "não encontrado" });
        }

            
        }



    }


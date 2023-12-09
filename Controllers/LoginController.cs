using ApiAuth.Data;
using ApiAuth.Models;
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

        [HttpPost("cadastrar")]

        public async Task<IActionResult> Cadastrar(Users input)
        {
             _context.Users.Add(input);
            _context.SaveChanges();
            return Ok(new { Id = input.Id,Name=input.Name,Username = input.Username, Password=input.Password});
        }

        [HttpPost]
        public async Task<IActionResult> Logar(string username,string senha)
        {
            SqliteConnection sqLiteConnection = new SqliteConnection("DataSource=UsersDB.db;Cache=Shared;");
            await sqLiteConnection.OpenAsync();
            SqliteCommand sqliteCommand = sqLiteConnection.CreateCommand();
            
            

            sqliteCommand.CommandText = $"SELECT * FROM Users WHERE Username ='{username}'AND Password = '{senha}'";

            SqliteDataReader reader = sqliteCommand.ExecuteReader();


            if (await reader.ReadAsync())
            {
                return Json(new { Msg = "logado" });
            }
            return Json(new { Msg = "não encontrado" });
        }

            
        }



    }


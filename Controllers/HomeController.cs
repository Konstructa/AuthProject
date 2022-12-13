using AuthProject.Models;
using AuthProject.Services;
using AuthProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthProject.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        //nao precisa de Task aqui pq não é assicrono
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null) return NotFound(new { message = "Usuário ou senha inválido" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user,
                token,
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado - {User?.Identity?.Name}";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Evo.API.Data;
using Evo.API.Models;

namespace Evo.Controllers
{
    [Route("v1/configuracao")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext context)
        {
            var funcionario = new Usuarios { Id = 1, Nome = "Funcionario", Senha = "func01", Role = "funcionario" };
            var gerente = new Usuarios { Id = 2, Nome = "Gerente", Senha = "batman", Role = "gerente" };
            var departamento = new Departamento { Id = 1, Nome = "Tecnologia da Informação", Sigla = "TI" };
            var func = new Funcionario { Id = 1, Departamento = departamento, Nome = "Mouse", Foto = "https://", RG = "11.111.111-1", DepartamentoId = 1 };
            context.Usuarios.Add(funcionario);
            context.Usuarios.Add(gerente);
            context.Departamentos.Add(departamento);
            context.Funcionarios.Add(func);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}
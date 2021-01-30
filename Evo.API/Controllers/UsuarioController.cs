using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evo.API.Data;
using Evo.API.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Evo.API.Services;

namespace Evo.API.Controllers
{
    [Route("v1/usuarios")]
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "gerente")]
        public async Task<ActionResult<List<Usuarios>>> Get([FromServices] DataContext context)
        {
            var users = await context
                .Usuarios
                .AsNoTracking()
                .ToListAsync();
            return users;
        }

        [HttpPost]
        [Route("")]        
        //[Authorize(Roles = "gerente")]
        public async Task<ActionResult<Usuarios>> Post(
            [FromServices] DataContext context,
            [FromBody] Usuarios model)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Força o usuário a ser sempre "funcionário"
                model.Role = "funcionario";

                context.Usuarios.Add(model);
                await context.SaveChangesAsync();

                // Esconde a senha
                model.Senha = "";
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "gerente")]
        public async Task<ActionResult<Usuarios>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] Usuarios model)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Usuário não encontrada" });

            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });

            }
        }

        [HttpPost]
        [Route("login")]
        //[AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] DataContext context,
                    [FromBody] Usuarios model)
        {
            var user = await context.Usuarios
                .AsNoTracking()
                .Where(x => x.Nome == model.Nome && x.Senha == model.Senha)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            // Esconde a senha
            user.Senha = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}

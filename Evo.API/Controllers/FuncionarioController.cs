using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evo.API.Data;
using Evo.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Evo.API.Controllers
{
    [Route("v1/funcionarios")]
    public class FuncionarioController : Controller
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Funcionario>>> Get([FromServices] DataContext context)
        {
            var funcionarios = await context.Funcionarios.Include(x => x.Departamento).AsNoTracking().ToListAsync();

            return Ok(funcionarios);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Funcionario>> GetById(int id, [FromServices] DataContext context)
        {
            var funcionario = await context.Funcionarios.Include(x => x.Departamento).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
                return NotFound(new { message = "Funcionario não encontrado" });

            else
                return Ok(funcionario);            
        }

        [HttpGet]
        [Route("departamentos/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Funcionario>>> GetByDepartamento(int id, [FromServices] DataContext context)
        {
            var funcionarios = await context.Funcionarios.Include(x => x.Departamento).AsNoTracking().Where(x => x.DepartamentoId == id).ToListAsync();

            if (funcionarios == null)
                return NotFound(new { message = "Não foi possível encontrar funcionários" });

            else
                return Ok(funcionarios);            
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<Funcionario>> Post([FromBody] Funcionario model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Funcionarios.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível incluir o funcionario" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<Funcionario>> Put(int id, [FromBody] Funcionario model, [FromServices] DataContext context)
        {
            // Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Funcionario não encontrado" });

            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Funcionario>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o funcionario" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o funcionario" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "gerente")]
        public async Task<ActionResult<Funcionario>> Delete(int id, [FromServices] DataContext context)
        {
            var funcionario = await context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
                return NotFound(new { message = "Funcionário não encontrado" });

            try
            {
                context.Funcionarios.Remove(funcionario);
                await context.SaveChangesAsync();
                return Ok(new { message = "Funcionário removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o funcionario" });
            }
        }




    }
}

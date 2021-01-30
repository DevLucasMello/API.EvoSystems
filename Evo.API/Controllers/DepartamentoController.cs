using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evo.API.Data;
using Evo.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Evo.API.Controllers
{
    [Route("v1/departamentos")]
    public class DepartamentoController : Controller
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Departamento>>> Get([FromServices] DataContext context)
        {
            var departamentos = await context.Departamentos.AsNoTracking().ToListAsync();
            
                return Ok(departamentos);            
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Departamento>> GetById(int id, [FromServices] DataContext context)
        {
            var departamento = await context.Departamentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (departamento == null)
                return NotFound(new { message = "Departamento não encontrado" });

            else
                return Ok(departamento);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "gerente")]
        public async Task<ActionResult<Departamento>> Post([FromBody] Departamento model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Departamentos.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar um departamento" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "gerente")]
        public async Task<ActionResult<Departamento>> Put(int id, [FromBody] Departamento model, [FromServices] DataContext context)
        {
            // Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Departamento não encontrado" });

            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Departamento>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o departamento" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar o departamento" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "gerente")]
        public async Task<ActionResult<Departamento>> Delete(int id, [FromServices] DataContext context)
        {
            var category = await context.Departamentos.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound(new { message = "Departamento não encontrado" });

            try
            {
                context.Departamentos.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Departamento removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o departamento" });
            }
        }
    }
}

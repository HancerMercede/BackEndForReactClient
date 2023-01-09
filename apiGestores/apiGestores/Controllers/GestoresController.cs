using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiGestores.Context;
using apiGestores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace apiGestores.Controllers;

[Route("api/[controller]")]
public class GestoresController : Controller
{
    private readonly AppDbContext context;
    public GestoresController(AppDbContext context)
    {
        this.context = context;
    }
    // GET: api/<controller>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await context.gestores_bd.ToListAsync());
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/<controller>/5
    [HttpGet("{id}", Name ="GetGestor")]
    public ActionResult Get(int Id)
    {
        try
        {
            if (Id is 0)
                return BadRequest($"The id:{Id} can be 0");

            var gestor = context.gestores_bd.FirstOrDefault(g => g.Id == Id);
            return Ok(gestor);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST api/<controller>
    [HttpPost]
    public ActionResult Post([FromBody]Gestores_Bd gestor)
    {
        try
        {
            if (gestor is null)
                ArgumentNullException.ThrowIfNull(gestor);
            
            context.gestores_bd.Add(gestor);
            context.SaveChanges();
            return CreatedAtRoute("GetGestor", new { id = gestor.Id }, gestor);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }  
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public ActionResult Put(int Id, [FromBody]Gestores_Bd gestor)
    {
        try
        {
            if (gestor.Id == Id)
            {
                var exist = context.gestores_bd.Any(g => g.Id == Id);

                if (exist)
                {
                    context.Entry(gestor).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { Id = gestor.Id }, gestor);
                }
                return BadRequest($"The id:{Id} dopes not exist.");
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int Id)
    {
        try
        {
            var gestor = context.gestores_bd.FirstOrDefault(g => g.Id == Id);
            if (gestor != null)
            {
                context.gestores_bd.Remove(gestor);
                context.SaveChanges();
                return Ok(Id);
            }
            else
            {
                return BadRequest();
            }
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

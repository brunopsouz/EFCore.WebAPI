using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repo;

        public BatalhaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repo.GetAllBatalha(true);
                return Ok(herois);
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
            
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id, true);
                return Ok(batalha);
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangeAsync())
                {
                    return Ok("Post Success");
                }
            
            }
            catch (Exception ex)
            {

                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Not Success");

        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Batalha model)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repo.Update(model);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Update Success!");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Not deleted");

        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repo.Delete(batalha);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Delete Success!");
                    }
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Not deleted");
        }
    }
}

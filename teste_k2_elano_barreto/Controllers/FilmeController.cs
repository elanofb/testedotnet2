using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
//using TesteElano.Util;
using teste_k2_elano_barreto.Models;

namespace TesteElano.Controllers
{
    /// <summary>
    /// Criar um lançamento de hora
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        IFilmeRepository filmeRepository;
        public FilmeController(IFilmeRepository _filmeRepository)
        {
            filmeRepository = _filmeRepository;
        }

        [HttpGet]
        //[Route("GetFilmes")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filmes = filmeRepository.Get();
                if (filmes == null)
                {
                    return NotFound();
                }
             
                return Ok(filmes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetFilme")]
        public async Task<IActionResult> GetFilme(int? filmeId)
        {
            if (filmeId == null)
            {
                return BadRequest();
            }

            try
            {
                var filme = await filmeRepository.GetFilme(filmeId);

                if (filme == null)
                {
                    return NotFound();
                }

                return Ok(filme);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Um desenvolvedor só pode lançar horas se estiver autenticado (Autenticação JWT com expiração de 5 minutos)
        /// Validações de integridade e duplicidade
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize("Bearer")]
        //[Route("AddFilme")]
        public async Task<IActionResult> Add([FromBody]Filme model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filmeId = await filmeRepository.AddFilme(model);
                    if (filmeId > 0)
                    {                    
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteFilme")]
        public async Task<IActionResult> DeleteFilme(int? filmeId)
        {
            int result = 0;

            if (filmeId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await filmeRepository.DeleteFilme(filmeId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdateFilme")]
        public async Task<IActionResult> UpdateFilme([FromBody]Filme model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await filmeRepository.UpdateFilme(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}

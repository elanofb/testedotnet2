using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.Repository;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;
//using Newtonsoft.Json;

namespace TesteElano.Controllers
{
    /// <summary>
    /// CRUD de projeto (Será considerado um diferencial paginação na listagem)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjetoController : ControllerBase
    {
        IProjetoRepository projetoRepository;
        public ProjetoController(IProjetoRepository _projetoRepository)
        {
            projetoRepository = _projetoRepository;
        }
        
        [HttpGet]
        [Route("GetProjetos")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProjetos()
        {
            try
            {
                var projetos = projetoRepository.GetProjetos();
                if (projetos == null)
                {
                    return NotFound();
                }
                return Ok(projetos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetProjeto")]
        public async Task<IActionResult> GetProjeto(int? projetoId)
        {
            if (projetoId == null)
            {
                return BadRequest();
            }

            try
            {
                var projeto = await projetoRepository.GetProjeto(projetoId);

                if (projeto == null)
                {
                    return NotFound();
                }

                return Ok(projeto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddProjeto")]
        public async Task<IActionResult> AddProjeto([FromBody]Projeto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var projetoId = await projetoRepository.AddProjeto(model);
                    if (projetoId > 0)
                    {
                        return Ok(projetoId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteProjeto")]
        public async Task<IActionResult> DeleteProjeto(int? projetoId)
        {
            int result = 0;

            if (projetoId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await projetoRepository.DeleteProjeto(projetoId);
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
        [Route("UpdateProjeto")]
        public async Task<IActionResult> UpdateProjeto([FromBody]Projeto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await projetoRepository.UpdateProjeto(model);

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

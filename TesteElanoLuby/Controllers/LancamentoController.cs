using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TesteElano.Util;

namespace TesteElano.Controllers
{
    /// <summary>
    /// Criar um lançamento de hora
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LancamentoController : ControllerBase
    {
        ILancamentoRepository lancamentoRepository;
        IDesenvolvedorRepository desenvolvedorRepository;
        IProjetoRepository projetoRepository;
        public LancamentoController(ILancamentoRepository _lancamentoRepository,
                                    IDesenvolvedorRepository _desenvolvedorRepository,
                                    IProjetoRepository _projetoRepository)
        {
            lancamentoRepository = _lancamentoRepository;
            desenvolvedorRepository = _desenvolvedorRepository;
            projetoRepository = _projetoRepository;
        }

        [HttpGet]
        [Route("GetLancamentos")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLancamentos()
        {
            try
            {
                var lancamentos = lancamentoRepository.GetLancamentos();
                if (lancamentos == null)
                {
                    return NotFound();
                }
             
                return Ok(lancamentos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetLancamento")]
        public async Task<IActionResult> GetLancamento(int? lancamentoId)
        {
            if (lancamentoId == null)
            {
                return BadRequest();
            }

            try
            {
                var lancamento = await lancamentoRepository.GetLancamento(lancamentoId);

                if (lancamento == null)
                {
                    return NotFound();
                }

                return Ok(lancamento);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Retornar ranking dos 5 desenvolvedores da semana com maior média de horas trabalhadas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRankingLancamentos")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRankingLancamentos()
        {
            try
            {
                var lancamentos = lancamentoRepository.GetRankingLancamentos();
                if (lancamentos == null)
                {
                    return NotFound();
                }
                
                return Ok(lancamentos);
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
        [Authorize("Bearer")]
        [Route("AddLancamento")]
        public async Task<IActionResult> AddLancamento([FromBody]LancamentoHoras model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Um desenvolvedor só pode lançar horas em projetos que ele esteja vinculado
                    var devInfo = desenvolvedorRepository.GetDesenvolvedor(model.DesenvolvedorId);

                    if (devInfo.ProjetoId == model.ProjetoId)
                    {

                        var lancamentoId = await lancamentoRepository.AddLancamento(model);
                        if (lancamentoId > 0)
                        {
                            //Na confirmação do lançamento de horas, uma notificação é enviada, e o serviço pode estar 
                            //indisponível / instável.Para enviar a notificação, use o endpoint 
                            //abaixo(https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd)
                            var msg = new Common().GetExternalService("https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd");
                            return Ok(msg);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else {
                        return Unauthorized("O desenvolvedor não pertence ao projeto informado.");
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
        [Route("DeleteLancamento")]
        public async Task<IActionResult> DeleteLancamento(int? lancamentoId)
        {
            int result = 0;

            if (lancamentoId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await lancamentoRepository.DeleteLancamento(lancamentoId);
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
        [Route("UpdateLancamento")]
        public async Task<IActionResult> UpdateLancamento([FromBody]LancamentoHoras model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await lancamentoRepository.UpdateLancamento(model);

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

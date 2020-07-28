using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteElano.Models;
using TesteElano.Repository;
using Microsoft.AspNetCore.Mvc;
using TesteElano.Util;

namespace TesteElano.Controllers
{
    /// <summary>
    /// CRUD para desenvolvedor (Será considerado um diferencial paginação na listagem)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DesenvolvedorController : ControllerBase
    {
        IDesenvolvedorRepository desenvolvedorRepository;
        public DesenvolvedorController(IDesenvolvedorRepository _desenvolvedorRepository)
        {
            desenvolvedorRepository = _desenvolvedorRepository;
        }

        [HttpGet]
        [Route("GetDesenvolvedores")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDesenvolvedores()
        {
            try
            {                
                var desenvolvedors = desenvolvedorRepository.GetDesenvolvedores();
                if (desenvolvedors == null)
                {
                    return NotFound();
                }
             
                return Ok(desenvolvedors);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetDesenvolvedor")]
        public IActionResult GetDesenvolvedor(int? desenvolvedorId)
        {
            if (desenvolvedorId == null)
            {
                return BadRequest();
            }

            try
            {
                var desenvolvedor = desenvolvedorRepository.GetDesenvolvedor(desenvolvedorId);

                if (desenvolvedor == null)
                {
                    return NotFound();
                }

                return Ok(desenvolvedor);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }        

        [HttpPost]
        [Route("AddDesenvolvedor")]
        public IActionResult AddDesenvolvedor([FromBody]Desenvolvedor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Validar CPF
                    //Antes de cadastrar um desenvolvedor, devemos validar se seu CPF é válido, para essa validação, pode ser usado o endpoint (https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9)
                    var msg = new Common().GetExternalService("https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9");

                    if (msg.ToLower() == "autorizado")
                    {
                        var desenvolvedorId = desenvolvedorRepository.AddDesenvolvedor(model);
                        if (desenvolvedorId > 0)
                        {
                            return Ok("Novo Desenvolvedor: " + desenvolvedorId.ToString());
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return Unauthorized("Não autorizado!!");
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
        [Route("DeleteDesenvolvedor")]
        public async Task<IActionResult> DeleteDesenvolvedor(int? desenvolvedorId)
        {
            int result = 0;

            if (desenvolvedorId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await desenvolvedorRepository.DeleteDesenvolvedor(desenvolvedorId);
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
        [Route("UpdateDesenvolvedor")]
        public IActionResult UpdateDesenvolvedor([FromBody]Desenvolvedor model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    desenvolvedorRepository.UpdateDesenvolvedor(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
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

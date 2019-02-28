﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senai.InLock.DatabaseFisrt.Solution.Domains;

namespace Senai.InLock.DatabaseFisrt.Solution.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (InLockContext ctx = new InLockContext())
                {
                    return Ok(ctx.Estudios.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/EstudiosComJogos")]
        public IActionResult Get2()
        {
            try
            {
                using (InLockContext ctx = new InLockContext())
                {
                    return Ok(ctx.Estudios.Include("Jogos").ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post(Estudios estudios)
        {
            try
            {
                using (InLockContext ctx = new InLockContext())
                {
                    ctx.Estudios.Add(estudios);
                    ctx.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult Put(Estudios estudio)
        {
            try
            {
                using (InLockContext ctx = new InLockContext())
                {
                    Estudios estudioExiste = ctx.Estudios.Find(estudio.EstudioId);

                    if(estudioExiste == null)
                    {
                        return NotFound("Estudio não encontrado");
                    }

                    ctx.Estudios.Update(estudioExiste);
                    //ctx.Estudios.Attach(estudio);
                    ctx.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{EstudioId}")]
        public IActionResult Delete(int EstudioId)
        {
            try
            {
                using (InLockContext ctx = new InLockContext())
                {
                    Estudios estudio = ctx.Estudios.Find(EstudioId);

                    if(estudio == null)
                    {
                        return NotFound("Estudio não existe");
                    }

                    ctx.Estudios.Remove(estudio);
                    ctx.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
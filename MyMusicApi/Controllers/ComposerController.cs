using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Services;
using MyMusicApi.Ressources;
using MyMusicApi.Services;
using MyMusicApi.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposerController : ControllerBase
    {
        private readonly IComposerService _composerService;
        private readonly IMapper _mapperService;
        public ComposerController(IComposerService composerService, IMapper mapperService)
        {
            _composerService = composerService;
            _mapperService = mapperService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ComposerRessource>>> GetAllComposer()
        {
            var composers = await _composerService.GetAllComposers();
            var composerRessource = _mapperService.Map<IEnumerable<Composer>, IEnumerable<ComposerRessource>>(composers);
            return Ok(composerRessource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComposerRessource>> GetComposerById(string id)
        {
            try
            {
                var composer = await _composerService.GetComposerById(id);
                if(composer == null) return NotFound();
                var composerRessource = _mapperService.Map<Composer,ComposerRessource>(composer);
                return Ok(composerRessource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("")]
        public async Task<ActionResult<Composer>> CreateComposer(SaveComposerRessource saveComposerRessource)
        {
            var validation = new SaveComposerRessourceValidator();
            var validationResult = await validation.ValidateAsync(saveComposerRessource);
            if(!validationResult.IsValid) return BadRequest(validationResult.Errors);

            //mappage
            var composer =  _mapperService.Map<SaveComposerRessource, Composer>(saveComposerRessource);
            //create composer
            var composerNew = await _composerService.Create(composer);
            var composerRessource = _mapperService.Map<Composer, ComposerRessource>(composerNew);
            return Ok(composerRessource);

        }

        [HttpPut("")]
        public async Task<ActionResult<ComposerRessource>> UpdateComposer(string id, SaveComposerRessource saveComposerRessource)
        {
            var validation = new SaveComposerRessourceValidator();
            var validationResult = await validation.ValidateAsync(saveComposerRessource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

           
            //create composer
            var composerUpdate= await _composerService.GetComposerById(id);
            if (composerUpdate == null) return NotFound();
            //mappage
            var composer = _mapperService.Map<SaveComposerRessource, Composer>(saveComposerRessource);
            _composerService.Update(id, composer);
            var composerNewRessource = await _composerService.GetComposerById(id);
            var composerRessource = _mapperService.Map<Composer, ComposerRessource>(composerNewRessource);
            return Ok(composerRessource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ComposerRessource>> DeleteComposerById(string id)
        {

            var composer = await _composerService.GetComposerById(id);
            if(composer == null) return NotFound();
            await _composerService.Delete(id);
            return NoContent();

        }

    }
}

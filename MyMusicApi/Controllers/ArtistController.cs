using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Services;
using MyMusicApi.Ressources;
using MyMusicApi.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _serviceArtist;
        private readonly IMapper _mapperService;

        public ArtistController(IArtistService serviceArtist, IMapper mapperService)
        {
            _serviceArtist = serviceArtist;
            _mapperService = mapperService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistRessource>>> GetAllArtist()
        {
            var artists = await _serviceArtist.GetAllArtists();
            var artistRessource = _mapperService.Map<IEnumerable<Artist>, IEnumerable<ArtistRessource>>(artists);
            return Ok(artistRessource);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistRessource>> GetArtistById(int id)
        {
            try
            {
                var artist = await _serviceArtist.GetArtistById(id);
                if (artist == null) return BadRequest("L'artist n'existe pas");
                var artistRessource = _mapperService.Map<Artist, ArtistRessource>(artist);
                return Ok(artistRessource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<ArtistRessource>> CreateArtist(SaveArtistRessource saveArtistRessource)
        {
            //validation
            var validation = new SaveArtistRessourceValidator();
            var validationResult = await validation.ValidateAsync(saveArtistRessource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            //mappage
            var artist = _mapperService.Map<SaveArtistRessource, Artist>(saveArtistRessource);
            //creation artist
            var artistNew = await _serviceArtist.CreateArtist(artist);
            //mappage
            var artistRessource =  _mapperService.Map<Artist,ArtistRessource>(artistNew);
            return Ok(artistRessource); 
        }

        [HttpPut("")]
        public async Task<ActionResult<ArtistRessource>> UpdateArtist(int id,SaveArtistRessource saveArtistRessource)
        {
            //validation
            var validation = new SaveArtistRessourceValidator();
            var validationResult = await validation.ValidateAsync(saveArtistRessource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            //GetArtisteById
            var artistUpdate = await _serviceArtist.GetArtistById(id);
            if (artistUpdate == null) return NotFound();
            //mappage
            var artist = _mapperService.Map<SaveArtistRessource, Artist>(saveArtistRessource);
            //GetArtisteById
            await _serviceArtist.UpdateArtist(artistUpdate, artist);
            //mappage
            var artistNew = await _serviceArtist.GetArtistById(id);
            var artistNewRessource = _mapperService.Map<Artist,ArtistRessource>(artistNew);
            return Ok(artistNewRessource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _serviceArtist.GetArtistById(id);
            if (artist == null) return NotFound();
            await _serviceArtist.DeleteArtist(artist);
            return NoContent();
        }
    }
}

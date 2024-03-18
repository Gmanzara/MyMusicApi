using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Services;
using MyMusicApi.Ressources;
using MyMusicApi.Services;
using MyMusicApi.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IArtistService _artistService;

        private readonly IMapper _mapperService;
        public MusicController(IMusicService musicService, IMapper mapperService, IArtistService artistService)
        {
            this._musicService = musicService;
            this._mapperService = mapperService;
            this._artistService = artistService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MusicRessource>>> GetAllMusic()
        {
            try
            {
                var musics = await _musicService.GetAllWithArtist();
                var musicRessource = _mapperService.Map<IEnumerable<Music>, IEnumerable<MusicRessource>>(musics);
                return Ok(musicRessource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MusicRessource>> GetMusicById(int id)
        {
            try
            {
                var music = await _musicService.GetMusicById(id);
                if (music == null)
                {
                    return NotFound();
                }
                else
                {
                    var musicRessource = _mapperService.Map<Music, MusicRessource>(music);
                    return Ok(musicRessource);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);           
            }
        }
        [HttpPost()]

        public async Task<ActionResult<MusicRessource>> CreateMusic(SaveMusicRessource saveMusicRessource )
        {
            //Validation
            var validation = new SaveMusicRessourceValidator();
            var validatonResult = await validation.ValidateAsync(saveMusicRessource);
            if(!validatonResult.IsValid) return BadRequest(validatonResult.Errors);

            //Mappage
            var music = _mapperService.Map<SaveMusicRessource, Music>(saveMusicRessource);
            //Creation de Musique
            var newMusic = await _musicService.CreateMusic(music);
            //Mappage
            var musicRessource = _mapperService.Map<Music, MusicRessource>(newMusic);
            return Ok(musicRessource);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MusicRessource>> UpdateMusic(int id,SaveMusicRessource saveMusicRessource)
        {
            //validation
            var validation = new SaveMusicRessourceValidator();
            var validatonResult = await validation.ValidateAsync(saveMusicRessource);
            if (!validatonResult.IsValid) return BadRequest(validatonResult.Errors);

            //si la musique existe depuis le id 
            var musicUpdate = await _musicService.GetMusicById(id);
            if (musicUpdate == null) return BadRequest("La musique n'exiiste pas");
            var music = _mapperService.Map<SaveMusicRessource, Music>(saveMusicRessource);
            await _musicService.UpdateMusic(musicUpdate,music);
            var musicUpdateNew = await _musicService.GetMusicById(id);
            var musicRessourceUpdate = _mapperService.Map<Music, SaveMusicRessource>(musicUpdateNew);
            return Ok(musicRessourceUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMusic(int id)
        {
            var music = await _musicService.GetMusicById(id);
            if (music == null) return BadRequest("La musique n'existe pas");
            await _musicService.DeleteMusic(music);
            return NoContent();
        }

        [HttpGet("Artist/id")]
        public async Task<ActionResult<IEnumerable<MusicRessource>>> GetAllMusicByIdArtist(int id)
        {
            try
            {
                var musics = await _musicService.GetMusicsByArtistId(id);
                if (musics == null) return BadRequest("Pour cet artiste il n'y a pas de musique");
                var musicRessources = _mapperService.Map<IEnumerable<Music>, IEnumerable<MusicRessource>>(musics);
                return Ok(musicRessources);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

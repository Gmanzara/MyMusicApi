using AutoMapper;
using MyMusicApi.Core.Models;
using MyMusicApi.Ressources;

namespace MyMusicApi.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            //Domain(base de données) vers ressource(View) 
            CreateMap<Music, MusicRessource>();
            CreateMap<Artist, ArtistRessource>();
            CreateMap<Music, SaveMusicRessource>();
            CreateMap<Artist,SaveArtistRessource>();
            CreateMap<Composer, ComposerRessource>();
            CreateMap<Composer, SaveComposerRessource>();
            //Ressource vers domain
            CreateMap<MusicRessource, Music>();
            CreateMap<ArtistRessource, Artist>();
            CreateMap<SaveMusicRessource, Music>();
            CreateMap<SaveArtistRessource,Artist>();
            CreateMap<ComposerRessource, Composer>();
            CreateMap<SaveComposerRessource,Composer>();
        }

    }
}

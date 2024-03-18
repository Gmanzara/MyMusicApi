using System;
using System.Threading.Tasks;

namespace MyMusicApi.Ressources
{
    public class SaveArtistRessource
    {
        public string Name { get; set; }

        internal Task ValidationAsync(SaveArtistRessource saveArtistRessource)
        {
            throw new NotImplementedException();
        }
    }
}

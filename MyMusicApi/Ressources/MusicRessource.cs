namespace MyMusicApi.Ressources
{
    public class MusicRessource
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public ArtistRessource Artist { get; set; }
    }
}

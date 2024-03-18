using MongoDB.Bson;

namespace MyMusicApi.Ressources
{
    public class ComposerRessource
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

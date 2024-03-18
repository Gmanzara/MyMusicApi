using FluentValidation;
using MongoDB.Driver;
using MyMusicApi.Ressources;

namespace MyMusicApi.Validation
{
    public class SaveArtistRessourceValidator : AbstractValidator<SaveArtistRessource>
    {
        public SaveArtistRessourceValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MaximumLength(50);
        }
    }
}

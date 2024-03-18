using FluentValidation;
using MyMusicApi.Ressources;

namespace MyMusicApi.Validation
{
    public class SaveMusicRessourceValidator : AbstractValidator<SaveMusicRessource>
    {
        public SaveMusicRessourceValidator()
        {
            RuleFor(m=>m.Name).NotEmpty().MaximumLength(50);
            RuleFor(m => m.ArtistId).NotEmpty().WithMessage("'Artist Id' must no be 0");
        }
    }
}

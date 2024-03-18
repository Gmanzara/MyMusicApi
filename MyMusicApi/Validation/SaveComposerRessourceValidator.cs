using FluentValidation;
using MyMusicApi.Ressources;

namespace MyMusicApi.Validation
{
    public class SaveComposerRessourceValidator : AbstractValidator<SaveComposerRessource>
    {
        public SaveComposerRessourceValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(m=>m.LastName).NotEmpty().MaximumLength(50);
        }

    }
}

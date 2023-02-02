using FluentValidation;

namespace NZWalks_Api.Validators
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}

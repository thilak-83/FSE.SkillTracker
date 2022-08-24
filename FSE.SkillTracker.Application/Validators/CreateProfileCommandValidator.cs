using FluentValidation;
using FSE.SkillTracker.Application.Features.Profile.Commands;
using FSE.SkillTracker.Domain.Entities;

namespace FSE.SkillTracker.Application.Validators
{
    public sealed class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileCommandValidator()
        {
            RuleFor(x => x.Name)
                //.Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(5, 30)
                .WithMessage("{PropertyName} must have only characters.");

            RuleFor(x => x.AssociateId)
                //.Cascade(CascadeMode.Stop)
                .NotNull()
                .Length(5, 30)
                .Must(x => x.StartsWith("CTS"))
                .WithMessage("{PropertyName} must starts with CTS.");
                

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Mobile)
                //.Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(10)
                .Matches(@"^\d+$");

            RuleForEach(x => x.SkillExpertise)
                .NotNull()
                .NotEmpty()
                .SetValidator(new SkillExpertiseValidator());
        }
    }

    public sealed class SkillExpertiseValidator : AbstractValidator<SkillExpertise>
    {
        public SkillExpertiseValidator()
        {
            RuleFor(x => x.Expertise)
                //.Must(IsValidName)
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(20);
        }

        private bool IsValidName(int expertise)
        {
            return expertise.GetType().Name != typeof(int).Name ? true : false;
        }
    }
}

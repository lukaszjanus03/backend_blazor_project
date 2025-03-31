using FluentValidation;
using WebAPI.Dto;

namespace WebAPI.Validators;


public class NewQuizItemValidatedDtoValidator : AbstractValidator<NewQuizItemValidatedDto>
{
    public NewQuizItemValidatedDtoValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Pole 'Question' nie może być puste.");

        RuleFor(x => x.Options)
            .NotEmpty().WithMessage("Pole 'Options' musi zawierać przynajmniej jedną opcję.")
            .Must(options => options.Count >= 2).WithMessage("Muszą być przynajmniej dwie opcje odpowiedzi.");

        RuleFor(x => x.CorrectOptionIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Indeks poprawnej odpowiedzi nie może być ujemny.")
            .Must((dto, index) => index < dto.Options.Count)
            .WithMessage("Indeks poprawnej odpowiedzi musi mieścić się w zakresie dostępnych opcji.");
    }
}
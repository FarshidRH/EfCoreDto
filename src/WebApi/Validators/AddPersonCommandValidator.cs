namespace EfCoreDto.WebApi.Validators;

public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
	public AddPersonCommandValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(128);

		RuleFor(x => x.LastName).NotEmpty().MaximumLength(128);
	}
}

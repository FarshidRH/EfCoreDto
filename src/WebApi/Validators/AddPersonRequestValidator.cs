namespace EfCoreDto.WebApi.Validators;

public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
{
	public AddPersonRequestValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(128);

		RuleFor(x => x.LastName).NotEmpty().MaximumLength(128);
	}
}

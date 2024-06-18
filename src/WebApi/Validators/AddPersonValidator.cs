namespace EfCoreDto.WebApi.Validators;

public class AddPersonValidator : AbstractValidator<AddPersonRequest>
{
	public AddPersonValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(128);

		RuleFor(x => x.LastName).NotEmpty().MaximumLength(128);
	}
}

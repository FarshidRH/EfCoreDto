namespace EfCoreDto.WebApi.Validators;

public class SetPersonAddressRequestValidator : AbstractValidator<SetPersonAddressRequest>
{
	public SetPersonAddressRequestValidator()
	{
		RuleFor(x => x.AddressLine1).NotEmpty();

		RuleFor(x => x.PostalCode)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.Length(10);

		RuleFor(x => x.City).NotEmpty().MaximumLength(128);

		RuleFor(x => x.Country).NotEmpty().MaximumLength(128);
	}
}

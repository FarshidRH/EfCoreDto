namespace EfCoreDto.WebApi.Validators;

public class AddVehicleRequestValidator : AbstractValidator<AddVehicleRequest>
{
	public AddVehicleRequestValidator()
	{
		RuleFor(x => x.Vin).NotEmpty();

		RuleFor(x => x.PersonId).GreaterThan(0);
	}
}

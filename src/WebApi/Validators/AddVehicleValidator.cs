namespace EfCoreDto.WebApi.Validators;

public class AddVehicleValidator : AbstractValidator<AddVehicleRequest>
{
	public AddVehicleValidator()
	{
		RuleFor(x => x.Vin).NotEmpty();

		RuleFor(x => x.PersonId).GreaterThan(0);
	}
}

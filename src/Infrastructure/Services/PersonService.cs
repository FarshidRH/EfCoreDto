namespace EfCoreDto.Infrastructure.Services;

internal sealed class PersonService(AppDbContext dbContext) : IPersonService
{
	private readonly AppDbContext _dbContext = dbContext;

	public async Task<Result<PersonDTO>> AddPersonAsync(
		string firstName, string lastName, CancellationToken cancellationToken)
	{
		try
		{
			/* only for testing of CancellationToken.
			//await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
			*/

			var newPerson = Person.Create(firstName, lastName);
			await _dbContext.AddAsync(newPerson, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return Result.Success(newPerson.ToModel());
		}
		catch (Exception exception)
		{
			return Result.Fail<PersonDTO>(exception);
		}
	}

	public async Task<Result<PersonDTO>> GetPersonByIdAsync(int id)
	{
		Person? person = await _dbContext.PersonWithIdAsync(id, asNoTracking: true);

		return person != null
			? Result.Success(person.ToModel())
			: Result.Fail<PersonDTO>(PersonErrors.PersonNotFound);
	}

	public async Task<Result<AddressDTO[]>> GetPersonsAddressesAsync(int personId)
	{
		Person? person = await _dbContext.PersonWithIdAsync(personId, asNoTracking: true);

		return person != null
			? Result.Success(person.GetAllAddresses().Select(x => x.ToModel()).ToArray())
			: Result.Fail<AddressDTO[]>(PersonErrors.PersonNotFound);
	}

	public async Task<Result<AddressDTO>> SetPersonsAddressAsync(
		int personId,
		AddressType addressType,
		string addressLine1,
		string? addressLine2,
		string postalCode,
		string city,
		string country)
	{
		Person? person = await _dbContext.PersonWithIdAsync(personId);
		if (person == null)
		{
			return Result.Fail<AddressDTO>(PersonErrors.PersonNotFound);
		}

		try
		{
			Address? newAddress = addressType switch
			{
				AddressType.Delivery => person.SetDeliveryAddress(addressLine1, addressLine2, postalCode, city, country),
				AddressType.Invoice => person.SetInvoiceAddress(addressLine1, addressLine2, postalCode, city, country),
				_ => null,
			};

			if (newAddress == null)
			{
				return Result.Fail<AddressDTO>(AddressErrors.AddressTypeInUnknown);
			}

			await _dbContext.SaveChangesAsync();
			return Result.Success(newAddress.ToModel());
		}
		catch (Exception exception)
		{
			return Result.Fail<AddressDTO>(exception);
		}
	}
}

namespace EfCoreDto.Core.Services;

public interface IPersonService
{
	Task<Result<PersonDto>> AddPersonAsync(string firstName, string lastName);
	Task<Result<PersonDto>> GetPersonByIdAsync(int id);
	Task<Result<AddressDto[]>> GetPersonsAddressesAsync(int personId);
	Task<Result<AddressDto>> SetPersonsAddressAsync(
		int personId,
		AddressType type,
		string addressLine1,
		string addressLine2,
		string postalCode,
		string city,
		string country);
}

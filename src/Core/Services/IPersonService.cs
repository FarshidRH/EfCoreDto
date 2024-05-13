namespace EfCoreDto.Core.Services;

public interface IPersonService
{
	Task<Result<PersonDTO>> AddPersonAsync(string firstName, string lastName);
	Task<Result<PersonDTO>> GetPersonByIdAsync(int id);
	Task<Result<AddressDTO[]>> GetPersonsAddressesAsync(int personId);
	Task<Result<AddressDTO>> SetPersonsAddressAsync(
		int personId,
		AddressType type,
		string addressLine1,
		string addressLine2,
		string postalCode,
		string city,
		string country);
}

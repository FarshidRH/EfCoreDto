namespace EfCoreDto.Core.Services;

public interface IPersonService
{
    Task<Result<PersonDTO>> AddPerson(string firstName, string lastName);
    Task<Result<PersonDTO>> GetPersonById(int id);
    Task<Result<AddressDTO[]>> GetPersonsAddresses(int personId);
    Task<Result<AddressDTO>> SetPersonsAddress(
        int personId,
        AddressType type,
        string addressLine1,
        string addressLine2,
        string postalCode,
        string city,
        string country);
}

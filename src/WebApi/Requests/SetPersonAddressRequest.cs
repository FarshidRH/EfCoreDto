namespace EfCoreDto.WebApi.Requests;

public record SetPersonAddressRequest(
	AddressType Type,
	string AddressLine1,
	string? AddressLine2,
	string PostalCode,
	string City,
	string Country);

namespace EfCoreDto.Core.DTOs;

public record PersonDTO(int Id, NameDTO Name, AddressDTO? DeliveryAddress, AddressDTO? InvoiceAddress);

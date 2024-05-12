namespace EfCoreDto.Core.DTOs;

public record PersonDto(int Id, NameDto Name, AddressDto? DeliveryAddress, AddressDto? InvoiceAddress);

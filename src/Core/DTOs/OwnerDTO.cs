namespace EfCoreDto.Core.DTOs;

public record OwnerDto(int Id, NameDto Name, DateTime From, DateTime? To);

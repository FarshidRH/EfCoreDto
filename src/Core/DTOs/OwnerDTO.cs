namespace EfCoreDto.Core.DTOs;

public record OwnerDTO(int Id, NameDTO Name, DateTime From, DateTime? To);

namespace EfCoreDto.Core.Extensions;

public static class DtoExtenstion
{
	public static VehicleDto ToModel(this Vehicle vehicle)
	{
		ArgumentNullException.ThrowIfNull(vehicle);

		return new(vehicle.VIN.Value,
				vehicle.CurrentOwner?.ToModel(),
				vehicle.PreviousOwners.OrderBy(x => x.To).Select(x => x.ToModel()).ToArray());
	}

	public static OwnerDto ToModel(this Owner owner)
	{
		ArgumentNullException.ThrowIfNull(owner);

		return new(owner.Id, owner.Name.ToModel(), owner.From, owner.To);
	}

	public static PersonDto ToModel(this Person person)
	{
		ArgumentNullException.ThrowIfNull(person);

		return new(((IHaveId<int>)person).Id, person.Name.ToModel(),
				person.DeliveryAddress?.ToModel(),
				person.InvoiceAddress?.ToModel());
	}

	public static AddressDto ToModel(this Address address)
	{
		ArgumentNullException.ThrowIfNull(address);

		var addressType = address.GetType().Name.Replace("Address", "", StringComparison.Ordinal);

		return new(Enum.Parse<AddressType>(addressType),
				address.AddressLine1, address.AddressLine2, address.PostalCode,
				address.City, address.Country, address.IsCurrent);
	}

	public static NameDto ToModel(this Name name)
	{
		ArgumentNullException.ThrowIfNull(name);

		return new(name.FirstName, name.LastName);
	}
}

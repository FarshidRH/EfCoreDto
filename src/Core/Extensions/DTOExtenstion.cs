namespace EfCoreDto.Core.Extensions;

public static class DTOExtenstion
{
	public static VehicleDTO ToModel(this Vehicle vehicle)
	{
		ArgumentNullException.ThrowIfNull(vehicle);

		return new(vehicle.VIN.Value,
				vehicle.CurrentOwner?.ToModel(),
				vehicle.PreviousOwners.OrderBy(x => x.To).Select(x => x.ToModel()).ToArray());
	}

	public static OwnerDTO ToModel(this Owner owner)
	{
		ArgumentNullException.ThrowIfNull(owner);

		return new(owner.Id, owner.Name.ToModel(), owner.From, owner.To);
	}

	public static PersonDTO ToModel(this Person person)
	{
		ArgumentNullException.ThrowIfNull(person);

		return new(((IHaveId<int>)person).Id, person.Name.ToModel(),
				person.DeliveryAddress?.ToModel(),
				person.InvoiceAddress?.ToModel());
	}

	public static AddressDTO ToModel(this Address address)
	{
		ArgumentNullException.ThrowIfNull(address);

		return new(address.GetType().Name.Replace("Address", "", StringComparison.Ordinal),
				address.AddressLine1, address.AddressLine2, address.PostalCode,
				address.City, address.Country, address.IsCurrent);
	}

	public static NameDTO ToModel(this Name name)
	{
		ArgumentNullException.ThrowIfNull(name);

		return new(name.FirstName, name.LastName);
	}
}

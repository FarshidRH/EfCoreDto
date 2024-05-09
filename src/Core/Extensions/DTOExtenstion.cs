namespace EfCoreDto.Core.Extensions;

public static class DTOExtenstion
{
    public static VehicleDTO ToModel(this Vehicle vehicle)
        => new(vehicle.VIN.Value,
            vehicle.CurrentOwner?.ToModel(),
            vehicle.PreviousOwners.OrderBy(x => x.To).Select(x => x.ToModel()).ToArray());

    public static OwnerDTO ToModel(this Owner owner)
        => new(owner.Id, owner.Name.ToModel(), owner.From, owner.To);

    public static PersonDTO ToModel(this Person person)
        => new(((IHaveId<int>)person).Id, person.Name.ToModel(),
            person.DeliveryAddress?.ToModel(),
            person.InvoiceAddress?.ToModel());

    public static AddressDTO ToModel(this Address address)
        => new(Enum.Parse<AddressType>(address.GetType().Name.Replace("Address", "")),
            address.AddressLine1, address.AddressLine2, address.PostalCode,
            address.City, address.Country, address.IsCurrent);

    public static NameDTO ToModel(this Name name)
        => new(name.FirstName, name.LastName);
}

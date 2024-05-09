namespace EfCoreDto.Core.Entities;

public class Person : IHaveId<int>
{
    private int _id = default;
    private List<Address> _addresses = [];

#pragma warning disable CS8618 // Required by Entity Framework
    private Person() { }
#pragma warning restore CS8618

    public static Person Create(string firstName, string lastName)
        => new() { Name = new(firstName, lastName) };

    public Address[] GetAllAddresses() => [.. _addresses];

    public Address SetDeliveryAddress(string addressLine1, string addressLine2, string postalCode, string city, string country)
        => SetAddress<DeliveryAddress>(addressLine1, addressLine2, postalCode, city, country);

    public Address SetInvoiceAddress(string addressLine1, string addressLine2, string postalCode, string city, string country)
        => SetAddress<InvoiceAddress>(addressLine1, addressLine2, postalCode, city, country);

    private T SetAddress<T>(
        string addressLine1,
        string addressLine2,
        string postalCode,
        string city,
        string country) where T : Address, new()
    {
        _addresses.OfType<T>().FirstOrDefault(x => x.IsCurrent)?.Disable();

        var newAddress = Address.Create<T>(addressLine1, addressLine2, postalCode, city, country, true);
        _addresses.Add(newAddress);

        var currentAddresses = _addresses.OfType<T>().ToArray();
        if (currentAddresses.Length > 3)
        {
            for (var i = 0; i < currentAddresses.Length - 3; i++)
            {
                _addresses.Remove(currentAddresses[i]);
            }
        }

        return newAddress;
    }

    public Name Name { get; set; }

    public Address? DeliveryAddress
        => _addresses.OfType<DeliveryAddress>().FirstOrDefault(x => x.IsCurrent);
    public Address? InvoiceAddress
        => _addresses.OfType<InvoiceAddress>().FirstOrDefault(x => x.IsCurrent);

    int IHaveId<int>.Id => _id;
}
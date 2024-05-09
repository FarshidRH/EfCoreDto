namespace EfCoreDto.Core.Entities;

public abstract class Address
{
    protected Address() { }

    public static T Create<T>(
        string addresssLine1, string addressLine2,
        string postalCode, string city, string country,
        bool isCurrent) where T : Address, new()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(addresssLine1);
        ArgumentException.ThrowIfNullOrWhiteSpace(postalCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);

        return new T
        {
            AddressLine1 = addresssLine1,
            AddressLine2 = addressLine2,
            PostalCode = postalCode,
            City = city,
            Country = country,
            IsCurrent = isCurrent
        };
    }

    public void Disable() => IsCurrent = false;

    public string AddressLine1 { get; private set; } = string.Empty;
    public string? AddressLine2 { get; private set; }
    public string PostalCode { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public bool IsCurrent { get; private set; } = false;
}

public class DeliveryAddress : Address { }

public class InvoiceAddress : Address { }
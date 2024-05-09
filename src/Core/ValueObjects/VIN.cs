using System.Text.RegularExpressions;

namespace EfCoreDto.Core.ValueObjects;

public record VIN(string Value)
{
    private static readonly Regex VinValidationRegex = new("[A-HJ-NPR-Z0-9]{17}");

    public static VIN Create(string value)
    {
        if (!IsValid(value))
        {
            throw new InvalidVinException();
        }

        return new VIN(value);
    }

    public static bool TryCreate(string value, out VIN? vin)
    {
        if (!IsValid(value))
        {
            vin = null;
            return false;
        }

        vin = new VIN(value);
        return true;
    }

    public static bool IsValid(string value) => VinValidationRegex.IsMatch(value);
}

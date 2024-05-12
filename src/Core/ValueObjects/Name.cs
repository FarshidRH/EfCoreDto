namespace EfCoreDto.Core.ValueObjects;

public record Name
{
	public Name(string firstName, string lastName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
		ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

		this.FirstName = firstName;
		this.LastName = lastName;
	}

	public string FirstName { get; }
	public string LastName { get; }
}

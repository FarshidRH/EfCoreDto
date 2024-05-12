namespace EfCoreDto.Core.Entities;

public sealed class Owner
{
	private readonly Person _person;

#pragma warning disable CS8618
	private Owner() { } /* Required by Entity Framework. */
#pragma warning restore CS8618

	private Owner(Person person) => _person = person;

	public static Owner Create(Person person) => new(person);

	public void EndOwnership() => this.To = DateTime.Today;

	public int Id => ((IHaveId<int>)_person).Id;
	public Name Name => _person.Name;
	public DateTime From { get; } = DateTime.Today;
	public DateTime? To { get; private set; }
}

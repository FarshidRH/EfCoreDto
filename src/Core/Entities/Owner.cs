namespace EfCoreDto.Core.Entities;

public class Owner
{
    private Person _person;

#pragma warning disable CS8618 // Required by Entity Framework
    private Owner() { }
#pragma warning restore CS8618

    private Owner(Person person)
    {
        _person = person;
    }

    public static Owner Create(Person person) => new(person);

    public void EndOwnership() => To = DateTime.Today;

    public int Id => ((IHaveId<int>)_person).Id;
    public Name Name => _person.Name;
    public DateTime From { get; private set; } = DateTime.Today;
    public DateTime? To { get; private set; }
}
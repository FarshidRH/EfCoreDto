namespace EfCoreDto.Core.Entities;

public class Vehicle
{
    private List<Owner> _owners = [];

#pragma warning disable CS8618 // Required by Entity Framework
    private Vehicle() { }
#pragma warning restore CS8618

    private Vehicle(VIN vin)
    {
        VIN = vin;
    }

    public static Vehicle Create(VIN vin, Person owner)
    {
        var vehicle = new Vehicle(vin);
        vehicle._owners.Add(Owner.Create(owner));
        return vehicle;
    }

    public Owner SetOwner(Person newOwner)
    {
        if (CurrentOwner != null)
        {
            if (((IHaveId<int>)newOwner).Id == CurrentOwner.Id)
            {
                throw new DuplicateOwnerException();
            }
            CurrentOwner!.EndOwnership();
        }

        var owner = Owner.Create(newOwner);
        _owners.Add(owner);
        return owner;
    }

    public VIN VIN { get; private set; }

    public Owner? CurrentOwner => _owners.FirstOrDefault(x => x.To == null);
    public Owner[] PreviousOwners => _owners.Where(x => x.To != null).ToArray();
}

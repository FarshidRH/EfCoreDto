namespace EfCoreDto.Core.Entities;

public sealed class Vehicle
{
	private readonly List<Owner> _owners = [];

	// Required by Entity Framework.
	private Vehicle() { }

	private Vehicle(VIN vin) => this.VIN = vin;

	public static Vehicle Create(VIN vin, Person owner)
	{
		var vehicle = new Vehicle(vin);
		vehicle._owners.Add(Owner.Create(owner));
		return vehicle;
	}

	public Owner SetOwner(Person newOwner)
	{
		ArgumentNullException.ThrowIfNull(newOwner);

		if (this.CurrentOwner != null)
		{
			if (((IHaveId<int>)newOwner).Id == this.CurrentOwner.Id)
			{
				throw new DuplicateOwnerException();
			}
			this.CurrentOwner!.EndOwnership();
		}

		var owner = Owner.Create(newOwner);
		_owners.Add(owner);
		return owner;
	}

	public VIN VIN { get; }

	public Owner? CurrentOwner => _owners.Find(x => x.To == null);
	public IReadOnlyCollection<Owner> PreviousOwners => [.. _owners.Where(x => x.To != null)];
}

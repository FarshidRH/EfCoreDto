namespace EfCoreDto.Infrastructure.Handlers.CommandHandlers;

internal sealed class AddPersonCommandHandler(AppDbContext dbContext)
	: IRequestHandler<AddPersonCommand, Result<PersonDTO>>
{
	private readonly AppDbContext _dbContext = dbContext;

	public async Task<Result<PersonDTO>> Handle(
		AddPersonCommand request, CancellationToken cancellationToken)
	{
		try
		{
			/* only for testing of CancellationToken.
			//await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
			*/

			var newPerson = Person.Create(request.FirstName, request.LastName);
			await _dbContext.AddAsync(newPerson, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return Result.Success(newPerson.ToModel());
		}
		catch (Exception exception)
		{
			return Result.Fail<PersonDTO>(exception);
		}
	}
}

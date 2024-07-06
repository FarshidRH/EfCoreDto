namespace EfCoreDto.Infrastructure.Handlers.QueryHandlers;

internal sealed class GetPersonByIdQueryHandler(AppDbContext dbContext)
	: IRequestHandler<GetPersonByIdQuery, Result<PersonDTO>>
{
	private readonly AppDbContext _dbContext = dbContext;

	public async Task<Result<PersonDTO>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
	{
		Person? person = await _dbContext.PersonWithIdAsync(request.Id, asNoTracking: true);

		return person != null
			? Result.Success(person.ToModel())
			: Result.Fail<PersonDTO>(PersonErrors.PersonNotFound);
	}
}

namespace EfCoreDto.Core.Requests.Queries;

public sealed record GetPersonByIdQuery(int Id)
	: IRequest<Result<PersonDTO>>;

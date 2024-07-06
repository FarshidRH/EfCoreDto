namespace EfCoreDto.Core.Requests.Commands;

public sealed record AddPersonCommand(string FirstName, string LastName)
	: IRequest<Result<PersonDTO>>;

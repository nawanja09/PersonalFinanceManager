using MediatR;

namespace Finance.Application.Features.Authentication.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<RegisterResponse>;
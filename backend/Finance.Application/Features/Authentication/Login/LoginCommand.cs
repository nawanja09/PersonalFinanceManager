using MediatR;

namespace Finance.Application.Features.Authentication.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResponse>;
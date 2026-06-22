using Finance.Application.Abstractions.Persistence;
using Finance.Application.Abstractions.Services;
using MediatR;

namespace Finance.Application.Features.Authentication.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new Exception("Invalid email or password.");
        }

        bool isPasswordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password.");
        }

        string token =
            _jwtTokenGenerator.GenerateToken(
                user.Id,
                user.Email);

        return new LoginResponse
        {
            Token = token
        };
    }
}
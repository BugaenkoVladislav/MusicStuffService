using Grpc.Core;
using MusicStuffBackend;

namespace AuthService.Services;

public class AuthMicroservice:Auth.AuthBase
{
    public override Task<Token> GetToken(LoginPassword request, ServerCallContext context)
    {
        return base.GetToken(request, context);
    }
}
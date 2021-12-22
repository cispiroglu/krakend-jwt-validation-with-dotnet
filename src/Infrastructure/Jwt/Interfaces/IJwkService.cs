using Jwt.Models;

namespace Jwt.Interfaces;

public interface IJwkService
{
    JwkWrapper GetJwk();
}
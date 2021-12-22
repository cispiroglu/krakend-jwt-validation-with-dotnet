namespace Jwt.Models;

public class JwkWrapper
{
    public JwkWrapper()
    {
        keys = new List<Jwk>();
    }

    public List<Jwk> keys { get; }
}

public class Jwk
{
    public string kty { get; set; }
        
    public string alg { get; set; }
        
    public string kid { get; set; }
        
    public string use { get; set; }
        
    public string e { get; set; }
        
    public string n { get; set; }
}
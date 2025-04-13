using System.Security.Cryptography;
using TodoApp.Application.Interfaces;

namespace TodoApp.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    private const char Delimiter = ':';

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            Algorithm,
            KeySize
        );

        return string.Join(
            Delimiter,
            Convert.ToBase64String(hash),
            Convert.ToBase64String(salt),
            Iterations,
            Algorithm
        );
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var elements = passwordHash.Split(Delimiter);
        var hash = Convert.FromBase64String(elements[0]);
        var salt = Convert.FromBase64String(elements[1]);
        var iterations = int.Parse(elements[2]);
        var algorithm = new HashAlgorithmName(elements[3]);
        
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt, 
            iterations,
            algorithm,
            KeySize
        );

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}
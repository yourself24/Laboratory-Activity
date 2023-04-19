using Assign.DAL.Models;

namespace Assign.BLL.Services.Contracts;

public interface ITokenService
{
    Task<Token> CreateToken(Token token);
    Task<List<Token>> GetAll();
    bool TokenUsed(string token);
    Task DeleteToken(int id);
}
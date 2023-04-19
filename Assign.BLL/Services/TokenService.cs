using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assign.BLL.Services.Contracts;
using Assign.DAL.Models;
using Assign.DAL.Repos.Contracts;

namespace Assign.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IGenericRepo<Token> _tokenRepo;

        public TokenService(IGenericRepo<Token> tokenRepo)
        {
            _tokenRepo = tokenRepo;
        }

        public async Task<Token> CreateToken(Token token)
        {
            return await _tokenRepo.CreateToken(token);
        }

        public async Task<List<Token>> GetAll()
        {
            try
            {
                return await _tokenRepo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteToken(int id)
        {
            await _tokenRepo.DeleteToken(id);
        }
        public  bool TokenUsed(string token)
        {
            var tokenvar =  _tokenRepo.GetTokenByString(token);
            if (tokenvar == null)
            {
                return false;
            }
            DeleteToken(tokenvar.TokId);
            return true;
            
        }
    }
}

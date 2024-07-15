using JWT_Bearer_Token.FakeDatabase;
using JWT_Bearer_Token.Interface;
using JWT_Bearer_Token.JWT.Services;
using JWT_Bearer_Token.Model;

namespace JWT_Bearer_Token.Repository
{
    public class AuthentificationCustomRepository : IAuthentificationCustomRepository
    {
        private readonly UserModel[] _database;
        private readonly JWTGenerationService _jWTGenerationService;
        public AuthentificationCustomRepository(JWTGenerationService jWTGenerationService)
        {
            _database = MockupDatabase.GetDataUser();
            _jWTGenerationService = jWTGenerationService;
        }

        public IResult Authentification(string mail, string password)
        {
            var result = _database.Where(u => u.Mail == mail && u.Password == password).FirstOrDefault();

            if (result is null)
                return TypedResults.BadRequest(new { Message = "Identification failed !" });

            string token = _jWTGenerationService.GenerateToken(result.Id.ToString(), result.Mail, result.Pseudo, result.Role);

            return  TypedResults.Ok(new {token} );
        }
    }
}

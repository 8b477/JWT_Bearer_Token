namespace JWT_Bearer_Token.Interface
{
    public interface IAuthentificationCustomRepository
    {
        IResult Authentification(string mail, string password);
    }
}

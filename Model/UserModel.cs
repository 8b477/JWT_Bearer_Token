namespace JWT_Bearer_Token.Model
{
    public class UserModel
    {
        public UserModel(int id, string pseudo, string mail, string password, string role)
        {
            Id = id;
            Pseudo = pseudo;
            Mail = mail;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

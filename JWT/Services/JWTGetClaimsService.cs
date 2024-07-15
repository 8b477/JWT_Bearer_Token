using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace JWT_Bearer_Token.JWT.Services
{
    public class JWTGetClaimsService
    {

        #region Injection
        private readonly IHttpContextAccessor _context;

        public JWTGetClaimsService(IHttpContextAccessor context) => _context = context;
        #endregion



        /// <summary>
        /// Retrieves the user ID from the JWT token.
        /// </summary>
        /// <returns>
        /// The user ID as an integer if successfully parsed from the token, or 0 if parsing fails or the claim is not present.
        /// </returns>
        public int GetIdUserToken()
        {
            string? identifiant = _context?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(identifiant, out int id))
            {
                return id;
            }
            return 0;
        }


        /// <summary>
        /// Retrieves the user's pseudo (username) from the JWT token.
        /// </summary>
        /// <returns>
        /// The user's pseudo as a string if present in the token, or an empty string if not found.
        /// </returns>
        public string GetPseudoUserToken()
        {
            string? name = _context?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return name ?? string.Empty;
        }


        /// <summary>
        /// Retrieves the user's role from the JWT token.
        /// </summary>
        /// <returns>
        /// The user's role as a string if present in the token, or an empty string if not found.
        /// </returns>
        public string GetRoleUserToken()
        {
            string? role = _context?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role ?? string.Empty;
        }


        /// <summary>
        /// Retrieves the user's email address from the JWT token.
        /// </summary>
        /// <returns>
        /// The user's email address as a string if present in the token, or an empty string if not found.
        /// </returns>
        public string GetEmailUserToken()
        {
            string? email = _context?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return email ?? string.Empty;
        }

    }
}

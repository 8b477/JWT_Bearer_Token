# Sécurisation des endpoints avec JWT Bearer Token 🔒
Ce projet donne une base de conception pour sécuriser des endpoints API en utilisant JWT (JSON Web Token) avec un Bearer Token en ASP.NET Core.
## Configuration 🛠️

Dans le fichier appsettings.json, configurez les paramètres JWT :
```json
"JWT": {
"Key": "",
"Issuer": "https://localhost:7054", // L'identifiant de l'émetteur du jeton.
"Audience": "http://localhost:4200", // L'identifiant de la réception du jeton.
"DurationInMinutes": 180
}
```
**Note : La clé sera générée automatiquement si elle n'est pas spécifiée.** 

------------------

Dans Program.cs, ajoutez les services nécessaires :
```c#
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IAuthentificationCustomRepository, AuthentificationCustomRepository>();
builder.Services.AddScoped<JWTGenerationService>();
builder.Services.AddScoped<JWTGetClaimsService>();
builder.Services.AddHttpContextAccessor();
```
Configurez l'authentification et l'autorisation :
```c#
JWTConfigurationCustomService.AddAuthentication(builder);
HandlerPolicy.AddAuthorization(builder);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
```

Ajoutez les middleware d'authentification et d'autorisation :
```c#
app.UseAuthentication();
app.UseAuthorization();
```
------------------

## Création d'endpoints sécurisés 🛡️

Endpoint sans authentification :
```c#
app.MapGet("/freeAccess", () => TypedResults.Ok(MockupDatabase.GetDataWeather()));
```

Endpoint avec authentification (rôle User ou Admin) :
```c#
app.MapGet("/userOrAdminAccess", [Authorize(Policy = "UserOrAdmin")] () =>
TypedResults.Ok(MockupDatabase.GetDataWeather()));
```

Endpoint avec authentification (rôle Admin uniquement) :
```c#
app.MapGet("/adminAccess", [Authorize(Policy = "AdminOnly")] () =>
TypedResults.Ok(MockupDatabase.GetDataWeather()));
```
*Accès au endpoint:*  
[Get_data.http](https://github.com/8b477/JWT_Bearer_Token/blob/master/Get_data.http)

------------------

## Obtention du token 🔑  
Pour obtenir un token JWT, utilisez l'endpoint d'authentification :
```C#
app.MapGet("/log", ([FromServices] IAuthentificationCustomRepository authenticationService, [FromBody] UserLogDto log)
=> authenticationService.Authentification(log.Mail, log.Password));
```
*Accès au endpoint:*    
[Get_Token.http](https://github.com/8b477/JWT_Bearer_Token/blob/master/Get_Token.http)

------------  

### Utilisation du token 🚀  
Ajoute le token JWT dans l'en-tête de la requête :
```http
Authorization: Bearer <votre_token_jwt>
```
------------- 

## Services JWT 🧰

`JWTGenerationService` : Génère les tokens JWT.  
`JWTGetClaimsService` : Récupère les claims du token JWT.  
`JWTGenerationSecretKeyService` : Gère la clé secrète pour la signature des tokens.

## Politiques d'autorisation 📜
Les politiques d'autorisation sont définies dans ``HandlerPolicy.cs`` :

- **AdminOnly** : Restreint l'accès aux utilisateurs avec le rôle "Admin".  
- **UserOrAdmin** : Permet l'accès aux utilisateurs avec le rôle "User" ou "Admin".

## Sécurité 🔐

- Les tokens JWT sont signés avec une clé secrète.
- La durée de validité du token est configurable.
- Les rôles sont vérifiés pour l'accès aux endpoints sécurisés.

-----------

# ⚠️ **N'oubliez pas de protéger votre clé secrète et de ne jamais la partager ou la committer dans votre dépôt de code.** ⚠️

------------

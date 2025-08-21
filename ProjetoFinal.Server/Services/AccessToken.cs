using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProjetoFinal.Server.Services
{
    /// <summary>
    /// Representa as configurações utilizadas para gerar e validar tokens JWT.
    /// Essas informações normalmente vêm do appsettings.json.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Quem emite o token (normalmente o nome da API).
        /// </summary>
        public string Issuer { get; set; } = "";

        /// <summary>
        /// Quem pode consumir/usar o token (ex.: frontend, outro serviço).
        /// </summary>
        public string Audience { get; set; } = "";

        /// <summary>
        /// Chave secreta usada para assinar e validar o token.
        /// Deve ser uma string aleatória e segura (>= 32 bytes).
        /// </summary>
        public string Key { get; set; } = "";

        /// <summary>
        /// Tempo de vida do token em minutos.
        /// </summary>
        public int AccessTokenMinutes { get; set; } = 15;
    }

    /// <summary>
    /// Serviço responsável por gerar tokens JWT assinados.
    /// O token inclui informações básicas do usuário (claims) e tem tempo de expiração definido.
    /// </summary>
    public class TokenService
    {
        private readonly JwtOptions _opt;

        /// <summary>
        /// Construtor que injeta as opções do JWT a partir da configuração (IOptions).
        /// </summary>
        /// <param name="opt">Opções de configuração do JWT (Issuer, Audience, Key, Tempo de vida).</param>
        public TokenService(IOptions<JwtOptions> opt) => _opt = opt.Value;

        /// <summary>
        /// Gera e retorna um token JWT válido para o usuário informado.
        /// </summary>
        /// <param name="userId">Identificador único do usuário (vai em "sub").</param>
        /// <param name="roles">Lista de roles (perfis/permissões) atribuídas ao usuário.</param>
        /// <returns>String contendo o token JWT assinado e pronto para uso.</returns>
        public string EmitirAccessToken(string userId, IEnumerable<string>? roles = null)
        {
            // Claims são as informações que vão "dentro" do token
            var claims = new List<Claim>
            {
                // Identificador único do usuário
                new Claim(JwtRegisteredClaimNames.Sub, userId),

                // Identificador único do token (pode ser usado para revogação)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // Data de emissão em Unix Time
                new Claim(JwtRegisteredClaimNames.Iat,
                          DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                          ClaimValueTypes.Integer64)
            };

            // Adiciona roles (perfis) caso existam
            if (roles != null)
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // Cria chave simétrica a partir da Key configurada
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));

            // Cria credenciais de assinatura (algoritmo HS256)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define data de expiração do token
            var expires = DateTime.UtcNow.AddMinutes(_opt.AccessTokenMinutes);

            // Monta o objeto JWT
            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,  // não pode ser usado antes deste horário
                expires: expires,            // expiração definida
                signingCredentials: creds
            );

            // Retorna token em formato compactado (string)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
    
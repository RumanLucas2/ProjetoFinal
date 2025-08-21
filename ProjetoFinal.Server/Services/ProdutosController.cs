namespace ProjetoFinal.Server.Services
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjetoFinal.Server.Services.Conn;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProdutosController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var itens = await _db.Produtos.AsNoTracking().ToListAsync();
            return Ok(new { userId, count = itens.Count, data = itens });
        }
    }

}

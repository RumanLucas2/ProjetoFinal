using BlMadre.C_.Helpers;
using BlMadre.C_.Models;
using BlMadre.C_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace BlMadre.Pages
{
    public class PedidoModel : PageModel
    {
        private readonly ILogger<PedidoModel> logger;

        [BindProperty]
        public Carrinho Carrinho { get; set; } = new Carrinho();

        [BindProperty]
        public List<Produto> Produtos { get; set; } = new List<Produto>
        {
           
        };

        public PedidoModel(ILogger<PedidoModel> _logger)
        {
            logger = _logger;
            

        }
        public void OnGet()
        {
            logger.LogInformation("Metodo OnGet chamado.");
            Carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho") ?? new Carrinho();
            var aux = MongoDbContext.Produtos.Find(_ => true).ToList();

            foreach (var i in aux)
            {
                Produtos.Add(new Produto
                {
                    Nome = i.Nome,
                    Preco = i.Preco,
                    Quantidade = -1 // -1 significa que o estoque não é controlado
                });
            }
        }
        public JsonResult OnPostAdicionarProduto(string nome, string preco, string qtd)
        {
            Carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho") ?? new Carrinho();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(preco))
            {
                logger.LogWarning("Produto não enviado");
                return new JsonResult(new { sucesso = false, mensagem = "Nome e preço são obrigatórios." });
            }

            decimal precoDecimal = decimal.Parse(preco.Replace("R$ ", "").Replace(".",","));
            int quantidade = int.TryParse(qtd, out int novaQtd) ? novaQtd : 1;

            var itemExistente = Carrinho.FindByName(nome);
            if (itemExistente != null)
            {
                logger.LogInformation($"Produto {nome} já existe no carrinho, atualizando quantidade.");
                itemExistente.Quantidade += quantidade;
                logger.LogInformation($"\tAtualizado para {itemExistente.Quantidade}");

            }
            else
            {
                logger.LogInformation($"Adicionando produto {nome} ao carrinho." +
                    $"valor: {preco}\n" +
                    $"valordecimal: {precoDecimal}.");
                Carrinho.Add(new Produto
                {
                    Nome = nome,
                    Preco = precoDecimal,
                    Quantidade = int.TryParse(qtd, out int nova) ? nova : 1
                });
            }

            HttpContext.Session.SetObject("Carrinho", Carrinho);

            return new JsonResult(new
            {
                sucesso = true,
                nome,
                preco = precoDecimal.ToString("C"),
                quantidade = itemExistente?.Quantidade.ToString() ?? qtd
            });
        }


        public JsonResult OnPostEditProduto(string nome, int qtd)
        {
            Carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho") ?? new Carrinho();

            var produto = Carrinho.Inner.FirstOrDefault(p => p.Nome == nome);
            if (produto != null)
            {
                produto.Quantidade = qtd;
                HttpContext.Session.SetObject("Carrinho", Carrinho);
                logger.LogInformation($"Produto {nome} atualizado no carrinho com quantidade {qtd}.");
                return new JsonResult(new { sucesso = true, mensagem = "Produto atualizado com sucesso." });
            }
            logger.LogInformation($"Produto nao encontrado.");

            return new JsonResult(new { sucesso = false, mensagem = "Produto não encontrado no carrinho." });
        }


        public JsonResult OnGetLoadBuffer()
        {
            logger.LogInformation("Carregando buffer do carrinho.");
            Carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho") ?? new Carrinho();
            var produtos = MongoDbContext.Produtos.Find(_ => true).ToList();
            var buffer = Carrinho.ToJsonResult();
            return new JsonResult(buffer);
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("encomenda");
        }
    }
}

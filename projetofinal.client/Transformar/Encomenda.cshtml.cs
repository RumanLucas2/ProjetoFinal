using BlMadre.C_.Helpers;
using BlMadre.C_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlMadre.Pages
{
    public class EncomendaModel : PageModel
    {
        private readonly ILogger<EncomendaModel> logger;
        public Carrinho Carrinho { get; set; } = new Carrinho();

        public EncomendaModel(ILogger<EncomendaModel> _logger)
        {
            logger = _logger;
        }


        public void OnGet()
        {

            Carrinho = HttpContext.Session.GetObject<Carrinho>("Carrinho") ?? new Carrinho();

            if (Carrinho.Inner.Count == 0)
            {
                logger.LogWarning("Carrinho veio vazio.");
            }
        }



    }
}

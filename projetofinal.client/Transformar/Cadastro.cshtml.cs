using BlMadre.C_.Helpers;
using BlMadre.C_.Models;
using BlMadre.C_.Services;
using DynamicJson;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BlMadre.Pages
{
    public class CadastroModel : PageModel
    {
        private LojaSessionModel? cookie = null;

        [BindProperty]
        public Client Usuario { get; set; } = new Client();
        private readonly ILogger<CadastroModel> logger;
        private readonly CookieHelper CookieHelper;

        public CadastroModel(ILogger<CadastroModel> _logger, CookieHelper cookieHelper)
        {
            logger = _logger;
            CookieHelper = cookieHelper;
        }

        public IActionResult OnGet(string telefone)
        {
            var a = CookieHelper.GetUserCookie(Request);

            if (!CookieHelper.IsNullOrEmpty(a))
            {
                cookie = a!;
                logger.LogInformation(telefone);
                if (!string.IsNullOrWhiteSpace(telefone))
                {
                    Usuario.Telefone = telefone;
                }
                return Page();
            }
            return RedirectToPage("/Index", ErrorCode.Unauthorized);
            
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Salvar o usuário no banco ou outra lógica
            //UserServices.AddAsync(Usuario);


            //plota no log o resultado do user

            logger.LogInformation(Usuario.ToString());
            return RedirectToPage("/Pedido");
        }
    }
}
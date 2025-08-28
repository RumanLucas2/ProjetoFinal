using BlMadre.C_.Helpers;
using BlMadre.C_.Models;
using BlMadre.C_.Services;
using DynamicJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using System.Net;
using System.Text.Json;


namespace BlMadre.Pages
{
    public class ClientModel : PageModel
    {
        private readonly ILogger logger;

        private LojaSessionModel? cookie;

        [BindProperty]
        public string? Telefone
        {
            get => field;
            set => field = value;
        }
        public CookieHelper CookieHelper { get; }

        public ClientModel(ILogger<ClientModel> logger, CookieHelper cookieHelper)
        {
            this.logger = logger;
            this.CookieHelper = cookieHelper;
        }

        public IActionResult OnPost()
        {
            var Usr = MongoDbContext.Usuarios.Find(l => l.Telefone == Telefone).FirstOrDefault();

#if DEBUG
    logger.LogInformation("Redirecionando com fallback de telefone DEBUG.");
    logger.LogInformation("telefone usado: " + Telefone);

#elif BETA
            logger.LogInformation("Redirecionando com fallback de telefone BETA.");
            logger.LogInformation("telefone usado: " + Telefone);
#endif
            if (Usr is null)
                return RedirectToPage("/Cadastro", new { telefone = Telefone });
            return RedirectToPage("/Index");


        }


        public IActionResult OnGet()
        {
            cookie = CookieHelper.GetUserCookie(Request);

            if (!CookieHelper.IsNullOrEmpty(cookie))
            {
                return Page();
            }

            // (Opcional) Logar tentativas inválidas
            // logger.LogWarning("Cookie inválido ou ausente para usuário não autenticado");

            return RedirectToPage("/Index", ErrorCode.Unauthorized);
        }

    }
}

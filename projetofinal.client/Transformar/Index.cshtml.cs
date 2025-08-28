using BlMadre.C_.Helpers;
using BlMadre.C_.Models;
using BlMadre.C_.Services;
using DynamicJson;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;



namespace BlMadre.Pages;

public class IndexModel : PageModel
{
        
    [BindProperty]
    public string? Entry
    {
        get => field;
        set => field = value;
    }

    [BindProperty]
    public string? senha
    {
        get => field;
        set => field = value;
    }

    [BindProperty]
    public string? campo 
    {
        get => field; 
        set => field = value; 
    }
    private readonly ILogger<IndexModel> logger;
    private readonly CookieHelper _cookieHelper;

    public IndexModel(ILogger<IndexModel> logger, CookieHelper cookieHelper)
    {
        this.logger = logger;
        this._cookieHelper = cookieHelper;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var loja = await LoginService.AutenticarAsync(
            new LoginRequest { Login = Entry ?? "", Senha = senha ?? "" },
            this.Response,
            _cookieHelper
        );
        if (loja == null)
        {
            return RedirectToPage(new{ code = ErrorCode.UserNotFound }); // retorna para a mesma página com erro
        }
    #if DEBUG || BETA

        logger.LogInformation($"Tentativa de login: {loja.ToString()}, response: {(loja != null ? "Succes" : "Fail")}");
    #endif
        return RedirectToPage("/ClientArea"); // ou outra página logada
    }


    public IActionResult OnGet(ErrorCode code = ErrorCode.None)
    {
        var cookie = _cookieHelper.GetUserCookie(Request);


#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
        if (!CookieHelper.IsNullOrEmpty(cookie))
        {
            // Redireciona para a página desejada
            ///quero ver se o cookie é valido, se for, redireciona para a área do cliente
            return RedirectToPage("/ClientArea");
        }
        if (code != 0)
        {
            campo = ErrorCodeExtensions.GetMessage(code);
        }
        else
        {
            campo = string.Empty;
        }
        return Page(); /// continua na mesma página se não houver redirecionamento
    }


    public void OnLoad()
    {

    }
}

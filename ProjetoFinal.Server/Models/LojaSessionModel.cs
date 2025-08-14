using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlMadre.C_.Models
{
    public class LojaSessionModel
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;

        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = string.Empty;

        public LojaSessionModel() { }

        public LojaSessionModel(Loja loja)
        {
            Nome = loja.Nome ?? string.Empty;
            Login = loja.Login ?? string.Empty;
            Tipo = loja.Tipo ?? string.Empty;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public bool IsNullOrEmpty()
        {
            if (this == null)
                return true;

            return string.IsNullOrEmpty(Login) && string.IsNullOrEmpty(Nome) && string.IsNullOrEmpty(Tipo);
        }

        /// <summary>
        /// Verifica se há campos vazios na sessão da loja.
        /// </summary>
        public bool HasEmptyFields()
        {
            return
                string.IsNullOrEmpty(Login) ||
                string.IsNullOrEmpty(Nome) ||
                string.IsNullOrEmpty(Tipo);

        }
    }
}

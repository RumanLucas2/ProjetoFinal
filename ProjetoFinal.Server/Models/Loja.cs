using Microsoft.AspNetCore.Mvc;


namespace ProjetoFinal.Server.Models
{
    /// <summary>
    /// Classe representando uma loja.
    /// </summary>
    public class Loja
    {

        /// <summary>
        /// Id da loja.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID do <see cref="User"/> associado à loja.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Login da loja.
        /// </summary>
        public required string Login { get; set; }

        /// <summary>
        /// Senha da loja.
        /// </summary>
        public required string Senha
        {
            get;
            set
            {
                field = value;
            }
        }

        /// <summary>
        /// Nome da loja.
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Tipo da loja.
        /// </summary>
        public string? Tipo { get; set; }

        /// <summary>
        /// Transforma o objeto Loja em uma string formatada.
        /// </summary>
        /// <returns></returns>
        /// <remarks><b>- Use Apenas em Development, mostra a senha nao criptografada -</b></remarks>
        public override string ToString()
        {
            return $"Loja: {Nome}, Senha: {Senha}, Login: {Login}, Tipo: {Tipo}";
        }

        /// <summary>
        /// Transforma o objeto Loja em um JsonResult.
        /// </summary>
        /// <returns></returns>
        public JsonResult ToJsonResult()
        {
            return new JsonResult(this);
        }
    }

}


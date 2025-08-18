using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;


namespace ProjetoFinal.Server.Models
{
    #region summary
    /// <summary>
    /// Classe representando uma loja.
    /// </summary>
    /// 
    #endregion
    public class Loja
    {
        #region summary
        /// <summary>
        /// Id da loja.
        /// </summary>
        /// 
        #endregion
        public Guid Id { get; set; }

        #region summary
        /// <summary>
        /// ID do <see cref="User"/> associado à loja.
        /// </summary>
        /// 
        #endregion
        public Guid UserId { get; set; }

        #region summary
        /// <summary>
        /// Login da loja.
        /// </summary>
        /// 
        #endregion
        public required string Login { get; set; }

        #region summary
        /// <summary>
        /// Senha da loja.
        /// </summary>
        /// <remarks><b>- Use Apenas em Development, mostra a senha nao criptografada -</b></remarks>
        #endregion
        [PasswordPropertyText]
        public required string Senha
        {
            get;
            set
            {
                field = value;
            }
        }

        #region summary
        /// <summary>
        /// Nome da loja.
        /// </summary>
        /// 
        #endregion
        public string? Nome { get; set; }

        #region summary
        /// <summary>
        /// Tipo da loja.
        /// </summary>
        /// 
        #endregion
        public string? Tipo { get; set; }

        #region summary
        /// <summary>
        /// Transforma o objeto Loja em uma string formatada.
        /// </summary>
        /// <returns></returns>
        /// <remarks><b>- Use Apenas em Development, mostra a senha nao criptografada -</b></remarks>
        /// 
        #endregion
        public override string ToString()
        {
            return $"Loja: {Nome}, Senha: {Senha}, Login: {Login}, Tipo: {Tipo}";
        }

        #region summary
        /// <summary>
        /// Transforma o objeto Loja em um JsonResult.
        /// </summary>
        /// <returns></returns>
        /// 
        #endregion
        public JsonResult ToJsonResult()
        {
            return new JsonResult(this);
        }
    }

}


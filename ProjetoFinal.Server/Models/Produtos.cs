namespace ProjetoFinal.Server.Models
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    #region summary
    /// <summary>
    /// Classe de Produto
    /// </summary>
    /// 
    #endregion
    public class Produto
    {
        #region summary
        /// <summary>
        /// ID do produto em estoque
        /// </summary>
        #endregion
        public string? Id { get; set; }

        #region summary
        /// <summary>
        /// Categoria do produto em estoque
        /// </summary>
        #endregion
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Categoria { get; set; }

        #region summary
        /// <summary>
        /// Nome do produto em estoque
        /// </summary>
        #endregion
        public string? Nome { get; set; }

        #region summary
        /// <summary>
        /// Preco unitario do produto:
        /// <list type="bullet">
        /// <item>
        /// <description><c>0</c> - Produto sem preco</description>
        /// </item>
        /// <item>
        /// <description><c>-1</c> - Preco nao atribuido</description>
        /// </item>
        /// </list>
        /// </summary>
        #endregion
        public decimal Preco { get; set; }

        #region summary
        /// <summary>
        /// Estoque do produto:
        /// <list type="bullet">
        /// <item>
        /// <description><c>0</c> - Produto esgotado</description>
        /// </item>
        /// <item>
        /// <description><c>-1</c> - Estoque não é controlado</description>
        /// </item>
        /// </list>
        /// </summary>

        #endregion
        public int? Quantidade { 
            get; 
            set; 
        }
    }

}

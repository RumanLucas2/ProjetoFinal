namespace ProjetoFinal.Server.Models
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Classe de Produto
    /// </summary>
    public class Produto
    {
        #region summaryID
        /// <summary>
        /// ID do produto em estoque
        /// </summary>
        #endregion
        public string? Id { get; set; }

        #region summaryCategoria
        /// <summary>
        /// Categoria do produto em estoque
        /// </summary>
        #endregion
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Categoria { get; set; }

        #region summaryNome
        /// <summary>
        /// Nome do produto em estoque
        /// </summary>
        #endregion
        public string? Nome { get; set; }

        #region summaryPreco
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

        #region summaryQuantidade
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

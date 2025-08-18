using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ProjetoFinal.Server.Models.UserModels
{
    #region summary
    /// <summary>
    /// CLasse de CEP
    /// </summary>
    #endregion 
    public class CEP
    {
        //TODO: mudar cep para numero aqui e no banco!
        private int cep;
        private string logradouro = string.Empty;
        private string complemento = string.Empty;
        private string unidade = string.Empty;
        private string bairro = string.Empty;
        private string localidade = string.Empty;
        private string uf = string.Empty;
        private string estado = string.Empty;
        private string regiao = string.Empty;
        private string ibge = string.Empty;
        private string gia = string.Empty;
        private string siafi = string.Empty;

        #region summary
        /// <summary>
        /// Inicializa um novo objeto CEP com a string fornecida fornecido.
        /// </summary>
        /// <param name="_cep"></param>
        /// <returns></returns>
        #endregion
        public static CEP SetCEP(string _cep)
        {
            if (!Regex.IsMatch(_cep, "^\\d{5}-\\d{3}$\r\n")) throw new AppException(ErrorCode.InvalidCep);
            return new CEP {
                                                                /*Caso CEP*/            /*Caso Endereco*/
                cep = int.TryParse(_cep, out var resultado) ? UseAPIBrasil(resultado) : UseViaCep(_cep),
            };
        }

        #region summary
        /// <summary>
        /// Processes the specified result using the API Brasil logic.
        /// </summary>
        /// <param name="result">An integer value to be processed. This value is returned as-is.</param>
        /// <returns>The same integer value provided in the <paramref name="result"/> parameter.</returns>
        /// <remarks>- <b>Work in progress, this method is a placeholder for future logic.</b> -</remarks>
        #endregion
        private static int UseAPIBrasil(int result)
        {
            return result;
        }

        #region summary
        /// <summary>
        /// Processes the given result string and returns its length.
        /// </summary>
        /// <param name="result">The string to be processed. Cannot be null.</param>
        /// <returns>The length of the <paramref name="result"/> string.</returns>
        /// <remarks>- <b>Work in progress, this method is a placeholder for future logic.</b> -</remarks>
        #endregion  
        private static int UseViaCep(string result)
        {
            return result.Length;
        }

        /// <summary>
        /// Transforma o CEP em uma string formatada, útil para APIs.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{cep}";
        }

        /// <summary>
        /// Transforma o CEP completo em uma string formatada, útil para APIs.
        /// </summary>
        /// <returns></returns>
        public string FullString()
        {
            return "" +
                "CEP{\n" +
                $"\t\"Cep\": \"{cep}\"\n" +
                $"\t\"Logradouro\": \"{logradouro}\"\n" +
                $"\t\"Complemento\": \"{complemento}\"\n" +
                $"\t\"Unidade\": \"{unidade}\"\n" +
                $"\t\"Bairro\": \"{bairro}\"\n" +
                $"\t\"Localidade\": \"{localidade}\"\n" +
                $"\t\"UF\": \"{uf}\"\n" +
                $"\t\"Estado\": \"{estado}\"\n" +
                $"\t\"Regiao\": \"{regiao}\"\n" +
                $"\t\"IBGE\": \"{ibge}\"\n" +
                $"\t\"GIA\": \"{gia}\"\n" +
                $"\t\"SIAFI\": \"{siafi}\"\n" +
                "}";
        }
    }
}

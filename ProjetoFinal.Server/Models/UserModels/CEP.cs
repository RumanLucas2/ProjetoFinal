using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics.CodeAnalysis;

namespace BlMadre.C_.Models
{
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

        //TODO: fazer depois CEP
        public static CEP SetCEP(string _cep)
        {
            
            return new CEP {
                                                                /*Caso CEP*/            /*Caso Endereco*/
                cep = int.TryParse(_cep, out var resultado) ? UseAPIBrasil(resultado) : UseViaCep(_cep),
            };
        }


        /// <summary>
        /// Processes the specified result using the API Brasil logic.
        /// </summary>
        /// <param name="result">An integer value to be processed. This value is returned as-is.</param>
        /// <returns>The same integer value provided in the <paramref name="result"/> parameter.</returns>
        /// <remarks>- <b>Work in progress, this method is a placeholder for future logic.</b> -</remarks>
        private static int UseAPIBrasil(int result)
        {
            return result;
        }


        /// <summary>
        /// Processes the given result string and returns its length.
        /// </summary>
        /// <param name="result">The string to be processed. Cannot be null.</param>
        /// <returns>The length of the <paramref name="result"/> string.</returns>
        /// <remarks>- <b>Work in progress, this method is a placeholder for future logic.</b> -</remarks>
        private static int UseViaCep(string result)
        {
            return result.Length;
        }


        public override string ToString()
        {
            return $"{cep}";
        }

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

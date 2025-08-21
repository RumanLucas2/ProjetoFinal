namespace ProjetoFinal.Server.Models.UserModels
{
    /// <summary>
    /// Classe de telefone
    /// </summary>
    public class Telefone
    {
        private string numero = string.Empty;

        /// <summary>
        /// DDD do telefone, representando o código de área.
        /// </summary>
        public byte Ddd
        {
            get;
            private set
            {
                field = value.ToString().Length>2?
                    throw new AppException(ErrorCode.InvalidDDD):
                    value;
            }
        }

        /// <summary>
        /// Numero de telefone, sem o DDD e sem formatação.
        /// </summary>
        public long NumeroTelefone
        {
            get;
            private set
            {
                field = value.ToString().Length >= 10 || value < 8 ?
                    throw new AppException(ErrorCode.InvalidPhoneNumber) :
                    value;
                numero = value.ToString().Length==9?
                    $"({Ddd}) {value:00000-0000}": 
                    $"({Ddd}) {value:0000-0000}";
                IsFixo = value.ToString().Length == 8;
            }
        }

        
        /// <summary>
        /// representa se o telefone é fixo ou móvel.
        /// </summary>
        public bool IsFixo
        {
            get;
            private set;
        }

        /// <summary>
        /// Inicializa um novo objeto Telefone com o número fornecido.
        /// </summary>
        /// <param name="_numero">Número de telefone no formato (XX) XXXXX-XXXX</param>
        public Telefone(string _numero)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(_numero, @"^\(\d{2}\) \d{5}-\d{4}$"))
                throw new AppException(ErrorCode.InvalidPhoneNumber);
            NumeroTelefone = long.TryParse(numero.Remove(0, 4).Replace("-", "").Trim(), out long result)?result:0;
            Ddd = byte.TryParse(numero.Substring(1, 2), out byte dddResult) ? dddResult : (byte)0;
        }
        #region summary
        /// <summary>
        /// Retorna o número de telefone formatado.
        /// </summary>
        /// 
        #endregion
        public override string ToString() => numero;
    }
}

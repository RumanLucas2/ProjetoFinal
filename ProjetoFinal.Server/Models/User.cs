using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Server.Models.UserModels;

namespace ProjetoFinal.Server.Models
{
    /// <summary>
    /// Classe representando um cliente.
    /// </summary>
    public class User
    {
        #region symmaryID
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        #endregion
        public Guid id { get; set; }

        #region symmaryNome
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        #endregion
        public string? Nome { get; set; } = string.Empty;

        #region symmaryTelefone
        /// <summary>
        /// Gets or sets the phone number associated with the entity.
        /// </summary>
        #endregion
        public string? Telefone { get; set; } = string.Empty;

        #region symmaryCPF
        /// <summary>
        /// Gets or sets the CPF (Cadastro de Pessoas Físicas) associated with the entity.
        /// </summary>
        #endregion
        public string? Cpf { get; set; } = string.Empty;

        #region symmaryBirthday
        /// <summary>
        /// Gets or sets the date of birth of the entity.
        /// </summary>
        #endregion
        public DataNascimento? DataNascimento { get; set; }

        #region symmaryCEP
        /// <summary>
        /// Gets or sets the CEP (Código de Endereçamento Postal) associated with the entity.
        /// </summary>
        #endregion
        public string? CepString 
        { 
            get=> Cep.ToString();
            set
            {
                Cep = CEP.SetCEP(value!.Replace("-",""));
            }
        }

        #region symmaryTrueCEP
        /// <summary>
        /// Gets or sets the true CEP object associated with the entity.
        /// </summary>
        #endregion
        private CEP Cep 
        {
            get => field??new CEP();
            set => field = value!; 
        }

        #region symmaryLatitude
        /// <summary>
        /// Gets or sets the latitude of the entity's location.
        /// </summary>
        #endregion
        public string Latitude { get; set; } = string.Empty;
        #region symmaryLongitude
        /// <summary>
        /// Gets or sets the longitude of the entity's location.
        /// </summary>
        #endregion
        public string Longitude { get; set; } = string.Empty;
        #region symmaryNumero
        /// <summary>
        /// Gets or sets the number associated with the entity's address.
        /// </summary>
        #endregion
        public string? Numero { get; set; } = string.Empty;


        #region symmaryComplemento
        /// <summary>
        /// Gets or sets the additional information for the entity's address.
        /// </summary>
        #endregion
        public string? Complemento { get; set; } = string.Empty;

        #region ToString
        /// <summary>
        /// Returns a string representation of the User object.
        /// </summary>
        /// <returns><see cref="JsonResult"/> style string</returns>
        #endregion
        public override string ToString()
        {
            return "" +
                "Usuario{\n" +
                $"\t\"ID\": \"{this.id}\"\n" +
                $"\t\"Nome\": \"{this.Nome}\"\n" +
                $"\t\"Telefone\": \"{this.Telefone}\"\n" +
                $"\t\"CPF\": \"{this.Cpf}\"\n" +
                $"\t\"Birthday\": \"{this.DataNascimento}\"\n" +
                $"\t\"CEP\": \"{this.Cep}\"\n" +
                $"\t\"Numero\": \"{this.Numero}\"\n" +
                $"\t\"Complemento\": \"{this.Complemento}\"\n" +
                "}";
        }
    }
}

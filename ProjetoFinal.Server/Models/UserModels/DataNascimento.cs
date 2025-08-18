using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetoFinal.Server.Models.UserModels
{
    public class DataNascimento
    {
        #region summary
        /// <summary>
        /// Meses do ano, representados por seus números correspondentes.
        /// </summary>
        #endregion  
        public enum Meses
        {
            Janeiro = 1,
            Fevereiro = 2,
            Marco = 3,
            Abril = 4,
            Maio = 5,
            Junho = 6,
            Julho = 7,
            Agosto = 8,
            Setembro = 9,
            Outubro = 10,
            Novembro = 11,
            Dezembro = 12
        }

        public Byte dia { get; private set; }
        public Byte mes { get; private set; }

        public override string ToString()
        {
            return $"{(dia<10?"0"+dia : dia.ToString())}/{(mes<10?"0"+dia : dia.ToString())}";
        }

        public DataNascimento(Byte dia, Byte mes)
        {
            this.dia = dia;
            this.mes = mes;
        }


        public DataNascimento()
        {
            this.dia = 0;
            this.mes = 0;
        }


        public DataNascimento(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                dia = 0;
                mes = 0;
                return;
            }
            if(data.Length < 5)
            {
                throw new AppException(ErrorCode.Data5);
            }
            if (!data.Contains('/'))
            {
                throw new AppException(ErrorCode.DataNoSlash);
            }
            if(!Regex.Match(data,@"^\d{1,2}/\d{1,2}$").Success)
            {
                throw new AppException(ErrorCode.DataInvalidRegex);
            }
            var split = data.Split('/');
            dia = Byte.Parse(split[0].Replace("/", ""));
            mes = Byte.Parse(split[1].Replace("/", ""));
        }


        public DataNascimento(System.DateTime data)
        {
            dia = (Byte)data.Day;
            mes = (Byte)data.Month;
        }

        public DataNascimento(System.DateOnly data)
        {
            dia = (Byte)data.Day;
            mes = (Byte)data.Month;
        }
        #region summary
        /// <summary>
        /// Define implicitamente a conversão de uma DataOnly para uma DataNascimento.
        /// </summary>
        #endregion
        public static implicit operator DataNascimento(DateOnly d)
        {
            return new DataNascimento(d);
        }

        #region summary
        /// <summary>
        /// Define implicitamente a conversão para uma string de uma DataNascimento.
        /// </summary>
        #endregion
        public static implicit operator DataNascimento(string d)
        {
            try
            {
                return new DataNascimento(d);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region summary
        /// <summary>
        /// Retorna uma representação em string da data de nascimento no formato "dd/mm".
        /// </summary>
        /// <returns></returns>
        #endregion
        public override int GetHashCode()
        {
            return (dia << 8) | mes;
        }

        #region summary
        /// <summary>
        /// verifica se a data é o aniversário do usuário
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <description><c>true</c> - é o aniversario</description>
        /// </item>
        /// <item>
        /// <description><c>false</c> - não é o aniversario</description>
        /// </item>
        /// </list>
        /// </returns>
#endregion
        public bool IsBirthDay()
        {
            if (this == null) return false;
            if (!(DateTime.Today.Year % 4 == 0 && (DateTime.Today.Year % 100 != 0 || DateTime.Today.Year % 400 == 0))) // Ano não bissexto
            {
                if (this.mes == 2 && this.dia == 29 && DateTime.Today.Day == 28) return true; // Fevereiro não bissexto                    
            }
            if (this.mes == DateTime.Today.Month && this.dia == DateTime.Today.Day) return true;
            return false;
        }

        #region summary
        /// <summary>
        /// verifica se é o mês de aniversário do usuário
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <description><c>true</c> - é o mês de aniversario</description>
        /// </item>
        /// <item>
        /// <description><c>false</c> - não é o mês de aniversario</description>
        /// </item>
        /// </list>
        /// </returns>
        #endregion
        public bool IsBirthMonth()
        {
            if (this == null) return false;
            if (this.mes == DateTime.Today.Month) return true;
            return false;
        }
    }
}


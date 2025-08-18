using ProjetoFinal.Server.Models;

namespace ProjetoFinal.Server.Services
{
    /// <summary>
    /// Classe de Senha
    /// </summary>
    public static class Password
    {
        /// <summary>
        /// Encripta a senha informada usando o algoritmo BCrypt.
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="workFactor"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCode.NullReference"></exception>
        public static string Encript(string senha, int workFactor = 12)
        {
            if (string.IsNullOrEmpty(senha))
                throw new AppException(ErrorCode.NullReference);

            return BCrypt.Net.BCrypt.HashPassword(senha, workFactor);
        }

        /// <summary>
        /// Verifica se a senha informada corresponde ao hash fornecido.
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCode.NullReference"></exception>
        public static bool Verify(string senha, string hash)
        {
            if (string.IsNullOrEmpty(senha)) throw new AppException(ErrorCode.NullReference);
            if (string.IsNullOrEmpty(hash)) throw new AppException(ErrorCode.NullReference);
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

    }
}

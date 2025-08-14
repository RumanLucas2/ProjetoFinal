namespace BlMadre.C_.Services
{
    public static class Password
    {
        //TODO: Montar regra de senha

        public static string Encript(string senha, int workFactor = 12)
        {
            if (string.IsNullOrEmpty(senha))
                throw new ArgumentNullException(nameof(senha), "Texto para encriptar não pode ser nulo ou vazio.");

            return BCrypt.Net.BCrypt.HashPassword(senha, workFactor);
        }


        public static bool Verify(string senha, string hash)
        {
            if (string.IsNullOrEmpty(senha)) throw new ArgumentNullException(nameof(senha), "Texto para verificar não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(hash)) throw new ArgumentNullException(nameof(hash), "Hash para verificação não pode ser nulo ou vazio.");
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }

    }
}

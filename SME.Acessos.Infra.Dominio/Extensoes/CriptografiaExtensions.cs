using System.Security.Cryptography;
using System.Text;
using SME.Acessos.Infra.Dominio.Enumeradores;

namespace SME.Acessos.Infra.Dominio.Extensions
{
    public static class CriptografiaExtensions
    {
        private static readonly byte[] tdesKey = new byte[] { 107, 8, 82, 60, 113, 135, 190, 128, 188, 51, 238, 120, 59, 135, 57, 140, 107, 8, 82, 60, 113, 135, 190, 128 };
        private static readonly byte[] tdesIV = new byte[] { 113, 135, 190, 128, 186, 217, 34, 47 };

        public static string CriptografarSenhaSHA512(string senha)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                return Convert.ToBase64String(sha512.ComputeHash(Encoding.Unicode.GetBytes(senha))).TrimStart('/');
            }
        }

        /// Compara duas senhas - Uma que já está criptografada e outra não
        /// </summary>
        /// <param name="senha1">Senha digitada pelo usuário</param>
        /// <param name="senha2">Senha já criptografada do BD</param>
        /// <param name="tipo">1 = TripleDES / 3 = SHA512</param>
        /// <returns>true caso sejam iguais as senhas</returns>
        public static bool EqualsSenha(string senha1, string senha2, TipoCriptografia tipo)
        {
            if (tipo == TipoCriptografia.TripleDES)
                return CriptografarSenhaTripleDES(senha1).Equals(senha2);
            
            return CriptografarSenhaSHA512(senha1).Equals(senha2);
        }

        public static string CriptografarSenhaTripleDES(string senha)
        {
            byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(senha);
            MemoryStream ms = new MemoryStream();
            SymmetricAlgorithm sym = TripleDES.Create();
            CryptoStream encStream = new CryptoStream(ms, sym.CreateEncryptor(tdesKey, tdesIV), CryptoStreamMode.Write);
            encStream.Write(plainByte, 0, plainByte.Length);
            encStream.FlushFinalBlock();
            byte[] cryptoByte = ms.ToArray();
            return Convert.ToBase64String(cryptoByte);
        }

        public static string DescriptografarSenhaTripleDES(string senha)
        {
            byte[] cryptoByte = Convert.FromBase64String(senha);
            var sym = TripleDES.Create();
            MemoryStream ms = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
            CryptoStream cs = new CryptoStream(ms, sym.CreateDecryptor(tdesKey, tdesIV), CryptoStreamMode.Read);
            var ret = _ReadBytes(cs);
            return Encoding.ASCII.GetString(ret);
        }

        public static string CriptografarSenha(string senha, TipoCriptografia tipo)
        {
            switch (tipo)
            {
                case TipoCriptografia.TripleDES:
                    return CriptografarSenhaTripleDES(senha);
                case TipoCriptografia.MD5:
                    throw new NotImplementedException();
                case TipoCriptografia.SHA512:
                    return CriptografarSenhaSHA512(senha);
                default:
                    throw new NotImplementedException();
            }
        }

        private static byte[] _ReadBytes(Stream s)
        {
            int length = 10000000;
            byte[] buffer = new byte[length];
            int bytesLidos = length;
            using (MemoryStream ms = new MemoryStream())
            {
                while (bytesLidos == length)
                {
                    bytesLidos = s.Read(buffer, 0, length);
                    ms.Write(buffer, 0, bytesLidos);
                }

                return ms.ToArray();
            }

        }
    }
}

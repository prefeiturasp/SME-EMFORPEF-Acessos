using Microsoft.Extensions.PlatformAbstractions;
using Npgsql;
using System.Data;
using System.Text;

namespace SME.Acessos.TesteIntegracao.Setup
{
    public class ConstrutorDeTabelas
    {
        public void Contruir(NpgsqlConnection connection)
        {
            MontaBaseDados(connection);
        }

        private static void MontaBaseDados(NpgsqlConnection connection)
        {
            ExecutarPreScripts(connection);

            var scripts = ObterScripts();
            var d = new DirectoryInfo(scripts);

            var files = d.GetFiles("*.sql").OrderBy(a => int.Parse(CleanStringOfNonDigits_V1(a.Name.Replace("\uFEFF",""))));

            foreach (var file in files)
            {
                string? textoComEncodeCerto = string.Empty;

                if (file.Name.Contains("V8__"))
                {
                    var pathNovoArquivo = Path.Combine("Setup", "V8__PERMISSIONAMENTO_PROPOSTA_CONECTA_FORMACAO.sql");
                    var b = File.ReadAllBytes(pathNovoArquivo);
                    Encoding enc = Encoding.UTF8;
                    textoComEncodeCerto = ReadFileAndGetEncoding(b, ref enc);
                }
                else
                {
                    var b = File.ReadAllBytes(file.FullName);

                    Encoding enc = null;

                    textoComEncodeCerto = ReadFileAndGetEncoding(b, ref enc);
                }

                using var cmd = new NpgsqlCommand(textoComEncodeCerto, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao executar o script {file.FullName}. Erro: {ex.Message}", ex);
                }
            }
        }

        private static string ReadFileAndGetEncoding(Byte[] docBytes, ref Encoding encoding)
        {
            if (encoding == null)
                encoding = Encoding.GetEncoding(1252);
            Int32 len = docBytes.Length;
            if (len > 3 && docBytes[0] == 0xEF && docBytes[1] == 0xBB && docBytes[2] == 0xBF)
            {
                encoding = new UTF8Encoding(true);
                return encoding.GetString(docBytes, 3, len - 3);
            }
            Boolean isPureAscii = true;
            Boolean isUtf8Valid = true;
            for (Int32 i = 0; i < len; ++i)
            {
                Int32 skip = TestUtf8(docBytes, i);
                if (skip == 0)
                    continue;
                if (isPureAscii)
                    isPureAscii = false;
                if (skip < 0)
                {
                    isUtf8Valid = false;
                    break;
                }
                i += skip;
            }
            if (isPureAscii)
                encoding = new ASCIIEncoding();
            else if (isUtf8Valid)
                encoding = new UTF8Encoding(false);
            return encoding.GetString(docBytes);
        }

        private static Int32 TestUtf8(Byte[] binFile, Int32 offset)
        {
            const Int32 maxUtf8Length = 4;
            Byte current = binFile[offset];
            if ((current & 0x80) == 0)
                return 0;
            Int32 len = binFile.Length;
            for (Int32 addedlength = 1; addedlength < maxUtf8Length; ++addedlength)
            {
                Int32 fullmask = 0x80;
                Int32 testmask = 0;
                for (Int32 i = 0; i <= addedlength; ++i)
                {
                    testmask = fullmask;
                    fullmask += (0x80 >> (i + 1));
                }
                if ((current & fullmask) == testmask)
                {
                    if (offset + addedlength >= len)
                        return -1;
                    
                    for (Int32 i = 1; i <= addedlength; ++i)
                    {
                        if ((binFile[offset + i] & 0xC0) != 0x80)
                            return -1;
                    }
                    return addedlength;
                }
            }
            return -1;
        }

        private static string CleanStringOfNonDigits_V1(string s)
        {
            try
            {
                s = s.ToUpper().Replace("V", "");
                var clearStr = s.Split("__");
                return clearStr[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private static string ObterScripts()
        {
            var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
            var relativePathToHostProject = @"../../../../../scripts";

            return Path.GetFullPath(Path.Combine(testProjectPath, relativePathToHostProject));
        }

        private static void ExecutarPreScripts(NpgsqlConnection connection)
        {
            var builder = new StringBuilder();
            builder.Append("CREATE USER postgres;");
            builder.Append("SET client_encoding TO 'UTF8';");
            using var cmd = new NpgsqlCommand(builder.ToString(), connection);
            cmd.ExecuteNonQuery();
        }
    }
}

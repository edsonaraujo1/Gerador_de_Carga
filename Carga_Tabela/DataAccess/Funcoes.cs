using System.Text.RegularExpressions;

namespace Carga_Tabela.DataAccess
{
    public class Funcoes
    {
        public string ApenasNumeros(string str)
        {
            var apenasDigitos = new Regex(@"[^\d]");
            return apenasDigitos.Replace(str, "");

        }

    }
}

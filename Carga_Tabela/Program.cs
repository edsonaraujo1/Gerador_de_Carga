using Carga_Tabela.DataAccess;
using System;

namespace Carga_Tabela
{
    class Program
    {
        static void Main(string[] args)
        {
            int qtd = 10;
            int tipo_doc = 1; // 1 CPF e 2 CNPJ
            var ret = "";
            int notexi = 0;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("  Aquarde..");

            try
            {
                using (var contexto = new Contexto())
                {
                    NumberGenerator generator = new NumberGenerator();

                    for (int i = 0; i < qtd; i++)
                    {
                        if (tipo_doc == 1)
                        {
                            ret = generator.CpfComMascara(1);
                        }
                        else
                        {
                            tipo_doc = 2;
                            ret = generator.CnpjComMascara(1);
                        }

                        var non = CreateString(8) + " " + CreateString(2) + " " + CreateString(10);
                        var dtnas = RandomDate(1960, "yyyy-MM-dd");

                        Console.WriteLine(ret + PrimeiraMaiuscula(non));

                        string strQuery3 = string.Format("INSERT INTO ADGR_EDSON (   NOME_PESSOA, " +
                                                                                    "DOC_PESSOA, " +
                                                                                    "NASC_PESSOA, " +
                                                                                    "DT_INCLUSAO, " +
                                                                                    "DT_ALTERACAO," +
                                                                                    "TIPO_DOC)" +
                                                                          " VALUES ('" + non + "'," +
                                                                                    "'" + ret.Trim() + "'," +
                                                                                    "'" + dtnas + "'," +
                                                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                                                    "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                                                                    "'" + tipo_doc + "'" +
                                                                                   ")");
                        contexto.ExecutaComando(strQuery3);
                        notexi++;
                        //Console.WriteLine(" Insert: Nº " + notexi.ToString() + " - " + ret.ToString() + " OK ");

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            Console.WriteLine("  Quantidade de registro inseridos na base: " + notexi.ToString());
            Console.WriteLine("  Fim da Carga !!");
            Console.ReadLine();

        }

        static Random rd = new Random();
        public static string CreateString(int stringLength)
        {
            const string allowedChars = "ANABIANCACARLOSDEMETRIOEDSONFABIOGENUINOHEITORJOAOKLEBERLMNOPQRSTUVWXYZ ZYXWVUTSRQPONMLKJHGFEEDCBA ";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string PrimeiraMaiuscula(String strString)
        {
            string strResult = "";
            bool booPrimeira = true;

            if (strString.Length > 0)
            {
                for (int intCont = 0; intCont <= strString.Length - 1; intCont++)
                {
                    if ((booPrimeira) && (!strString.Substring(intCont, 1).Equals(" ")))
                    {
                        strResult += strString.Substring(intCont, 1).ToUpper();
                        booPrimeira = false;
                    }
                    else
                    {
                        strResult += strString.Substring(intCont, 1).ToLower();
                        if (strString.Substring(intCont, 1).Equals(" "))
                        {
                            booPrimeira = true;
                        }
                    }
                }
            }
            return strResult;
        }

        public static string RandomDate(int startYear, string outputDateFormat)
        {
            DateTime start = new DateTime(startYear, 1, 1);
            Random gen = new Random(Guid.NewGuid().GetHashCode());
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range)).ToString(outputDateFormat);
        }

    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Carga_Tabela.DataAccess
{
    public class Contexto : IDisposable
    {
        readonly string host = "localhost"; 
        readonly string banco = "PESSOASRISCO";
        readonly string user = "desenvolvimento";
        readonly string pwd = "dev123";
        readonly string polling = "true";

        public readonly SqlConnection minhaConexao;

        public Contexto()
        {
            minhaConexao = new SqlConnection("Data Source=" + host + "; Initial Catalog = " + banco + "; Uid = " + user + "; Password = " + pwd + "; MultipleActiveResultSets = true; Pooling = " + polling + "; Min Pool Size=0; Max Pool Size=250; Connect Timeout=30;");
            try
            {
                minhaConexao.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            try
            {
                cmdComando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                cmdComando.Dispose();
            }

        }

        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            using (var cmdComando = new SqlCommand(strQuery, minhaConexao))
            {
                try
                {
                    return cmdComando.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    cmdComando.Dispose();
                }

            }
        }

        // Método - Fechar Conexao
        public void FecharConexao()
        {
            minhaConexao.Close();
        }

        public void Dispose()
        {
            if (minhaConexao != null && minhaConexao.State == ConnectionState.Open)
            {
                minhaConexao.Close();
            }
            if (minhaConexao != null)
            {
                minhaConexao.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void Open()
        {
            if (minhaConexao.State == ConnectionState.Closed)
            {
                minhaConexao.Open();
            }
        }

        public void Close()
        {
            if (minhaConexao.State == ConnectionState.Open)
            {
                minhaConexao.Close();
            }
        }

        public Exception UltimaException { get; set; }

        public string msg { get; set; }

        public string EVENT_LOG_NAME { get; set; }

        [SerializableAttribute]
        [ComVisibleAttribute(true)]
        public class NullReferenceException : SystemException
        {

        }

    }
}
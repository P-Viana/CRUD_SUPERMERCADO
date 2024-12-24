using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_CadastroDeproduto_PEDRO
{
    internal class ConexaoProduto
    {
        MySqlConnection conn;
        public void ConectarBD()
        {
            try
            {
                conn = new MySqlConnection("Persist Security info = false; server = localhost; database = bdprodutos; user = root; pwd=;");
                conn.Open();
                //MessageBox.Show("Conectado!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao conectar: " + e.Message);
            }
        }

        public int ExecutarComandos(string sql)
        {
            int resultado;
            try
            {
                //Conectar ao Banco de Dados
                ConectarBD();
                //Preparar o comando
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //Executar o comando
                cmd.ExecuteNonQuery();
                resultado = 1;
            }
            catch (Exception)
            {
                resultado = 0;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return resultado;
        }

        public DataTable ExecutarConsulta(string sql)
        {
            try
            {
                ConectarBD();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

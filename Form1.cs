using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CRUD_CadastroDeproduto_PEDRO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ConexaoProduto bd = new ConexaoProduto();
        string tabela = "tblprodutos";

        private void Form1_Load(object sender, EventArgs e)
        {
            bd.ConectarBD();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string inserir;
            double preco;
            if (txtNome.Text != "" && txtCodigo.Text != "" && double.TryParse(txtPreco.Text, out preco))
            {
                inserir = String.Format($"INSERT INTO {tabela} VALUES (NULL, '{txtNome.Text}', '{txtCodigo.Text}', '{txtPreco.Text}')");
                bd.ExecutarComandos(inserir);
                ExibirDados();
                LimpaCampos();
            }
            else
            {
                MessageBox.Show("Informação Inválida!", "COnfirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ExibirDados()
        {
            string dados = $"SELECT * FROM {tabela}";
            DataTable dt = bd.ExecutarConsulta(dados);
            dtgProdutosMostrar.DataSource = dt.AsDataView();
        }
        private void LimpaCampos()
        {
            txtNome.Clear();
            txtCodigo.Clear();
            txtPreco.Clear();
            txtNome.Focus();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void dtgProdutosMostrar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblID.Text = dtgProdutosMostrar.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNome.Text = dtgProdutosMostrar.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCodigo.Text = dtgProdutosMostrar.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPreco.Text = dtgProdutosMostrar.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string excluir;
            if (txtNome.Text != " " && txtCodigo.Text != " " && txtPreco.Text !="")
            {
                excluir = string.Format($"DELETE FROM {tabela} WHERE id = {lblID.Text}");
                int resultado = bd.ExecutarComandos(excluir);
                if (resultado == 1)
                {
                    MessageBox.Show("Registro excluído com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro ao excluir!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ExibirDados();
                LimpaCampos();
            }
            else
            {
                MessageBox.Show("Informação Inválida!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string alterar;
            double preco;
            if (txtNome.Text != "" && double.TryParse(txtPreco.Text, out preco))
            {
                alterar = $"UPDATE {tabela} SET nome = '{txtNome.Text}', codigoDeBarras = '{txtCodigo.Text}', preco = '{txtPreco.Text}' WHERE id = {lblID.Text}";
                int resultado = bd.ExecutarComandos(alterar);
                if (resultado == 1)
                {
                    MessageBox.Show("Registro atualizado com sucesso!");
                    LimpaCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao atualizar!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ExibirDados();
            }
            else
            {
                MessageBox.Show("Informação Inválida!", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

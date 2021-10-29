using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SistemaWebControlerEstoque
{
    public partial class Produto : System.Web.UI.Page
    {
        ProdutoBLL objProduto = new ProdutoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarCategorias();
            CarregarGridView();
        }

        public void CarregarCategorias()
        {
            ddlCategoria.DataSource = objProduto.RetCategoriasProdutos();
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataTextField = "nome";
            ddlCategoria.DataBind();
        }

        public void CarregarGridView()
        {
            gridProdutos.DataSource = objProduto.RetListarProdutos();
            gridProdutos.DataBind();
        }

        public void LimparForm()
        {
            txtId.Text = String.Empty;
            txtNome.Text = String.Empty;
            txtDescricao.Text = String.Empty;
            txtCusto.Text = String.Empty;
            txtVenda.Text = String.Empty;
            txtQuantidade.Text = String.Empty;
            txtUnidade.Text = String.Empty;
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            objProduto.Nome = txtNome.Text;
            objProduto.Descricao = txtDescricao.Text;
            objProduto.Preco_Custo = decimal.Parse(txtCusto.Text);
            objProduto.Preco_Venda = decimal.Parse(txtVenda.Text);
            objProduto.Quantidade = double.Parse(txtQuantidade.Text);
            objProduto.Unidade_Medida = txtUnidade.Text;
            objProduto.Categoria_Id = int.Parse(ddlCategoria.SelectedValue.ToString());
            if (String.IsNullOrEmpty(txtId.Text))
            {
                objProduto.InserirProduto();
            }
            else
            {
                objProduto.AlterarProduto(txtId.Text);
            }
            LimparForm();
            CarregarGridView();
        }

        protected void btnCarregar_Click(object sender, EventArgs e)
        {
            DataTable data = objProduto.CarregarProdutoPorId(txtId.Text);
            txtNome.Text = data.Rows[0]["nome"].ToString();
            txtDescricao.Text = data.Rows[0]["descricao"].ToString();
            txtCusto.Text = data.Rows[0]["preco_custo"].ToString();
            txtVenda.Text = data.Rows[0]["preco_venda"].ToString();
            txtQuantidade.Text = data.Rows[0]["quantidade"].ToString();
            txtUnidade.Text = data.Rows[0]["unidade_medida"].ToString();
            ddlCategoria.SelectedValue = data.Rows[0]["categoria_id"].ToString();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            objProduto.ExcluirProduto(txtId.Text);
            LimparForm();
            CarregarGridView();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }
    }
}
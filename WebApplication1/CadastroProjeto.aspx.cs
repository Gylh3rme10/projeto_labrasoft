using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtAreaConhecimento.Text) ||
                string.IsNullOrWhiteSpace(txtVerba.Text)
                )
            {
                lblMensagem.Text = "Há campos não preenchidos.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                Projeto novoProjeto = new Projeto();

                novoProjeto.Titulo = txtTitulo.Text;
                novoProjeto.AreaConhecimento = txtAreaConhecimento.Text;
                
                decimal verba;

                if (decimal.TryParse(txtVerba.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out verba))
                {
                    novoProjeto.VerbaAprovada = verba;
                }
                else
                {
                    lblMensagem.Text = "Digite um valor válido para a verba.";
                    return;
                }
                int idCoordenador = Convert.ToInt32(ddlCoordenador.SelectedValue);

                novoProjeto.Coordenadores = Repositorio.ListarCoordenadores()
                    .FirstOrDefault(c => c.Id == idCoordenador);
                //terminar substituição de CPF por ID: concluido

                // Bolsistas selecionados
                foreach (ListItem item in lstBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        Bolsista bolsista = Repositorio.ListarBolsistas()
                            .FirstOrDefault(b => b.Id == Convert.ToInt32(item.Value));

                        if (bolsista != null)
                        {
                            novoProjeto.Bolsistas.Add(bolsista);
                        }
                    }
                }

                // Salva o projeto
                Repositorio.InserirProjeto(novoProjeto);

                Response.Redirect("CadastroProjeto.aspx");

                lblMensagem.Text = "Cadastro concluído:";
                lblMensagem.ForeColor = System.Drawing.Color.Green;


            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("CadastroProjeto.aspx");
            }
            AtualizarGrid();
        }
        protected void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Detalhes")
                return;

            int idProjeto = Convert.ToInt32(e.CommandArgument);

            Projeto projeto = Repositorio.ListarProjetos()
                .FirstOrDefault(p => p.Id == idProjeto);

            if (projeto == null)
            {
                lblMensagem.Text = "Projeto não encontrado.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            ViewState["ProjetoId"] = projeto.Id;

            string nomeCoordenador = projeto.Coordenadores != null
                ? projeto.Coordenadores.Nome
                : "Não informado";

            decimal total = Repositorio.TotalDespesasProjeto(projeto.Id);

            string listaBolsistas = "Nenhum bolsista";

            if (projeto.Bolsistas != null && projeto.Bolsistas.Count > 0)
            {
                listaBolsistas = string.Join(", ",
                    projeto.Bolsistas.Select(b => b.Nome));
            }
            var detalhes = new[]
            {
                new
                {
                Titulo = projeto.Titulo,
                AreaConhecimento = projeto.AreaConhecimento,
                Coordenador = nomeCoordenador,
                Bolsistas = listaBolsistas,
                VerbaAprovada = projeto.VerbaAprovada,
                TotalDespesas = total,
                Saldo = projeto.VerbaAprovada - total
                }
            };

            gvDetalhesProjeto.DataSource = detalhes;
            gvDetalhesProjeto.DataBind();

            pnlDetalhes.Visible = true;
            pnlDespesas.Visible = false;
        }

        protected void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
            pnlDespesas.Visible = false;
        }
        protected void btnFecharDespesas_Click(object sender, EventArgs e)
        {
            pnlDespesas.Visible = false;
        }
        private void AtualizarGrid()
        {
            CarregarCoordenadores();
            CarregarBolsistas();

            gvProjetos.DataSource = Repositorio.ListarProjetos();
            gvProjetos.DataBind();

            foreach (GridViewRow row in gvProjetos.Rows)
            {
                Button btn = (Button)row.FindControl("btnDetalhes");

                if (ViewState["ProjetoAberto"] != null &&
                    row.RowIndex == (int)ViewState["ProjetoAberto"])
                {
                    btn.Text = "Fechar";
                    btn.CssClass = "btn btn-secondary btn-sm";
                }
                else
                {
                    btn.Text = "Detalhes";
                    btn.CssClass = "btn btn-outline-primary btn-sm";
                }
            }
        }
        private void CarregarBolsistas()
        {
            // Todos os bolsistas cadastrados
            List<Bolsista> todos = Repositorio.ListarBolsistas();

            // Todos os projetos
            List<Projeto> projetos = Repositorio.ListarProjetos();

            // IDs dos bolsistas que já pertencem a algum projeto
            List<int> idsEmProjeto = projetos
                .SelectMany(p => p.Bolsistas)
                .Select(b => b.Id)
                .Distinct()
                .ToList();

            // Apenas os disponíveis
            var disponiveis = todos
                .Where(b => !idsEmProjeto.Contains(b.Id))
                .ToList();

            lstBolsistas.DataSource = disponiveis;
            lstBolsistas.DataTextField = "Nome";
            lstBolsistas.DataValueField = "Id";
            lstBolsistas.DataBind();
        }
        private void CarregarCoordenadores()
        {
            // Todos os coordenadores cadastrados
            List<Coordenador> todos = Repositorio.ListarCoordenadores();

            // Todos os projetos
            List<Projeto> projetos = Repositorio.ListarProjetos();

            // IDs dos coordenadores que já estão em algum projeto
            List<int> idsEmProjeto = projetos
                .Select(p => p.Coordenadores.Id)
                .Distinct()
                .ToList();

            // Apenas os disponíveis
            var disponiveis = todos
                .Where(c => !idsEmProjeto.Contains(c.Id))
                .ToList();

            ddlCoordenador.DataSource = disponiveis;
            ddlCoordenador.DataTextField = "Nome";
            ddlCoordenador.DataValueField = "Id";
            ddlCoordenador.DataBind();

            ddlCoordenador.Items.Insert(0,
                new ListItem("-- Selecione um coordenador --", ""));
        }
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAreaConhecimento.Text = "";
            txtVerba.Text = "";
            ddlCoordenador.SelectedIndex = 0;

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";

            pnlDetalhes.Visible = false;
           

            foreach (ListItem item in lstBolsistas.Items)
            {
                item.Selected = false;
            }
        }
        protected void BtnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void btnVerDespesas_Click(object sender, EventArgs e)
        {
            int idProjeto = Convert.ToInt32(ViewState["ProjetoId"]);

            gvDespesas.DataSource =
                Repositorio.ListarDespesasPorProjeto(idProjeto);

            gvDespesas.DataBind();

            pnlDespesas.Visible = true;
        }

    }
}
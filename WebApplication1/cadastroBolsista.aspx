<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Cadastro de Bolsista</h2>
            </div>
            <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label">Nome Completo:</label>
                        <asp:Textbox ID = "txtNome" runat="server" CssClass="form-control" placeholder="Digite o nome"></asp:Textbox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Cpf:</label>
                       <asp:Textbox ID = "txtCpf" runat="server" CssClass="form-control" placeholder="Digite o cpf"></asp:Textbox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Matrícula:</label>
                        <asp:Textbox ID = "txtMatricula" runat="server" CssClass="form-control" placeholder="Digite a matrícula"></asp:Textbox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Data de Nascimento:</label>
                        <asp:Textbox ID = "dateBirth" runat="server" TextMode="Date" CssClass="form-control"></asp:Textbox>
                    </div>

                    <div class="mb-3">
                        <label for="sexo">Sexo:</label>
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                            <asp:ListItem value="">Selecione...</asp:ListItem>
                            <asp:ListItem value="M">Masculino</asp:ListItem>
                            <asp:ListItem value="F">Feminino</asp:ListItem>
                            <asp:ListItem value="O">Outro/Não informado</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <hr />
                    <div class="d-grid gap-2">
                        <asp:Button ID="BtnSalvar" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="BtnSalvar_Click"/>
                    </div>
                <div class="mb-4">
                    <asp:Label ID="lblMensagem" runat="server" CssClass="h6"></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cadastroBolsista.aspx.cs" Inherits="WebApplication1.cadastroBolsista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
    
    <div class="card shadow-sm">
        <div class="card-header bg-primary text-white">
            <h3 class="mb-0">Cadastro Bolsista</h3>
        </div>
        <div class="card-body p-4">
            <p class="text-muted text-start fs-3"> Preencha os campos abaixo</p>
            <hr />
            <div class ="form-group mb-3 "> 
                <label class =" form-label font-weight-bold"> Nome completo: </label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder =" Nome completo"> </asp:TextBox>
            </div>
            <div class ="form-group mb-3 "> 
                <label class =" form-label font-weight-bold"> CPF: </label>
                <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder =" xxx.xxx.xxx - xx"> </asp:TextBox>
            </div>
             <div class ="form-group mb-3 "> 
                <label class =" form-label font-weight-bold"> Matricula: </label>
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" placeholder ="0000-000X"> </asp:TextBox>
            </div>
             <div class ="form-group mb-3 "> 
                <label class =" form-label font-weight-bold"> Data de Nascimento: </label>
                <asp:TextBox ID="txtDataNascimento" runat="server" TextMode="Date" CssClass="form-control" > </asp:TextBox>
            </div>
            <div class="form-group mb-3">
                 <label class =" form-label font-weight-bold"> Sexo: </label>
                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control"> 
                    <asp:ListItem Text="Selecione" Value="" Selected="True" Disabled="True" />
                    <asp:ListItem Text="Masculino" Value="M" />
                    <asp:ListItem Text="Feminino" Value="F" />
                    <asp:ListItem Text="Outro" Value="O" />
                </asp:DropDownList>
            </div>
            <div class="d-grid gap-2">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar Cadastro" CssClass="btn btn-success btn-lg w-100" OnClick="btnSalvar_click" />
            </div>
            <div class="mt-4 text-center">
                <asp:Label ID="lblMensagem" runat="server" CssClass="h6">   </asp:Label>
            </div>
        </div>
            
    </div>        
</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenadores.aspx.cs" Inherits="WebApplication1.CadastroCoordenadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">

                    <div class="card-header bg-primary text-white">
                        <h2 class="mb-0">Cadastro de Coordenadores</h2>
                    </div>

                    <div class="card-body">

                        <!-- Primeira linha -->
                        <div class="row">
                            <div class="col-md-8 mb-3">
                                <label class="form-label">Nome Completo:</label>
                                <asp:TextBox ID="txtNome" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite o nome">
                                </asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">CPF:</label>
                                <asp:TextBox ID="txtCPF" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite o CPF">
                                </asp:TextBox>
                            </div>
                        </div>
                        <!-- Segunda linha: Email e Titulação -->
                        <div class="row">
                            
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite seu email">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Titulação:</label>
                                <asp:DropDownList ID="ddlTitulacao" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="Bacharelado">Bacharelado</asp:ListItem>
                                    <asp:ListItem Value="Licenciatura">Licenciatura</asp:ListItem>
                                    <asp:ListItem Value="Tecnólogo">Tecnólogo</asp:ListItem>
                                    <asp:ListItem Value="Mestrado">Mestrado</asp:ListItem>
                                    <asp:ListItem Value="Doutorado">Doutorado</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Terceira linha: Area de atuação -->
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Area de atuacao:</label>
                                <asp:TextBox ID="txtAtuacao" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite sua area de atuação">
                                </asp:TextBox>
                            </div>
                        </div>
                        <hr />

                        <!-- Botões -->
                        <div class="row mt-3">
                            <div class="col-md-9 d-grid">
                                <asp:Button ID="BtnSalvar"
                                    runat="server"
                                    Text="Salvar"
                                    CssClass="btn btn-success"
                                    OnClick = "BtnSalvar_Click "/>
                            </div>

                            <div class="col-md-3 d-grid">
                                <asp:Button ID="BtnLimpar"
                                    runat="server"
                                    Text="Limpar"
                                    CssClass="btn btn-secondary"
                                    OnClick = "BtnLimpar_Click "
                                    />
                            </div>
                        </div>

                        <div class="mt-3">
                            <asp:Label ID="lblMensagem"
                                runat="server"
                                CssClass="h6">
                            </asp:Label>
                        </div>

                        <hr />

                        <!-- Lista -->
                        <div class="mt-4">
                            <h3 class="text-secondary">Lista de Coordenadores</h3>
                            <div class="mt-3">
                                <asp:Label ID="lblAviso"
                                    runat="server"
                                    CssClass="text-danger">
                                </asp:Label>
                            </div>
                            <asp:Label ID="lblBotao"
                                runat="server"
                                CssClass="text-success">
                            </asp:Label>
                            <asp:Panel ID="pneCoordenadores" runat="server">
                                <hr />
                                <div class="d-flex align-items-center gap-2 mb-3">
                                        <asp:TextBox ID="txtPesquisa"
                                            runat="server"
                                            CssClass="form-control"
                                            placeholder="Pesquisar por nome ou titulação"
                                            Style="max-width: 255px;"
                                            Visible ="false">
                                        </asp:TextBox>
                                        <asp:Button ID="btnPesquisar"
                                            runat="server"
                                            Text="Pesquisar"
                                            CssClass="btn btn-primary"
                                            OnClick="btnPesquisar_Click"
                                            Visible ="false"/>
                                        <asp:Button ID="BtnMostrarTodos"
                                            runat="server"
                                            Text="Mostrar todos"
                                            CssClass="btn btn-success"
                                            OnClick="MostrarTodos" 
                                            Visible = "false"/>
                                </div>
                                <asp:GridView ID="gvCoordenadores"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-striped table-hover">

                                    <Columns>
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                        <asp:BoundField DataField="AreaAtuacao" HeaderText="Area de atuação" />
                                        <asp:BoundField DataField="Titulacao" HeaderText="Titulacão" />
                                    </Columns>

                                </asp:GridView>
                        
                            </asp:Panel>
                        </div>

                    </div>

                </div>
            </div>
       </div>
     </div>
        </asp:Content>
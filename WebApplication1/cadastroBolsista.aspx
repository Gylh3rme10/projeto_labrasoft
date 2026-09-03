<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.CadastroBolsista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">

                    <div class="card-header bg-primary text-white">
                        <h2 class="mb-0">Cadastro de Bolsista</h2>
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
                                <asp:TextBox ID="txtCpf" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite o CPF">
                                </asp:TextBox>
                            </div>
                        </div>

            <!-- Segunda linha -->
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label class="form-label">Matrícula:</label>
                                <asp:TextBox ID="txtMatricula" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite a matrícula">
                                </asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Data de Nascimento:</label>
                                <asp:TextBox ID="dateBirth" runat="server"
                                    TextMode="Date"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Sexo:</label>
                                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                    <asp:ListItem Value="O">Outro/Não informado</asp:ListItem>
                                </asp:DropDownList>
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
                                    OnClick="BtnSalvar_Click" />
                            </div>

                            <div class="col-md-3 d-grid">
                                <asp:Button ID="BtnLimpar"
                                    runat="server"
                                    Text="Limpar"
                                    CssClass="btn btn-secondary"
                                    OnClick="BtnLimpar_Click" />
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
                            <h3 class="text-secondary">Lista de Bolsistas Cadastrados</h3>
                            <div class="mt-3">
                                <asp:Label ID="lblAviso"
                                    runat="server"
                                    CssClass="text-danger">
                                </asp:Label>
                            </div>
                            <asp:Panel ID="pneBolsistas" runat="server">
                                <div class="d-flex gap-2 mt-3">
                                    <asp:Button ID="BtnFiltrarMulheres"
                                        runat="server"
                                        Text="Filtrar mulheres"
                                        CssClass="btn btn-danger"
                                        OnClick="FiltrarMulheres" 
                                        Visible ="false"/>

                                    <asp:Button ID="BtnOrdenarPorNome"
                                        runat="server"
                                        Text="Ordenar por nome"
                                        CssClass="btn btn-secondary"
                                        OnClick="OrdenarPorNome" 
                                        Visible ="false"/>

                                    <asp:Button ID="BtnMostrarTodos"
                                        runat="server"
                                        Text="Mostrar todos"
                                        CssClass="btn btn-success"
                                        OnClick="MostrarTodos" 
                                        Visible = "false"/>
                           
                                </div>
                                <br />
                                <asp:Label ID="lblBotao"
                                    runat="server"
                                    CssClass="text-success">
                                </asp:Label>
                                <hr />
               <!--Gridview-->
                                <asp:GridView ID="gvBolsistas"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-striped table-hover">

                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="Id" />
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
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
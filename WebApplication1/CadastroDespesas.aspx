<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroDespesas.aspx.cs" Inherits="WebApplication1.CadastroDespesas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">

                    <div class="card-header bg-primary text-white">
                        <h2 class="mb-0">Despesas</h2>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <!-- Coluna da Esquerda: Valor, Data e Projeto -->
                            <div class="col-md-6">
                                 <!-- Linha para colocar Valor e Data lado a lado -->
                                 <div class="row">
                                      <div class="col-md-6">
                                            <div class="mb-3">
                                                <label class="form-label">Valor:</label>
                                                <asp:TextBox ID="txtValorDespesa" runat="server"
                                                    CssClass="form-control"
                                                    placeholder="0,00"
                                                    textmode="Number">
                                                </asp:TextBox>
                                            </div>
                                      </div>
                                      <div class="col-md-6">
                                            <div class="mb-3">
                                                <label class="form-label">Data:</label>
                                                <asp:TextBox ID="dataDespesa" runat="server"
                                                    TextMode="Date"
                                                    CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                      </div>
                                 </div> <!-- Fim da row interna (Valor + Data) -->

                                 <div class="mb-3">
                                    <label class="form-label">Projeto:</label>
                                    <asp:DropDownList ID="ddlProjeto" runat="server" CssClass="form-select">
                                    </asp:DropDownList>
                                 </div>
                            </div>

                            <!-- Coluna da Direita: Descrição -->
                            <div class="col-md-6">
                                <div class="mb-3">
                                    <label for="txtCategoria" class="form-label">Categoria:</label>
                                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtDescricao" class="form-label">Descrição</label>
                                    <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Botões -->
                        <div class="row mt-3">
                            <div class="col-md-9 d-grid">
                                <asp:Button ID="BtnSalvar"
                                    runat="server"
                                    Text="Salvar"
                                    CssClass="btn btn-success"
                                    onclick="BtnSalvar_Click"
                                    />
                            </div>
                            <div class="col-md-3 d-grid">
                                <asp:Button ID="BtnLimpar"
                                    runat="server"
                                    Text="Limpar"
                                    CssClass="btn btn-secondary"
                                    onclick="BtnLimpar_Click"
                                    />
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="mt-3">
                                <asp:Label ID="lblMensagem"
                                    runat="server"
                                    CssClass="h6">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

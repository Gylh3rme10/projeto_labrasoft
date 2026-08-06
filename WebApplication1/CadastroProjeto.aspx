<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-9 col-xl-8">
                <div class="card shadow">

                    <div class="card-header bg-primary text-white">
                        <h2 class="mb-0">Projetos</h2>
                    </div>

                   <div class="card-body p-4">

                    <!-- Dados do Projeto -->
                    <div class="card border-0 shadow-sm mb-4">
                        <div class="card-header bg-light">
                            <h5 class="mb-0 text-primary">📁 Dados do Projeto</h5>
                        </div>

                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-8 mb-3">
                                    <label class="form-label fw-semibold">Título</label>

                                    <asp:TextBox
                                        ID="txtTitulo"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Digite o título do projeto">
                                    </asp:TextBox>

                                </div>

                                <div class="col-md-4 mb-3">

                                    <label class="form-label fw-semibold">
                                        Área de conhecimento
                                    </label>

                                    <asp:TextBox
                                        ID="txtAreaConhecimento"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Digite a área">
                                    </asp:TextBox>

                                </div>

                            </div>

                           <div class="row">

                            <!-- Verba Total -->
                            <div class="col-md-6 mb-3">

                                <label class="form-label fw-semibold">
                                    Verba Total
                                </label>

                                <asp:TextBox
                                    ID="txtVerba"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Number"
                                    placeholder="Digite a verba total">
                                </asp:TextBox>

                            </div>

                            <!-- Verba Individual -->
                            <div class="col-md-6 mb-3">

                                <label class="form-label fw-semibold">
                                    Verba Individual
                                </label>

                                <asp:TextBox
                                    ID="txtVerbaIndividual"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Number"
                                    placeholder="Digite a verba por bolsista">
                                </asp:TextBox>

                            </div>

                        </div>

                        </div>
                    </div>


                    <!-- Equipe -->
                    <div class="card border-0 shadow-sm">

                        <div class="card-header bg-light">
                            <h5 class="mb-0 text-primary">👥 Equipe do Projeto</h5>
                        </div>

                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-6">

                                    <label class="form-label fw-semibold">
                                        Coordenador
                                    </label>

                                    <asp:DropDownList
                                        ID="ddlCoordenador"
                                        runat="server"
                                        CssClass="form-select">

                                        <asp:ListItem
                                            Text="-- Selecione um coordenador --"
                                            Value="">
                                        </asp:ListItem>

                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-6">

                                    <label class="form-label fw-semibold">
                                        Bolsistas
                                    </label>

                                    <asp:ListBox
                                        ID="lstBolsistas"
                                        runat="server"
                                        CssClass="form-control"
                                        SelectionMode="Multiple"
                                        Rows="7">
                                    </asp:ListBox>

                                </div>

                            </div>

                        </div>

                    </div>

                    <hr class="my-4"/>

                    <!-- Botões -->
                    <div class="row">

                        <div class="col-md-9 d-grid">

                            <asp:Button
                                ID="BtnSalvar"
                                runat="server"
                                Text="Salvar"
                                CssClass="btn btn-success btn-lg"
                                OnClick="BtnSalvar_Click"/>

                        </div>

                        <div class="col-md-3 d-grid">

                            <asp:Button
                                ID="BtnLimpar"
                                runat="server"
                                Text="Limpar"
                                CssClass="btn btn-outline-secondary btn-lg"
                                OnClick="BtnLimpar_Click"/>

                        </div>

                    </div>

                </div>

                <%-- Lista de Projetos --%>

                <div class="card-body">

                    <div class="mt-3">
                        <asp:Label ID="lblMensagem"
                            runat="server"
                            CssClass="h6">
                        </asp:Label>
                    </div>

                    <h3 class="text-secondary mb-3">
                        Projetos cadastrados
                    </h3>

                    <asp:GridView ID="gvProjetos"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="table table-hover table-bordered align-middle"
                        DataKeyNames="Id"
                        OnRowCommand="gvProjetos_RowCommand">

                        <Columns>

                            <asp:BoundField
                                DataField="Titulo"
                                HeaderText="Título" />

                            <asp:BoundField
                                DataField="AreaConhecimento"
                                HeaderText="Área de Conhecimento" />

                            <asp:BoundField
                                DataField="VerbaAprovada"
                                HeaderText="Verba"
                                DataFormatString="{0:C}" />

                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>

                                    <asp:Button
                                        ID="btnDetalhes"
                                        runat="server"
                                        Text="Detalhes"
                                        CssClass="btn btn-outline-primary btn-sm"
                                        CommandName="Detalhes"
                                        CommandArgument='<%# Eval("Id") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                    <asp:Panel ID="pnlDetalhes"
                        runat="server"
                        Visible="false"
                        CssClass="card border-primary mt-4">

                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0">Detalhes do Projeto</h5>
                        </div>

                        <div class="card-body">

                            <asp:GridView
                                ID="gvDetalhesProjeto"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped">

                                <Columns>

                                    <asp:BoundField
                                        DataField="Titulo"
                                        HeaderText="Título" />

                                    <asp:BoundField
                                        DataField="AreaConhecimento"
                                        HeaderText="Área" />

                                    <asp:BoundField
                                        DataField="Coordenador"
                                        HeaderText="Coordenador" />
                                    <asp:BoundField
                                        DataField="Bolsistas"
                                        HeaderText="Bolsistas" />
                                    <asp:BoundField
                                        DataField="VerbaAprovada"
                                        HeaderText="Verba"
                                        DataFormatString="{0:C}" />

                                    <asp:BoundField
                                        DataField="TotalDespesas"
                                        HeaderText="Despesas"
                                        DataFormatString="{0:C}" />

                                    <asp:BoundField
                                        DataField="Saldo"
                                        HeaderText="Saldo"
                                        DataFormatString="{0:C}" />

                                </Columns>

                            </asp:GridView>

                            <div class="mt-3">

                                <asp:Button
                                    ID="btnVerDespesas"
                                    runat="server"
                                    Text="Ver despesas"
                                    CssClass="btn btn-primary"
                                    OnClick="btnVerDespesas_Click" />

                                <asp:Button
                                    ID="btnOcultarDetalhes"
                                    runat="server"
                                    Text="Ocultar detalhes"
                                    CssClass="btn btn-secondary ms-2"
                                    OnClick="btnFecharDetalhes_Click" />

                            </div>

                        </div>

                    </asp:Panel>

                    <asp:Panel
                        ID="pnlDespesas"
                        runat="server"
                        Visible="false"
                        CssClass="card border-secondary mt-3">

                        <div class="card-header bg-secondary text-white">
                            <h5 class="mb-0">Despesas do Projeto</h5>
                        </div>

                        <div class="card-body">

                            <asp:GridView
                                ID="gvDespesas"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered">

                                <Columns>

                                    <asp:BoundField
                                        DataField="Data"
                                        HeaderText="Data"
                                        DataFormatString="{0:dd/MM/yyyy}" />

                                    <asp:BoundField
                                        DataField="Categoria"
                                        HeaderText="Categoria" />

                                    <asp:BoundField
                                        DataField="Descricao"
                                        HeaderText="Descrição">
                                        <ItemStyle Width="500px" Wrap="true" />
                                    </asp:BoundField>

                                    <asp:BoundField
                                        DataField="Valor"
                                        HeaderText="Valor"
                                        DataFormatString="{0:C}" />

                                </Columns>

                            </asp:GridView>

                            <asp:Button
                                ID="btnFecharDespesas"
                                runat="server"
                                Text="Fechar despesas"
                                CssClass="btn btn-secondary"
                                OnClick="btnFecharDespesas_Click" />

                        </div>

                    </asp:Panel>

                </div>
                </div>
            </div>
       </div>
   </div>
  </asp:Content>
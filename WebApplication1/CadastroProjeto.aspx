<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">

                    <div class="card-header bg-primary text-white">
                        <h2 class="mb-0">Projetos</h2>
                    </div>

                    <div class="card-body">

                        <!-- Primeira linha -->
                        <div class="row">
                            <div class="col-md-8 mb-3">
                                <label class="form-label">Titulo:</label>
                                <asp:TextBox ID="txtTitulo" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite o titulo do projeto">
                                </asp:TextBox>
                            </div>

                            <div class="col-md-4 mb-3">
                                <label class="form-label">Area de conhecimento:</label>
                                <asp:TextBox ID="txtAreaConhecimento" runat="server"
                                    CssClass="form-control"
                                    placeholder="Digite a area de conhecimento">
                                </asp:TextBox>
                            </div>
                        </div>
                        <!-- Segunda linha -->
                        <div class="row">
                            
                            <div class="col-md-6 mb-3">
                                <label class="form-label">Verba:</label>
                                <asp:TextBox ID="txtVerba" runat="server"
                                    CssClass="form-control"
                                    TextMode="Number">
                                </asp:TextBox>
                            </div>

                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblBolsistas" runat="server" Text="Bolsistas"></asp:Label>

                                <asp:ListBox
                                    ID="lstBolsistas"
                                    runat="server"
                                    CssClass="form-control"
                                    SelectionMode="Multiple"
                                    Rows="6">
                                </asp:ListBox>
                            </div>
                        </div>

                        <!-- Terceira linha: Area de atuação -->
                        <div class="row">
                            <div class="col-md-6 mb-3">
                               <asp:Label ID="lblCoordenador" runat="server" Text="Coordenador"></asp:Label>

                                <asp:DropDownList
                                    ID="ddlCoordenador"
                                    runat="server"
                                    CssClass="form-select">
                                    <asp:ListItem Text="-- Selecione um coordenador --" Value=""></asp:ListItem>
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

                        <%--lista--%>
                        <h3 class="text-secondary">Lista de Projetos</h3>

                        <asp:GridView ID="gvProjetos"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-bordered"
                            DataKeyNames="Titulo"
                            OnRowCommand="gvProjetos_RowCommand">

                            <Columns>

                                <asp:BoundField DataField="Titulo" HeaderText="Título" />

                                <asp:BoundField DataField="AreaConhecimento" HeaderText="Área de Conhecimento" />

                                <asp:BoundField DataField="Verba"
                                    HeaderText="Verba"
                                    DataFormatString="{0:C}" />

                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Button
                                            ID="btnDetalhes"
                                            runat="server"
                                            Text="Detalhes"
                                            CssClass="btn btn-info btn-sm"
                                            CommandName="Detalhes"
                                            CommandArgument="<%# Container.DataItemIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                        <asp:Panel ID="pnlDetalhes" runat="server" CssClass="alert alert-info mt-3" Visible="false">
                            <asp:Label ID="lblDetalhes" runat="server"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
       </div>
     </div>
        </asp:Content>
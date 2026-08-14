<%@ Page Title="Usuários" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Usuario.aspx.cs"
    Inherits="WebApplication1.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <asp:MultiView ID="mvUsuario" runat="server" ActiveViewIndex="0">

    <!-- LOGIN -->
    <asp:View ID="vwLogin" runat="server">

        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-4">
                    <div class="card shadow mt-5">
                        <div class="card-body p-4">
                            <h2 class="text-center mb-4">Login</h2>

                            <!-- Campos do login aqui -->
                            <div class="mb-3">
                                <asp:Label
                                    ID="lblEmailLogin"
                                    runat="server"
                                    Text="E-mail"
                                    CssClass="form-label" />

                                <asp:TextBox
                                    ID="txtEmailLogin"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Email" />
                            </div>

                            <div class="mb-3">
                                <asp:Label
                                    ID="lblSenhaLogin"
                                    runat="server"
                                    Text="Senha"
                                    CssClass="form-label" />

                                <asp:TextBox
                                    ID="txtSenhaLogin"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Password" />
                            </div>

                            <div class="d-grid">
                                <asp:Button
                                    ID="btnLogin"
                                    runat="server"
                                    Text="Entrar"
                                    CssClass="btn btn-primary"
                                    OnClick="btnLogin_Click" />
                            </div>

                            <div class="text-center mt-3">
                                <asp:LinkButton
                                    ID="lnkCadastro"
                                    runat="server"
                                    Text="Ainda não tenho uma conta"
                                    CssClass="text-decoration-none"
                                    OnClick="lnkCadastro_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

    </asp:View>


    <!-- CADASTRO -->
    <asp:View ID="vwCadastro" runat="server">

        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-4">
                    <div class="card shadow mt-5">
                        <div class="card-body p-4">
                            <h2 class="text-center mb-4">Cadastro</h2>

                            <!-- Campos do cadastro aqui -->
                            <!-- Nome -->
                            <div class="mb-3">
                                <asp:Label
                                    ID="Label1"
                                    runat="server"
                                    Text="Nome"
                                    CssClass="form-label" />

                                <asp:TextBox
                                    ID="txtNome"
                                    runat="server"
                                    CssClass="form-control"/>
                            </div>
                            <!-- email -->
                            <div class="mb-3">
                                <asp:Label
                                    ID="Label2"
                                    runat="server"
                                    Text="E-mail"
                                    CssClass="form-label" />

                                <asp:TextBox
                                    ID="txtEmail"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Email" />
                            </div>
                            <!-- Senha -->
                            <div class="mb-3">
                                <asp:Label
                                    ID="Label3"
                                    runat="server"
                                    Text="Senha"
                                    CssClass="form-label" />

                                <asp:TextBox
                                    ID="txtSenha"
                                    runat="server"
                                    CssClass="form-control"
                                    TextMode="Password" />
                            </div>

                            <div class="d-grid">
                                <asp:Button
                                    ID="btnCadastro"
                                    runat="server"
                                    Text="Cadastrar"
                                    CssClass="btn btn-primary"
                                    OnClick="btnCadastro_Click" />
                            </div>

                            <div class="text-center mt-3">
                                <asp:LinkButton
                                    ID="LinkButton1"
                                    runat="server"
                                    Text="Já tenho uma conta"
                                    CssClass="text-decoration-none"
                                    OnClick="lnkLogin_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </asp:View>

</asp:MultiView>

</asp:Content>
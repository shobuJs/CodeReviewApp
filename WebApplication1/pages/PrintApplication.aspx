<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PrintApplication.aspx.cs" Inherits="PrintApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="updPanel1">
        <ContentTemplate>

            <div class="right_col" role="main">
                <div class="container-fluid">
                    <div class="padd-t-20">
                        <div class="text-center padd-tb-10">
                            <h4 style="color:green;">Your Information has been updated successfully ! Please Print your application form here.</h4>
                            <br />
                            <br />
                            <div>
                                <asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="Print Application" OnClick="btnReport_Click" />
                            </div>
                            <br />
                            <%--<h3><a href="StudentInfo.aspx"> Click here </a> to go back to your Application Form.</h3>--%>
                            <h3><a href="https://svce.mastersofterp.in/academic/StudentInfo.aspx"> Click here </a> to go back to your Application Form.</h3>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server"></div>
</asp:Content>


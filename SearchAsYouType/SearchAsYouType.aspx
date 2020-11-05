<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SearchAsYouType.Master" CodeBehind="SearchAsYouType.aspx.vb" Inherits="SearchAsYouType.SearchAsYouType1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script type='text/javascript'>
        $(document).ready(function () {
            //Attach on change event to textbox here
            $("#TextBox1").change(function () {
                //Remove all previously added rows in gridview
                $("[id*=sampleGV] tr").not($("[id*=sampleGV] tr:first-child")).remove();
                $.ajax({
                    type: "POST",
                    //Call the method to get data from database 
                    url: "SearchAsYouType.aspx/GetOrderDetails",
                    //Pass the value from textbox to method
                    data: '{"companyname":' + $("#TextBox1").val() + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        //Add the resul to gridview
                        $("#sampleGV").append("<tr><td>" + result.d[0][0] + "</td><td>" + result.d[0][1] + "</td><td>" + result.d[0][2] + "</td></tr>");
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:GridView ID="sampleGV" runat="server" ShowHeaderWhenEmpty="true"
                CellPadding="0" CellSpacing="0">
            </asp:GridView>
   
</asp:Content>

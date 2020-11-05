Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Net.Mail
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Diagnostics.Debug

Public Class SearchAsYouType1
    Inherits System.Web.UI.Page
    'Dim Constr As String = WebConfigurationManager.ConnectionStrings("Audit_DataConnectionString").ConnectionString
    'Dim connect As New SqlConnection

    'Dim Constr_EWS As String = WebConfigurationManager.ConnectionStrings("ESW_ConnectionString").ConnectionString
    'Dim connect_EWS As New SqlConnection

    'Dim Constr_LF02 As String = WebConfigurationManager.ConnectionStrings("LF02_ConnectionString").ConnectionString
    'Dim connect_LF02 As New SqlConnection
    Private oriDataTable As New DataTable
    Private columnToFilter As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Load and empty row to gridview
        If Not IsPostBack Then
            Dim dt As New DataTable()
            dt.Columns.Add("company_name")
            dt.Columns.Add("mc")
            sampleGV.DataSource = dt
            sampleGV.DataBind()
        End If
    End Sub

    <Services.WebMethod>
    Public Shared Function GetOrderDetails(ByVal companyname As String) As Array

        'Create sql connection object here
        Dim conn As SqlConnection = Nothing
        'Create sql DataTable object here
        Dim dt As New DataTable
        'Create sql SqlDataAdapter object here
        Dim da As SqlDataAdapter
        'Create sql ArrayList object here
        Dim rows As New ArrayList()
        'Get the connectionstring from webconfig file
        Dim connection As String = ConfigurationManager.ConnectionStrings("VITLLF02").ConnectionString
        'Create sql SqlConnection object here and assign the conenctionstring
        conn = New SqlConnection(connection)
        'Create sql query here
        Dim sql As String = "SELECT * FROM DClientList a WHERE company_name = @companyname"
        'Create sql SqlCommand and assign connection and command here
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        'Pass your parameter here
        cmd.Parameters.AddWithValue("@companyname", companyname)
        'Open the SQLConnection here
        conn.Open()
        'Create SqlDataAdapter object here
        da = New SqlDataAdapter(cmd)
        Using ds As New DataSet()
            'Load the DataTable
            da.Fill(dt)
            For Each dataRow As DataRow In dt.Rows
                rows.Add(dataRow.ItemArray.[Select](Function(item) item.ToString()))
            Next
            'Create an array and then return the values
            Return rows.ToArray()
        End Using

        conn.Close()
    End Function

  


End Class
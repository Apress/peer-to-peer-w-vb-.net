﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.0.3705.288
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 1.0.3705.288.
'
Namespace localhost
    
    '<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="DiscoveryServiceSoap", [Namespace]:="www.prosetech.com")>  _
    Public Class DiscoveryService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        '<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = "http://localhost/Discovery/DiscoveryService.asmx"
        End Sub
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("www.prosetech.com/Register", RequestNamespace:="www.prosetech.com", ResponseNamespace:="www.prosetech.com", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Register(ByVal peer As Peer) As Boolean
            Dim results() As Object = Me.Invoke("Register", New Object() {peer})
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        Public Function BeginRegister(ByVal peer As Peer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Register", New Object() {peer}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndRegister(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("www.prosetech.com/RefreshRegistration", RequestNamespace:="www.prosetech.com", ResponseNamespace:="www.prosetech.com", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RefreshRegistration(ByVal peer As Peer) As Boolean
            Dim results() As Object = Me.Invoke("RefreshRegistration", New Object() {peer})
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        Public Function BeginRefreshRegistration(ByVal peer As Peer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("RefreshRegistration", New Object() {peer}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndRefreshRegistration(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("www.prosetech.com/Unregister", RequestNamespace:="www.prosetech.com", ResponseNamespace:="www.prosetech.com", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Sub Unregister(ByVal peer As Peer)
            Me.Invoke("Unregister", New Object() {peer})
        End Sub
        
        '<remarks/>
        Public Function BeginUnregister(ByVal peer As Peer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Unregister", New Object() {peer}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Sub EndUnregister(ByVal asyncResult As System.IAsyncResult)
            Me.EndInvoke(asyncResult)
        End Sub
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("www.prosetech.com/PublishFiles", RequestNamespace:="www.prosetech.com", ResponseNamespace:="www.prosetech.com", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function PublishFiles(ByVal files() As SharedFile, ByVal peer As Peer) As Boolean
            Dim results() As Object = Me.Invoke("PublishFiles", New Object() {files, peer})
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        Public Function BeginPublishFiles(ByVal files() As SharedFile, ByVal peer As Peer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("PublishFiles", New Object() {files, peer}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndPublishFiles(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("www.prosetech.com/SearchForFile", RequestNamespace:="www.prosetech.com", ResponseNamespace:="www.prosetech.com", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SearchForFile(ByVal keywords() As String) As SharedFile()
            Dim results() As Object = Me.Invoke("SearchForFile", New Object() {keywords})
            Return CType(results(0),SharedFile())
        End Function
        
        '<remarks/>
        Public Function BeginSearchForFile(ByVal keywords() As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("SearchForFile", New Object() {keywords}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndSearchForFile(ByVal asyncResult As System.IAsyncResult) As SharedFile()
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),SharedFile())
        End Function
    End Class
    
    '<remarks/>
    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="www.prosetech.com")>  _
    Public Class Peer
        
        '<remarks/>
        Public Guid As System.Guid
        
        '<remarks/>
        Public IP As String
        
        '<remarks/>
        Public Port As Integer
    End Class
    
    '<remarks/>
    <System.Xml.Serialization.XmlTypeAttribute([Namespace]:="www.prosetech.com")>  _
    Public Class SharedFile
        
        '<remarks/>
        Public Guid As System.Guid
        
        '<remarks/>
        Public FileName As String
        
        '<remarks/>
        Public FileCreated As Date
        
        '<remarks/>
        Public Peer As Peer
        
        '<remarks/>
        Public Keywords() As String
    End Class
End Namespace

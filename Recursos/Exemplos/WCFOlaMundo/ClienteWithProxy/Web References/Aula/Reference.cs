﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.225.
// 
#pragma warning disable 1591

namespace ClienteWithProxy.Aula {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IServiceOla", Namespace="http://tempuri.org/")]
    public partial class ServiceOla : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback olaSimplesOperationCompleted;
        
        private System.Threading.SendOrPostCallback olaPessoaOperationCompleted;
        
        private System.Threading.SendOrPostCallback getPessoaOperationCompleted;
        
        private System.Threading.SendOrPostCallback changeStateOperationCompleted;
        
        private System.Threading.SendOrPostCallback getStateOperationCompleted;
        
        private System.Threading.SendOrPostCallback testeOperationCompleted;
        
        private System.Threading.SendOrPostCallback getDataMTOMOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ServiceOla() {
            this.Url = global::ClienteWithProxy.Properties.Settings.Default.ClienteWithProxy_Aula_ServiceOla;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event olaSimplesCompletedEventHandler olaSimplesCompleted;
        
        /// <remarks/>
        public event olaPessoaCompletedEventHandler olaPessoaCompleted;
        
        /// <remarks/>
        public event getPessoaCompletedEventHandler getPessoaCompleted;
        
        /// <remarks/>
        public event changeStateCompletedEventHandler changeStateCompleted;
        
        /// <remarks/>
        public event getStateCompletedEventHandler getStateCompleted;
        
        /// <remarks/>
        public event testeCompletedEventHandler testeCompleted;
        
        /// <remarks/>
        public event getDataMTOMCompletedEventHandler getDataMTOMCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/olaSimples", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string olaSimples([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string nome) {
            object[] results = this.Invoke("olaSimples", new object[] {
                        nome});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void olaSimplesAsync(string nome) {
            this.olaSimplesAsync(nome, null);
        }
        
        /// <remarks/>
        public void olaSimplesAsync(string nome, object userState) {
            if ((this.olaSimplesOperationCompleted == null)) {
                this.olaSimplesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnolaSimplesOperationCompleted);
            }
            this.InvokeAsync("olaSimples", new object[] {
                        nome}, this.olaSimplesOperationCompleted, userState);
        }
        
        private void OnolaSimplesOperationCompleted(object arg) {
            if ((this.olaSimplesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.olaSimplesCompleted(this, new olaSimplesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/olaPessoa", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string olaPessoa([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] Pessoa pes) {
            object[] results = this.Invoke("olaPessoa", new object[] {
                        pes});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void olaPessoaAsync(Pessoa pes) {
            this.olaPessoaAsync(pes, null);
        }
        
        /// <remarks/>
        public void olaPessoaAsync(Pessoa pes, object userState) {
            if ((this.olaPessoaOperationCompleted == null)) {
                this.olaPessoaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnolaPessoaOperationCompleted);
            }
            this.InvokeAsync("olaPessoa", new object[] {
                        pes}, this.olaPessoaOperationCompleted, userState);
        }
        
        private void OnolaPessoaOperationCompleted(object arg) {
            if ((this.olaPessoaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.olaPessoaCompleted(this, new olaPessoaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/getPessoa", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public Pessoa getPessoa([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string firstName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string lastName) {
            object[] results = this.Invoke("getPessoa", new object[] {
                        firstName,
                        lastName});
            return ((Pessoa)(results[0]));
        }
        
        /// <remarks/>
        public void getPessoaAsync(string firstName, string lastName) {
            this.getPessoaAsync(firstName, lastName, null);
        }
        
        /// <remarks/>
        public void getPessoaAsync(string firstName, string lastName, object userState) {
            if ((this.getPessoaOperationCompleted == null)) {
                this.getPessoaOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetPessoaOperationCompleted);
            }
            this.InvokeAsync("getPessoa", new object[] {
                        firstName,
                        lastName}, this.getPessoaOperationCompleted, userState);
        }
        
        private void OngetPessoaOperationCompleted(object arg) {
            if ((this.getPessoaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getPessoaCompleted(this, new getPessoaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/changeState", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void changeState(int val, [System.Xml.Serialization.XmlIgnoreAttribute()] bool valSpecified) {
            this.Invoke("changeState", new object[] {
                        val,
                        valSpecified});
        }
        
        /// <remarks/>
        public void changeStateAsync(int val, bool valSpecified) {
            this.changeStateAsync(val, valSpecified, null);
        }
        
        /// <remarks/>
        public void changeStateAsync(int val, bool valSpecified, object userState) {
            if ((this.changeStateOperationCompleted == null)) {
                this.changeStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnchangeStateOperationCompleted);
            }
            this.InvokeAsync("changeState", new object[] {
                        val,
                        valSpecified}, this.changeStateOperationCompleted, userState);
        }
        
        private void OnchangeStateOperationCompleted(object arg) {
            if ((this.changeStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.changeStateCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/getState", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void getState(out int getStateResult, [System.Xml.Serialization.XmlIgnoreAttribute()] out bool getStateResultSpecified) {
            object[] results = this.Invoke("getState", new object[0]);
            getStateResult = ((int)(results[0]));
            getStateResultSpecified = ((bool)(results[1]));
        }
        
        /// <remarks/>
        public void getStateAsync() {
            this.getStateAsync(null);
        }
        
        /// <remarks/>
        public void getStateAsync(object userState) {
            if ((this.getStateOperationCompleted == null)) {
                this.getStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetStateOperationCompleted);
            }
            this.InvokeAsync("getState", new object[0], this.getStateOperationCompleted, userState);
        }
        
        private void OngetStateOperationCompleted(object arg) {
            if ((this.getStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getStateCompleted(this, new getStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/teste", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] teste([System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")] string[] str) {
            object[] results = this.Invoke("teste", new object[] {
                        str});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void testeAsync(string[] str) {
            this.testeAsync(str, null);
        }
        
        /// <remarks/>
        public void testeAsync(string[] str, object userState) {
            if ((this.testeOperationCompleted == null)) {
                this.testeOperationCompleted = new System.Threading.SendOrPostCallback(this.OntesteOperationCompleted);
            }
            this.InvokeAsync("teste", new object[] {
                        str}, this.testeOperationCompleted, userState);
        }
        
        private void OntesteOperationCompleted(object arg) {
            if ((this.testeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.testeCompleted(this, new testeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IServiceOla/getDataMTOM", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)]
        public byte[] getDataMTOM([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true)] byte[] arg) {
            object[] results = this.Invoke("getDataMTOM", new object[] {
                        arg});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void getDataMTOMAsync(byte[] arg) {
            this.getDataMTOMAsync(arg, null);
        }
        
        /// <remarks/>
        public void getDataMTOMAsync(byte[] arg, object userState) {
            if ((this.getDataMTOMOperationCompleted == null)) {
                this.getDataMTOMOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDataMTOMOperationCompleted);
            }
            this.InvokeAsync("getDataMTOM", new object[] {
                        arg}, this.getDataMTOMOperationCompleted, userState);
        }
        
        private void OngetDataMTOMOperationCompleted(object arg) {
            if ((this.getDataMTOMCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDataMTOMCompleted(this, new getDataMTOMCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Contratos")]
    public partial class Pessoa {
        
        private string firstNameField;
        
        private string lastNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string LastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void olaSimplesCompletedEventHandler(object sender, olaSimplesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class olaSimplesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal olaSimplesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void olaPessoaCompletedEventHandler(object sender, olaPessoaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class olaPessoaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal olaPessoaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getPessoaCompletedEventHandler(object sender, getPessoaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getPessoaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getPessoaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Pessoa Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Pessoa)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void changeStateCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getStateCompletedEventHandler(object sender, getStateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int getStateResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public bool getStateResultSpecified {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void testeCompletedEventHandler(object sender, testeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class testeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal testeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getDataMTOMCompletedEventHandler(object sender, getDataMTOMCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDataMTOMCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getDataMTOMCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
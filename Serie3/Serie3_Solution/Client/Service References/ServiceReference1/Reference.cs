﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ICentralService")]
    public interface ICentralService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICentralService/Register", ReplyAction="http://tempuri.org/ICentralService/RegisterResponse")]
        int Register(string endpoint);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICentralService/UnRegister", ReplyAction="http://tempuri.org/ICentralService/UnRegisterResponse")]
        void UnRegister(int clientId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICentralServiceChannel : Client.ServiceReference1.ICentralService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CentralServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.ICentralService>, Client.ServiceReference1.ICentralService {
        
        public CentralServiceClient() {
        }
        
        public CentralServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CentralServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CentralServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CentralServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Register(string endpoint) {
            return base.Channel.Register(endpoint);
        }
        
        public void UnRegister(int clientId) {
            base.Channel.UnRegister(clientId);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Contratos
{
    [ServiceContract()]
    public interface IServiceOla
    {
        [OperationContract]
        string olaSimples(string nome);
        [OperationContract]
        string olaPessoa(Pessoa pes);
        [OperationContract]
        Pessoa getPessoa(string firstName, string lastName);
        [OperationContract]
        void changeState(int val);
        [OperationContract]
        int getState();
        [OperationContract]
        string[] teste(string[] str);
        [OperationContract]
        byte[] getDataMTOM(byte[] arg);
        [OperationContract]
        [FaultContract(typeof(DivideByZeroException))]
        int division(int a, int b);
        
    }

    [DataContract] //(Namespace = "http://ADEETC.ISEL")] 
    public class Pessoa
    {
        string firstName;
        string lastName;

        [DataMember] //(Name="FirstName")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        [DataMember] //(Name = "LastName")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}

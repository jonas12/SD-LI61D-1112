using System.ServiceModel;

namespace Contracts
{
    public interface ICService
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string msg);
    }
}
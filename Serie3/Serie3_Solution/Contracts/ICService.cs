using System.ServiceModel;

namespace Contracts
{
    public interface ICService
    {
        [OperationContract]
        int Method(string msg);
    }
}
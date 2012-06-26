using System.Security.Policy;
using System.ServiceModel;

namespace Contracts
{
    /// <summary>
    /// Represents the server's registration contract
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ICService))]
    public interface ICentralService
    {
        /// <summary>
        /// Registers a client's ICService endpoint for communication.
        /// </summary>
        /// <param name="endpoint">The url of the client's ICService.</param>
        /// <returns>client's server generated id</returns>
        [OperationContract]
        int Register(string endpoint);

        /// <summary>
        /// Unregister's a client from server's ICentralService.
        /// </summary>
        /// <param name="clientId">The client's id.</param>
        [OperationContract]
        void UnRegister(int clientId);
    }
}

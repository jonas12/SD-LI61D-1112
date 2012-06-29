using System.Collections.Generic;
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
        /// <param name="language"> The language a client is interested in for his messages</param>
        /// <param name="theme"> The theme a client is interested in for his messages</param>
        /// <returns>client's server generated id</returns>
        [OperationContract]
        int Register(int theme, int language);

        /// <summary>
        /// Unregister's a client from server's ICentralService.
        /// </summary>
        /// <param name="clientId">The client's id.</param>
        [OperationContract]
        void UnRegister(int clientId);

        [OperationContract]
        string[] GetSupportedLanguages();

        [OperationContract]
        string[] GetSupportedThemes();

        [OperationContract]
        void SubmitMessage(string msg);
    }
}

using AElf.Cryptography.ECDSA;
using AElf.Testing.TestBase;

namespace AElf.Contracts.ACS404
{
    // The Module class load the context required for unit testing
    public class Module : ContractTestModule<ACS404Contract>
    {
        
    }
    
    // The TestBase class inherit ContractTestBase class, it defines Stub classes and gets instances required for unit testing
    public class TestBase : ContractTestBase<Module>
    {
        // The Stub class for unit testing
        internal readonly ACS404ContractContainer.ACS404ContractStub ACS404ContractStub;
        // A key pair that can be used to interact with the contract instance
        private ECKeyPair DefaultKeyPair => Accounts[0].KeyPair;

        public TestBase()
        {
            ACS404ContractStub = GetACS404ContractContractStub(DefaultKeyPair);
        }

        private ACS404ContractContainer.ACS404ContractStub GetACS404ContractContractStub(ECKeyPair senderKeyPair)
        {
            return GetTester<ACS404ContractContainer.ACS404ContractStub>(ContractAddress, senderKeyPair);
        }
    }
    
}
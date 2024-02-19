using AElf.Sdk.CSharp.State;
using AElf.Types;

namespace AElf.Contracts.ACS404
{
    // The state class is access the blockchain state
    public partial class ACS404ContractState : ContractState 
    {
        // A state that holds string value
        public StringState Message { get; set; }
        public SingletonState<bool> Initialized { get; set; }
        public SingletonState<Address> ServiceWallet { get; set; }
        public SingletonState<Address> Admin { get; set; }
    }
}
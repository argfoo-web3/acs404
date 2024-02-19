using AElf.Sdk.CSharp.State;
using AElf.Types;
using Google.Protobuf;

namespace AElf.Contracts.ACS404
{
    using ImageBytes = ByteString;

    // The state class is access the blockchain state
    public partial class ACS404ContractState : ContractState 
    {
        public SingletonState<bool> Initialized { get; set; }
        public SingletonState<Address> ServiceWallet { get; set; }
        public SingletonState<Address> Admin { get; set; }
        public StringState FungibleTokenSymbol { get; set; }
        public StringState InscriptionTokenSymbol { get; set; }
        public MappedState<Address, ImageBytes> PreviewState { get; set; }
    }
}
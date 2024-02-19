using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf.WellKnownTypes;
using AElf.Contracts.MultiToken;

namespace AElf.Contracts.ACS404
{
    public partial class ACS404Contract
    {
        private void RequireTokenContractStateSet()
        {
            if (State.TokenContract.Value != null)
                return;

            State.TokenContract.Value =
                Context.GetContractAddressByName(SmartContractConstants.TokenContractSystemName);
        }

        private void AssertSenderIsAdmin()
        {
            Assert(Context.Sender == State.Admin.Value, $"No permission. Admin is {State.Admin.Value}. ");
        }

        private void AssertSenderIsImageUploader()
        {
            Assert(Context.Sender == State.ImageUploader.Value, $"No permission. Image uploader is {State.ImageUploader.Value}. ");
        }

        private void AssertSymbolExists(string symbol)
        {
            RequireTokenContractStateSet();

            var tokenInfo = State.TokenContract.GetTokenInfo.Call(new GetTokenInfoInput
            {
                Symbol = symbol
            });

            Assert(tokenInfo != null && !string.IsNullOrEmpty(tokenInfo.Symbol), $"Symbol: {symbol} does not exists!");
        }

        private void TransferFee(string symbol, long amount, Address from)
        {
            if (amount <= 0)
            {
                return;
            }

            RequireTokenContractStateSet();
            State.TokenContract.TransferFrom.Send(new TransferFromInput
            {
                From = from,
                Amount = amount,
                Symbol = symbol,
                To = State.ServiceWallet.Value,
                Memo = "Transaction fee."
            });
        }
    }
    
}
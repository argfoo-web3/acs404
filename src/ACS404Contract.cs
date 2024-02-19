using System;
using AElf.Sdk.CSharp;
using AElf.Types;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.ACS404
{
    // Contract class must inherit the base class generated from the proto file
    public partial class ACS404Contract : ACS404ContractContainer.ACS404ContractBase
    {
        public override Empty Initialize(InitializeInput input)
        {
            Assert(!State.Initialized.Value, "Already initialized.");
            Assert(input.Admin.Value != null, "Admin address is invalid!");
            Assert(input.ServiceWallet.Value != null, "Service wallet address is invalid!");
            Assert(input.ImageUploader.Value != null, "Image uploader address is invalid!");
            AssertSymbolExists(input.FungibleTokenSymbol);
            AssertSymbolExists(input.InscriptionTokenSymbol);

            State.Admin.Value = input.Admin;
            State.ServiceWallet.Value = input.ServiceWallet;
            State.ImageUploader.Value = input.ImageUploader;

            State.FungibleTokenSymbol.Value = input.FungibleTokenSymbol;
            State.InscriptionTokenSymbol.Value = input.InscriptionTokenSymbol;
            
            State.Initialized.Value = true;

            return new Empty();
        }

        public override Empty SetAdmin(Address address)
        {
            AssertSenderIsAdmin();

            State.Admin.Value = address;

            return new Empty();
        }

        public override Address GetAdmin(Empty input)
        {
            return State.Admin.Value;
        }

        public override Empty SetServiceWallet(Address address)
        {
            AssertSenderIsAdmin();

            State.ServiceWallet.Value = address;

            return new Empty();
        }

        public override Address GetServiceWallet(Empty input)
        {
            return State.ServiceWallet.Value;
        }

        public override Empty Preview(Empty input)
        {
            State.PreviewState[Context.Sender] = ByteString.Empty;
            TransferFee(State.FungibleTokenSymbol.Value, ContractConstants.PreviewFee, Context.Sender);
            
            Context.Fire(new PreviewEvent
            {
                UserAddress = Context.Sender,
                Traits = 0 //TODO
            });
            
            return new Empty();
        }

        public override Empty UploadImage(UploadImageInput input)
        {
            AssertSenderIsImageUploader();

            State.PreviewState[input.UserAddress] = input.ImageBytes;

            Context.Fire(new UploadImageEvent
            {
                UserAddress = input.UserAddress,
                ImageBytes = input.ImageBytes
            });

            return new Empty();
        }

        public override Empty Exchange(ExchangeInput input)
        {
            return new Empty();
        }

        public override Empty Mint(MintInput input)
        {
            return new Empty();
        }
    }
    
}
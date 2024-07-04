﻿namespace Xpto.Domain.Entities;

public class Asset
{
    public Guid Id { get; private set; }
    public string Address { get; private set; }
    public Guid BlockchainId { get; private set; }
    public Guid WalletId { get; private set; }
    public virtual Wallet Wallet { get; set; }
    public Asset(string address)
    {
        Id = Guid.NewGuid();
        Address = address;
        BlockchainId = Guid.NewGuid();
    }
    public void SetWalletId(Guid walletId) => WalletId = walletId;
    public Asset() { }
}
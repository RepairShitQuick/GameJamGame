using System;

namespace Assets.Networking.Identity
{
    public interface INetworkEntity
    {
        Guid AssociatedNetworkIdentity { get; }
        bool PlayerOwned { get; set; }
    }
}

using System;

namespace Assets.Networking.Identity
{
    public interface INetworkEntity
    {
        Guid AssociatedNetworkIdentity { get; set; }
        bool PlayerOwned { get; set; }
    }
}

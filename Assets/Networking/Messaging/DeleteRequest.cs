using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Networking.Messaging
{
    public class DeleteRequest
    {
        public Guid GuidToDelete { get; }
        public Type TypeOfComponent { get; }

        public DeleteRequest(SnapShot snapShot)
        {
            GuidToDelete = snapShot.NetworkGuid;
            TypeOfComponent = snapShot.ObjectType;
        }
    }
}

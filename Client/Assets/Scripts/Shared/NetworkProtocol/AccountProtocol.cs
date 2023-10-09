using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.NetworkProtocol
{

    public class AccountAuthRequest : RequestPacket
    {
        public override Protocol Protocol => Protocol.Account_Auth;
        // SDK를 통해 계정을 생성했다면 true
        public bool NeedCreate { get; set; }
    }

    public class AccountAuthResponse : ResponsePacket
    {
        public override Protocol Protocol => Protocol.Account_Auth;
    }


    public class AccountSyncRequest : RequestPacket
    {
        public override Protocol Protocol => Protocol.Account_Sync;
        
    }

    public class AccountSyncResponse : ResponsePacket
    {
        public override Protocol Protocol => Protocol.Account_Sync;
    }
}

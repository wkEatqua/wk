using Shared.Model;

namespace Shared.NetworkProtocol
{

    public class AccountAuthRequest : RequestPacket
    {
        public override Protocol Protocol => Protocol.Account_Auth;
        public string NickName { get; set; }
        public string Password { get; set; }
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
        public AccountSnapshot AccountSnapshot { get; set; }
    }

    public class AccountCreateRequest : RequestPacket
    {
        public override Protocol Protocol => Protocol.Account_Create;
        public string NickName { get; set; }
        public string Password { get; set; }
    }

    public class AccountCreateResponse : ResponsePacket
    {
        public override Protocol Protocol => Protocol.Account_Create;
        public AccountDB AccountDB { get; set; }
    }
}

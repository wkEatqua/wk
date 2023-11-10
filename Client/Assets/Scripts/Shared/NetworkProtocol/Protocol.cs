using Shared.Model;

namespace Shared.NetworkProtocol
{
    public enum Protocol
    {
        None = 0,
        Error,

        Account_Auth = 10000,
        Account_Create,
        Account_Sync,

        Attendance_Reward = 20000,

        Scenario_Clear = 30000,
    }


    public abstract class BasePacket
    {
        public SessionKey SessionKey { get; set; }
        public abstract Protocol Protocol { get; }
    }

    public abstract class RequestPacket : BasePacket
    {
        public long Hash { get; set; } = 0;
    }

    public abstract class ResponsePacket : BasePacket
    {
        public long ServerTimeTicks { get; set; }
    }

    public class ErrorPacket : ResponsePacket
    {
        public override Protocol Protocol => Protocol.Error;

        public string Reason { get; set; }

        public WebAPIErrorCode ErrorCode { get; set; }
    }

}

namespace Shared.NetworkProtocol
{
    public enum WebAPIErrorCode
    {
        None,
        InvalidPacket,
        InvalidProtocol,
        InternalServerError,
        ClientUpdateRequire,
        ServerFailedToHandleRequest,
        InvalidToken,
        DBError,

    }
}

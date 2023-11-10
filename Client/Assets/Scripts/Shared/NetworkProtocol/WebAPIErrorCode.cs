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

        SessionInvalidInput,
        SessionNotFound,
        SessionParseFail,
        SessionNotAuth,
        SessionDuplicateLogin,
        SessionTimeOver,

        AccountNickNameEmpty,
        AccountNotFound,
        AccountPasswordNotCorrect,



    }
}

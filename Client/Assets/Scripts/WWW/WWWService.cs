using Proyecto26;
using Shared.NetworkProtocol;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Core
{
    public static class WWWService
    {
        private readonly static string basePath = "https://localhost:7236/";
        private static RequestHelper currentRequest;

        public static void Post(Protocol protocol, RequestPacket request, bool enableDebug = false)
        {
            if (!ValidRequest(protocol, request, out var errorMessage))
            {
                Debug.LogError(errorMessage);
            }

            var rest = GetAPI(protocol);

            currentRequest = new RequestHelper
            {
                Uri = basePath + $"api/{rest.Controller}/{rest.Api}",

                SimpleForm = CreateForm(request),

                EnableDebug = enableDebug
            };


            RestClient.Post(currentRequest)
            .Then(res => {

                RestClient.ClearDefaultParams();
                
                Debug.Log("Success");
            })
            .Catch(err => Debug.LogError(err.Message));
        }

        private static bool ValidRequest(Protocol protocol, RequestPacket request, out string errorMessage)
        {
            errorMessage = null;

            if (request == null)
            {
                errorMessage = "request is null.";
                return false;
            }

            if (request is not AccountAuthRequest)
            {
                if (request.SessionKey.AccountId == 0)
                {
                    errorMessage = $"{request.Protocol} protocol account is empty.";
                    return false;
                }

                if (request.SessionKey.Token == null)
                {
                    errorMessage = $"{request.Protocol} protocol token is empty.";
                    return false;
                }
            }

            return protocol == request.Protocol;
        }

        private static Dictionary<string, string> CreateForm(RequestPacket request)
        {
            var requestJson = JsonService.SerializePlainObject(request);

            var form = new Dictionary<string, string>();
            form["protocol"] = ProtocolHashHelper.GetHash(request.Protocol);
            form["packet"] = requestJson;

            return form;
        }

        private static (string Controller, string Api) GetAPI(Protocol protocol)
        {
            var split = protocol.ToString().Split('_');

            return (split[0], split[1]);
        }
    }
}

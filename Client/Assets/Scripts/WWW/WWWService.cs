using Newtonsoft.Json.Linq;
using Proyecto26;
using Shared.NetworkProtocol;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Shared.Core
{
    public static class WWWService
    {
        private readonly static string basePath = "https://localhost:5001/";
        private static RequestHelper currentRequest;

        public static ResponsePacket Post(Protocol protocol, RequestPacket request, bool enableDebug = false)
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

            ResponsePacket responsePacket = null;

            RestClient.Post(currentRequest)
            .Then(res => {

                RestClient.ClearDefaultParams();
                
                Debug.Log($"Success: {res.Text}");

                responsePacket = TextToResponsePacket(protocol, res.Text);
            })
            .Catch(err => Debug.LogError(err.Message));

            return responsePacket;
        }

        private static bool ValidRequest(Protocol protocol, RequestPacket request, out string errorMessage)
        {
            errorMessage = null;

            if (request == null)
            {
                errorMessage = "request is null.";
                return false;
            }

            if (request is not AccountCreateRequest)
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

            return (split[0].ToLower(), split[1].ToLower());
        }

        private static ResponsePacket TextToResponsePacket(Protocol protocol, string text)
        {
            var parse = JObject.Parse(text);

            var cleanText = CleanInput(parse["packet"].ToString());
            
            var responseType = ParseResponsePacket(protocol);

            return (ResponsePacket)JsonService.DeserializePlainObject(responseType, cleanText);
        }

        private static Type ParseResponsePacket(Protocol protocol)
        {
            switch (protocol)
            {
                case Protocol.Account_Auth:
                    break;
                case Protocol.Account_Create:
                    return typeof(AccountCreateResponse);
                case Protocol.Account_Sync:
                    break;
                case Protocol.Attendance_Reward:
                    break;
                case Protocol.Scenario_Clear:
                    break;
                default: throw new Exception($"Check ParseResponsePacket, Add Protocol {protocol}");
            }

            return null;
        }

        static string CleanInput(string strIn)
        {
            try
            {
                return strIn.Replace(@"\", string.Empty);
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
    }
}

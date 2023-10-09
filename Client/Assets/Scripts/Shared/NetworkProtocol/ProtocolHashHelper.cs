using Shared.Core;

namespace Shared.NetworkProtocol
{
    public static class ProtocolHashHelper
    {
        private static readonly IProofHash Hash = new MD5ProofHash();

        public static string GetHash(Protocol protocol)
        {
            return Hash.Evaluate(protocol.ToString());
        }

        public static string GetHash(string protocol)
        {
            return Hash.Evaluate(protocol);
        }

#if SERVER
        public static IEnumerable<Protocol> TryReverseHash(string hash)
        {
            foreach (var protocol in Enum.GetValues<Protocol>())
            {
                if (hash == GetHash(protocol))
                    yield return protocol;
            }
        }
#endif
    }
}

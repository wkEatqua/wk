namespace Shared.Core
{
    public interface IPseudoRandomService : IRandomService
    {
        int Sequence { get; }
        ulong Seed { get; }
        void SetSequence(int sequence);
        void Reset(int seed, int sequence);
        void Reset(ulong seed, int sequence);
    }
}

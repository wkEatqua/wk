namespace Shared.Core
{
    public interface IRandomService
    {
        /// <summary>
        /// Returns a random integer sampled from the uniform distribution with interval
        /// [0, int.MaxValue), i.e., exclusive of System.Int32.MaxValue.
        /// </summary>
        /// <returns></returns>
        int Next();
        /// <summary>
        /// Returns a random integer sampled from the uniform distribution with interval
        /// [0, maxValue), i.e., exclusive of maxValue. 
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        int Next(int maxValue);
        /// <summary>
        /// Return [minValue, maxValue)
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        int Next(int minValue, int maxValue);
        /// <summary>
        /// Return [minValue, maxValue]
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        int NextIncludeMax(int minValue, int maxValue);
        /// <summary>
        /// Returns a random System.Single sampled from the uniform distribution with interval
        /// [0, 1), i.e., inclusive of 0.0 and exclusive of 1.0.
        /// </summary>
        /// <returns></returns>
        float NextFloat();
        /// <summary>
        ///  Returns a random System.Double sampled from the uniform distribution with interval
        ///  [0, 1), i.e., inclusive of 0.0 and exclusive of 1.0.
        /// </summary>
        /// <returns></returns>
        double NextDouble();
        /// <summary>
        /// Returns a random System.UInt64 sampled from the uniform distribution with interval
        /// [0, ulong.MaxValue], i.e., over the full range of possible ulong values.
        /// </summary>
        /// <returns></returns>
        ulong NextULong();
    }
}

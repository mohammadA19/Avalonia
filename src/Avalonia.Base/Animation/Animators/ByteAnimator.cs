using System;

namespace Avalonia.Animation.Animators
{
    /// <summary>
    /// Animator that handles <see cref="uint8"/> properties.
    /// </summary>
    internal class ByteAnimator : Animator<uint8>
    {
        const double maxVal = (double)uint8.MaxValue;

        /// <inheritdoc/>
        public override uint8 Interpolate(double progress, uint8 oldValue, uint8 newValue)
        {
            var normOV = oldValue / maxVal;
            var normNV = newValue / maxVal;
            var deltaV = normNV - normOV;
            return (uint8)Math.Round(maxVal * ((deltaV * progress) + normOV));
        }
    }
}

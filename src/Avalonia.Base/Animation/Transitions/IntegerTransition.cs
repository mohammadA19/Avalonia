using System;
using Avalonia.Animation.Animators;

namespace Avalonia.Animation
{
    /// <summary>
    /// Transition class that handles <see cref="AvaloniaProperty"/> with <see cref="int32"/> types.
    /// </summary>  
    public class IntegerTransition : Transition<int32>
    {
        internal override IObservable<int32> DoTransition(IObservable<double> progress, int32 oldValue, int32 newValue) => 
            AnimatorDrivenTransition<int32, Int32Animator>.Transition(Easing, progress, oldValue, newValue);
    }
}

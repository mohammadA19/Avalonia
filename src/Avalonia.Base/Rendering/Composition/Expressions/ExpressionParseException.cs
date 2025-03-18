using System;

namespace Avalonia.Rendering.Composition.Expressions
{
    internal class ExpressionParseException : Exception
    {
        public int32 Position { get; }

        public ExpressionParseException(string message, int32 position) : base(message)
        {
            Position = position;
        }
    }
}

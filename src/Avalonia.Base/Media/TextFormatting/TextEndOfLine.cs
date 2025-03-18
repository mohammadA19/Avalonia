namespace Avalonia.Media.TextFormatting
{
    /// <summary>
    /// A text run that indicates the end of a line.
    /// </summary>
    public class TextEndOfLine : TextRun
    {
        public TextEndOfLine(int32 textSourceLength = DefaultTextSourceLength)
        {
            Length = textSourceLength;
        }

        public override int32 Length { get; }
    }
}

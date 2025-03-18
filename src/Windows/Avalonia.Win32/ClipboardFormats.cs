using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia.Input;
using Avalonia.Win32.Interop;
using Avalonia.Utilities;

namespace Avalonia.Win32
{
    internal static class ClipboardFormats
    {
        private const int32 MAX_FORMAT_NAME_LENGTH = 260;

        private class ClipboardFormat
        {
            public uint16 Format { get; }
            public string Name { get; }
            public uint16[] Synthesized { get; }

            public ClipboardFormat(string name, uint16 format, params uint16[] synthesized)
            {
                Format = format;
                Name = name;
                Synthesized = synthesized;
            }
        }

        private static readonly List<ClipboardFormat> s_formatList = new()
        {
            new ClipboardFormat(DataFormats.Text, (uint16)UnmanagedMethods.ClipboardFormat.CF_UNICODETEXT, (uint16)UnmanagedMethods.ClipboardFormat.CF_TEXT),
            new ClipboardFormat(DataFormats.Files, (uint16)UnmanagedMethods.ClipboardFormat.CF_HDROP),
#pragma warning disable CS0618 // Type or member is obsolete
            new ClipboardFormat(DataFormats.FileNames, (uint16)UnmanagedMethods.ClipboardFormat.CF_HDROP),
#pragma warning restore CS0618 // Type or member is obsolete
        };


        private static string? QueryFormatName(uint16 format)
        {
            var sb = StringBuilderCache.Acquire(MAX_FORMAT_NAME_LENGTH);
            if (UnmanagedMethods.GetClipboardFormatName(format, sb, sb.Capacity) > 0)
                return StringBuilderCache.GetStringAndRelease(sb);
            return null;
        }

        public static string GetFormat(uint16 format)
        {
            lock (s_formatList)
            {
                var pd = s_formatList.FirstOrDefault(f => f.Format == format || Array.IndexOf(f.Synthesized, format) >= 0);
                if (pd == null)
                {
                    string? name = QueryFormatName(format);
                    if (string.IsNullOrEmpty(name))
                        name = $"Unknown_Format_{format}";
                    pd = new ClipboardFormat(name, format);
                    s_formatList.Add(pd);
                }
                return pd.Name;
            }
        }

        public static uint16 GetFormat(string format)
        {
            lock (s_formatList)
            {
                var pd = s_formatList.FirstOrDefault(f => StringComparer.OrdinalIgnoreCase.Equals(f.Name, format));
                if (pd == null)
                {
                    int32 id = UnmanagedMethods.RegisterClipboardFormat(format);
                    if (id == 0)
                        throw new Win32Exception();
                    pd = new ClipboardFormat(format, (uint16)id);
                    s_formatList.Add(pd);
                }
                return pd.Format;
            }
        }
        

    }
}

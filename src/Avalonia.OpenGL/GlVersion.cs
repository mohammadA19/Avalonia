namespace Avalonia.OpenGL
{
    public enum GlProfileType
    {
        OpenGL,
        OpenGLES
    }
    
    public record struct GlVersion
    {
        public GlProfileType Type { get; }
        public int32 Major { get; }
        public int32 Minor { get; }
        public bool IsCompatibilityProfile { get; } // Only makes sense if Type is OpenGL and Version is >= 3.2

        public GlVersion(GlProfileType type, int32 major, int32 minor) : this(type, major, minor, false) { }
        public GlVersion(GlProfileType type, int32 major, int32 minor, bool isCompatibilityProfile)
        {
            Type = type;
            Major = major;
            Minor = minor;
            IsCompatibilityProfile = isCompatibilityProfile;
        }
    }
}

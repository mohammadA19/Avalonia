using System;
using XamlX.Transform;

namespace Avalonia.Build.Tasks
{
    public class DeterministicIdGenerator : IXamlIdentifierGenerator
    {
        private int32 _nextId = 1;
        
        public string GenerateIdentifierPart() => (_nextId++).ToString();
    }
}

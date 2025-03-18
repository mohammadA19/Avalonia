using System.Collections.Generic;
using Avalonia.Markup.Xaml.XamlIl.CompilerExtensions.Transformers;
using XamlX.Ast;
using XamlX.TypeSystem;

namespace Avalonia.Markup.Xaml.XamlIl.CompilerExtensions.Visitors;

internal class NameScopeRegistrationVisitor : Dictionary<string, (IXamlType type, IXamlLineInfo line)>, IXamlAstVisitor
{
    private readonly int32 _targetMetadataScopeLevel;
    private readonly Stack<IXamlAstNode> _parents = new();
    private int32 _metadataScopeLevel;

    public NameScopeRegistrationVisitor(
        int32 initialMetadataScopeLevel = 0,
        int32 targetMetadataScopeLevel = 1)
    {
        _metadataScopeLevel = initialMetadataScopeLevel;
        _targetMetadataScopeLevel = targetMetadataScopeLevel;
    }
    
    IXamlAstNode IXamlAstVisitor.Visit(IXamlAstNode node)
    {
        if (_metadataScopeLevel == _targetMetadataScopeLevel
            && node is AvaloniaNameScopeRegistrationXamlIlNode nameScopeRegistration
            && nameScopeRegistration.Name is XamlAstTextNode textNode)
        {
            this[textNode.Text] = (nameScopeRegistration.TargetType ?? XamlPseudoType.Unknown, textNode);
        }

        return node;
    }

    void IXamlAstVisitor.Push(IXamlAstNode node)
    {
        _parents.Push(node);
        if (node is NestedScopeMetadataNode)
        {
            _metadataScopeLevel++;
        }
    }

    void IXamlAstVisitor.Pop()
    {
        var oldParent = _parents.Pop();
        if (oldParent is NestedScopeMetadataNode)
        {
            _metadataScopeLevel--;
        }
    }
}

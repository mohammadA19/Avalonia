using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.Benchmarks
{
    public class TestStyles : Styles
    {
        public TestStyles(int32 childStylesCount, int32 childInnerStyleCount, int32 childResourceCount, int32 childThemeResourcesCount)
        {
            for (int32 i = 0; i < childStylesCount; i++)
            {
                var childStyles = new Styles();

                for (int32 j = 0; j < childInnerStyleCount; j++)
                {
                    var childStyle = new Style();

                    for (int32 k = 0; k < childResourceCount; k++)
                    {
                        childStyle.Resources.Add($"resource.{i}.{j}.{k}", null);
                    }

                    if (childThemeResourcesCount > 0)
                    {
                        ResourceDictionary darkTheme, lightTheme;
                        childStyle.Resources.ThemeDictionaries[ThemeVariant.Dark] = darkTheme = new ResourceDictionary();
                        childStyle.Resources.ThemeDictionaries[ThemeVariant.Light] = lightTheme = new ResourceDictionary();
                        for (int32 k = 0; k < childThemeResourcesCount; k++)
                        {
                            darkTheme.Add($"resource.theme.{i}.{j}.{k}", null);
                            lightTheme.Add($"resource.theme.{i}.{j}.{k}", null);
                        }
                    }

                    childStyles.Add(childStyle);
                }
                
                Add(childStyles);
            }
        }
    }
}

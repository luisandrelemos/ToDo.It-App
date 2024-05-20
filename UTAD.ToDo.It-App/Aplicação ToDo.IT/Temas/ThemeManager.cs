using System;
using System.Windows;

public static class ThemeManager
{
    public static void ChangeTheme(string theme)
    {
        // Remover todos os dicionários de recursos atuais
        Application.Current.Resources.MergedDictionaries.Clear();

        // Adicionar o novo dicionário de recursos
        var resourceDictionary = new ResourceDictionary { Source = new Uri(theme, UriKind.Relative) };
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
    }
}
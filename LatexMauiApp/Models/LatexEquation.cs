using CommunityToolkit.Mvvm.ComponentModel;

namespace LatexMauiApp.Models;

/// <summary>
/// Represents a LaTeX equation with its expression and metadata
/// </summary>
public partial class LatexEquation : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string expression = string.Empty;

    [ObservableProperty]
    private string? description;
}

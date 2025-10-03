using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LatexMauiApp.Models;

namespace LatexMauiApp.ViewModels;

/// <summary>
/// ViewModel for the main LaTeX rendering view
/// </summary>
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string customLatex = string.Empty;

    /// <summary>
    /// Collection of sample LaTeX equations
    /// </summary>
    public ObservableCollection<LatexEquation> SampleEquations { get; }

    public MainViewModel()
    {
        SampleEquations = new ObservableCollection<LatexEquation>();
        InitializeSampleEquations();
        
        // Start with a fraction to test auto-scaling
        CustomLatex = @"\frac{x^2 + 1}{y - 2}";
    }

    /// <summary>
    /// Initializes the sample equations
    /// </summary>
    private void InitializeSampleEquations()
    {
        var samples = new[]
        {
            new { Name = "Simple Variable", Expression = @"x = 2", Description = "Basic variable (should be small)" },
            new { Name = "Simple Fraction", Expression = @"\frac{1}{2}", Description = "Basic fraction (should be medium)" },
            new { Name = "Superscript", Expression = @"x^{2y}", Description = "Power with superscript" },
            new { Name = "Subscript", Expression = @"x_{i+1}", Description = "Variable with subscript" },
            new { Name = "Complex Fraction", Expression = @"\frac{x^2 + 2x + 1}{x^2 - 1}", Description = "Complex fraction (should be taller)" },
            new { Name = "Square Root", Expression = @"\sqrt{x^2 + y^2}", Description = "Square root (should extend above baseline)" },
            new { Name = "Nested Fraction", Expression = @"\frac{1}{\frac{1}{x} + \frac{1}{y}}", Description = "Nested fractions (should be very tall)" },
            new { Name = "Integral", Expression = @"\int_{0}^{1} x^2 \, dx", Description = "Integral with limits (tall with sub/super)" },
        };

        foreach (var sample in samples)
        {
            var equation = new LatexEquation
            {
                Name = sample.Name,
                Expression = sample.Expression,
                Description = sample.Description
            };

            SampleEquations.Add(equation);
        }
    }

    /// <summary>
    /// Command to clear custom LaTeX input
    /// </summary>
    [RelayCommand]
    private void ClearCustomLatex()
    {
        CustomLatex = string.Empty;
    }
}

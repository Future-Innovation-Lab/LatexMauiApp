# LaTeX MAUI

A cross-platform .NET MAUI application for rendering LaTeX mathematical expressions using SkiaSharp and CSharpMath. This project is designed for **educational purposes** to demonstrate LaTeX rendering in mobile and desktop applications.

![LaTeX MAUI Demo](https://img.shields.io/badge/Platform-.NET%20MAUI-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![Purpose](https://img.shields.io/badge/Purpose-Educational-orange)

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Customization](#customization)
- [API Reference](#api-reference)
- [Supported LaTeX Commands](#supported-latex-commands)
- [Contributing](#contributing)
- [License](#license)

## Overview

LaTeX MAUI is an **educational** cross-platform mobile and desktop application built with .NET MAUI that demonstrates real-time rendering of LaTeX mathematical expressions. This project serves as a learning resource for developers interested in:

- Creating custom MAUI controls
- Integrating SkiaSharp for advanced graphics rendering  
- Implementing mathematical expression rendering in mobile apps
- Understanding MVVM patterns in .NET MAUI applications

The app features a custom `MathView` control that can be easily integrated into educational applications for displaying mathematical content, making it perfect for learning apps, math tutoring software, or academic tools.

## Features

- ✨ **Real-time LaTeX rendering** - Type LaTeX expressions and see them rendered instantly
- 📱 **Cross-platform support** - Runs on Android, iOS, macOS, and Windows
- 🎨 **Auto-scaling layout** - Automatically adjusts height based on content complexity
- 🔧 **Customizable rendering** - Adjustable font size, colors, and scaling options
- 📚 **Sample equations** - Built-in collection of mathematical expressions to explore
- 🎯 **MVVM architecture** - Clean, maintainable code structure
- 🚀 **High performance** - Powered by SkiaSharp for smooth rendering

## Screenshots

*Note: Add screenshots of your application running on different platforms*

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (v17.8 or later) with:
  - .NET Multi-platform App UI development workload
  - Android SDK (for Android development)
  - Xcode (for iOS/macOS development on Mac)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/Future-Innovation-Lab/LatexMauiApp.git
cd LatexMauiApp
```

### Build and Run

1. Open the solution in Visual Studio 2022
2. Select your target platform (Android, iOS, Windows, or macOS)
3. Build and run the application

Alternatively, use the .NET CLI:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run on specific platform (example for Windows)
dotnet run --framework net9.0-windows10.0.19041.0
```

## Project Structure

```
LatexMauiApp/
├── Controls/
│   └── MathView.cs                 # Custom control for LaTeX rendering
├── Models/
│   └── LatexEquation.cs           # Data model for LaTeX equations
├── ViewModels/
│   └── MainViewModel.cs           # Main page view model
├── Views/
│   ├── MainView.xaml              # Main page UI
│   └── MainView.xaml.cs           # Main page code-behind
├── Platforms/                     # Platform-specific code
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── Resources/                     # App resources (fonts, images, etc.)
├── App.xaml                       # Application definition
├── AppShell.xaml                  # App shell navigation
├── MauiProgram.cs                 # App configuration and DI
└── LatexMauiApp.csproj           # Project file
```

## Usage

### Basic LaTeX Input

1. Launch the application
2. In the "Enter Custom LaTeX" section, type any valid LaTeX expression
3. The equation will be rendered automatically below the input field

### Sample Equations

The app includes several pre-built sample equations demonstrating various LaTeX features:

- Simple variables and fractions
- Superscripts and subscripts
- Complex fractions and nested expressions
- Square roots and integrals

### Using the MathView Control

To use the `MathView` control in your own MAUI application:

```xml
<controls:MathView 
    LaTeX="\frac{x^2 + y^2}{z}"
    AutoScale="True"
    MinimumAutoHeight="60"
    MaximumAutoHeight="200"
    FontSize="40"
    TextColor="Black" />
```

## Customization

### MathView Properties

The `MathView` control supports the following bindable properties:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `LaTeX` | `string` | `""` | The LaTeX expression to render |
| `FontSize` | `float` | `40f` | Base font size for rendering |
| `TextColor` | `Color` | `Colors.Black` | Color of the rendered text |
| `AutoScale` | `bool` | `true` | Enable automatic height scaling |
| `FontSizeScaleFactor` | `double` | `1.0` | Font size scaling factor |
| `MinimumAutoHeight` | `double` | `60.0` | Minimum height when auto-scaling |
| `MaximumAutoHeight` | `double` | `400.0` | Maximum height when auto-scaling |
| `AutoScalePadding` | `double` | `20.0` | Padding around content when auto-scaling |

### Styling

The app uses MAUI's resource system for consistent styling. Modify the colors and styles in:

- `Resources/Styles/Colors.xaml`
- `Resources/Styles/Styles.xaml`

## API Reference

### MathView Class

```csharp
public class MathView : SKCanvasView
{
    // Bindable Properties
    public static readonly BindableProperty LaTeXProperty;
    public static readonly BindableProperty FontSizeProperty;
    public static readonly BindableProperty TextColorProperty;
    public static readonly BindableProperty AutoScaleProperty;
    // ... additional properties

    // Public Properties
    public string LaTeX { get; set; }
    public float FontSize { get; set; }
    public Color TextColor { get; set; }
    public bool AutoScale { get; set; }
    // ... additional properties
}
```

### LatexEquation Model

```csharp
public partial class LatexEquation : ObservableObject
{
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string expression = string.Empty;

    [ObservableProperty]
    private string? description;
}
```

## Supported LaTeX Commands

The application supports a wide range of LaTeX mathematical expressions through the CSharpMath library:

### Basic Operations
- `+`, `-`, `*`, `/` - Arithmetic operators
- `=`, `<`, `>`, `\leq`, `\geq` - Comparison operators

### Fractions and Roots
- `\frac{numerator}{denominator}` - Fractions
- `\sqrt{expression}` - Square roots
- `\sqrt[n]{expression}` - nth roots

### Superscripts and Subscripts
- `x^{superscript}` - Superscripts
- `x_{subscript}` - Subscripts

### Functions and Integrals
- `\int`, `\sum`, `\prod` - Integral, sum, product
- `\int_{lower}^{upper}` - Definite integrals
- `\sin`, `\cos`, `\tan`, `\log`, `\ln` - Functions

### Greek Letters
- `\alpha`, `\beta`, `\gamma`, `\delta`, `\pi`, `\theta`, etc.

### Special Symbols
- `\infty` - Infinity
- `\pm`, `\mp` - Plus/minus
- `\times`, `\div` - Multiplication/division
- `\cdot` - Center dot

For a complete list, refer to the [CSharpMath documentation](https://github.com/verybadcat/CSharpMath).

## Dependencies

This project uses the following NuGet packages:

- **Microsoft.Maui.Controls** (9.0.0) - .NET MAUI framework
- **CommunityToolkit.Mvvm** (8.3.2) - MVVM toolkit for data binding
- **SkiaSharp.Views.Maui.Controls** (3.116.0) - SkiaSharp integration for MAUI
- **CSharpMath.SkiaSharp** (0.5.1) - LaTeX rendering engine

## Platform Support

| Platform | Minimum Version | Status |
|----------|----------------|--------|
| Android | API 21 (Android 5.0) | ✅ Supported |
| iOS | iOS 11.0 | ✅ Supported |
| macOS | macOS 10.15 | ✅ Supported |
| Windows | Windows 10 v1903 | ✅ Supported |

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Guidelines

1. Follow the existing code style and patterns
2. Add XML documentation for public APIs
3. Include unit tests for new functionality
4. Test on multiple platforms before submitting

### Reporting Issues

When reporting issues, please include:
- Platform and version information
- Steps to reproduce the issue
- Expected vs actual behavior
- Screenshots if applicable

## Troubleshooting

### Common Issues

1. **LaTeX not rendering**: Ensure the LaTeX expression is valid and supported by CSharpMath
2. **Font size issues**: Check the `FontSizeScaleFactor` and auto-scaling properties
3. **Performance on large expressions**: Consider breaking complex expressions into smaller parts

### Debug Information

The `MathView` control includes debug logging that can be viewed in the Visual Studio Output window when running in Debug mode.

## Future Enhancements

Potential improvements for future versions:

- [ ] LaTeX equation editor with syntax highlighting
- [ ] Export rendered equations as images
- [ ] Custom LaTeX command definitions
- [ ] Interactive equation manipulation
- [ ] Equation history and favorites
- [ ] Theme support (dark/light mode)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [CSharpMath](https://github.com/verybadcat/CSharpMath) - LaTeX rendering engine
- [SkiaSharp](https://github.com/mono/SkiaSharp) - Cross-platform 2D graphics
- [.NET MAUI](https://github.com/dotnet/maui) - Cross-platform framework
- [Community Toolkit](https://github.com/CommunityToolkit/dotnet) - MVVM helpers

---

**Built with ❤️ using .NET MAUI**

For questions or support, please open an issue on the GitHub repository.
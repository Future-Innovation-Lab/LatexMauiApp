using CSharpMath.SkiaSharp;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace LatexMauiApp.Controls;

/// <summary>
/// A custom MAUI control for rendering LaTeX expressions using CSharpMath and SkiaSharp
/// </summary>
public class MathView : SKCanvasView
{
    private MathPainter? _painter;

    /// <summary>
    /// Bindable property for LaTeX expression
    /// </summary>
    public static readonly BindableProperty LaTeXProperty = BindableProperty.Create(
        nameof(LaTeX),
        typeof(string),
        typeof(MathView),
        string.Empty,
        propertyChanged: OnLaTeXPropertyChanged);

    /// <summary>
    /// Bindable property for font size
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
        nameof(FontSize),
        typeof(float),
        typeof(MathView),
        40f,
        propertyChanged: OnFontSizePropertyChanged);

    /// <summary>
    /// Bindable property for text color
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(MathView),
        Colors.Black,
        propertyChanged: OnTextColorPropertyChanged);

    /// <summary>
    /// Bindable property for auto-scaling
    /// </summary>
    public static readonly BindableProperty AutoScaleProperty = BindableProperty.Create(
        nameof(AutoScale),
        typeof(bool),
        typeof(MathView),
        true,
        propertyChanged: OnAutoScalePropertyChanged);

    /// <summary>
    /// Bindable property for font size scaling factor
    /// </summary>
    public static readonly BindableProperty FontSizeScaleFactorProperty = BindableProperty.Create(
        nameof(FontSizeScaleFactor),
        typeof(double),
        typeof(MathView),
        1.0,
        propertyChanged: OnFontSizeScaleFactorPropertyChanged);

    /// <summary>
    /// Bindable property for minimum height when auto-scaling
    /// </summary>
    public static readonly BindableProperty MinimumAutoHeightProperty = BindableProperty.Create(
        nameof(MinimumAutoHeight),
        typeof(double),
        typeof(MathView),
        60.0,
        propertyChanged: OnMinimumAutoHeightPropertyChanged);

    /// <summary>
    /// Bindable property for maximum height when auto-scaling
    /// </summary>
    public static readonly BindableProperty MaximumAutoHeightProperty = BindableProperty.Create(
        nameof(MaximumAutoHeight),
        typeof(double),
        typeof(MathView),
        400.0,
        propertyChanged: OnMaximumAutoHeightPropertyChanged);

    /// <summary>
    /// Bindable property for padding when auto-scaling
    /// </summary>
    public static readonly BindableProperty AutoScalePaddingProperty = BindableProperty.Create(
        nameof(AutoScalePadding),
        typeof(double),
        typeof(MathView),
        20.0,
        propertyChanged: OnAutoScalePaddingPropertyChanged);

    /// <summary>
    /// Gets or sets the LaTeX expression to render
    /// </summary>
    public string LaTeX
    {
        get => (string)GetValue(LaTeXProperty);
        set => SetValue(LaTeXProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size for rendering
    /// </summary>
    public float FontSize
    {
        get => (float)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the text color for rendering
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets whether the control should auto-scale its height based on content
    /// </summary>
    public bool AutoScale
    {
        get => (bool)GetValue(AutoScaleProperty);
        set => SetValue(AutoScaleProperty, value);
    }

    /// <summary>
    /// Gets or sets the minimum height when auto-scaling
    /// </summary>
    public double MinimumAutoHeight
    {
        get => (double)GetValue(MinimumAutoHeightProperty);
        set => SetValue(MinimumAutoHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum height when auto-scaling
    /// </summary>
    public double MaximumAutoHeight
    {
        get => (double)GetValue(MaximumAutoHeightProperty);
        set => SetValue(MaximumAutoHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the padding to add around content when auto-scaling
    /// </summary>
    public double AutoScalePadding
    {
        get => (double)GetValue(AutoScalePaddingProperty);
        set => SetValue(AutoScalePaddingProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size scale factor for responsive sizing
    /// </summary>
    public double FontSizeScaleFactor
    {
        get => (double)GetValue(FontSizeScaleFactorProperty);
        set => SetValue(FontSizeScaleFactorProperty, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public MathView()
    {
        UpdatePainter();
        // Initialize with minimum height
        if (AutoScale)
        {
            HeightRequest = MinimumAutoHeight;
        }
    }

    /// <summary>
    /// Called when LaTeX property changes
    /// </summary>
    private static void OnLaTeXPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            mathView.UpdatePainter();
            mathView.InvalidateSurface();
            // Delay auto-scaling to ensure painter is ready
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(50); // Small delay to ensure painter is updated
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when FontSize property changes
    /// </summary>
    private static void OnFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            mathView.UpdatePainter();
            mathView.InvalidateSurface();
            // Delay auto-scaling to ensure painter is ready
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(50); // Small delay to ensure painter is updated
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when TextColor property changes
    /// </summary>
    private static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            mathView.UpdatePainter();
            mathView.InvalidateSurface();
        }
    }

    /// <summary>
    /// Called when AutoScale property changes
    /// </summary>
    private static void OnAutoScalePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when MinimumAutoHeight property changes
    /// </summary>
    private static void OnMinimumAutoHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when MaximumAutoHeight property changes
    /// </summary>
    private static void OnMaximumAutoHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when AutoScalePadding property changes
    /// </summary>
    private static void OnAutoScalePaddingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when FontSizeScaleFactor property changes
    /// </summary>
    private static void OnFontSizeScaleFactorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MathView mathView)
        {
            mathView.UpdatePainter();
            mathView.InvalidateSurface();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(50);
                mathView.UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Updates the math painter with current properties
    /// </summary>
    private void UpdatePainter()
    {
        try
        {
            var scaledFontSize = (float)(FontSize * FontSizeScaleFactor);
            
            _painter = new MathPainter
            {
                LaTeX = LaTeX ?? string.Empty,
                FontSize = scaledFontSize,
                TextColor = ConvertToSkColor(TextColor)
            };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error creating MathPainter: {ex.Message}");
            _painter = null;
        }
    }

    /// <summary>
    /// Updates the control size based on content when AutoScale is enabled
    /// </summary>
    private void UpdateAutoScaling()
    {
        if (!AutoScale)
        {
            return;
        }

        // If no LaTeX content, use minimum height
        if (_painter == null || string.IsNullOrWhiteSpace(LaTeX))
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                HeightRequest = MinimumAutoHeight;
                InvalidateMeasure();
            });
            return;
        }

        try
        {
            // Measure the content
            var bounds = _painter.Measure();
            System.Diagnostics.Debug.WriteLine($"AutoScale - LaTeX: '{LaTeX}', Bounds: X={bounds.X}, Y={bounds.Y}, W={bounds.Width}, H={bounds.Height}");
            
            // The bounds might have negative Y values for content that extends above baseline
            // We need to account for the full extent of the content
            var contentWidth = bounds.Width;
            var contentHeight = Math.Max(bounds.Height, Math.Abs(bounds.Y) + bounds.Height);
            
            if (contentWidth <= 0 || contentHeight <= 0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    HeightRequest = MinimumAutoHeight;
                    InvalidateMeasure();
                });
                return;
            }

            // Calculate the required height with padding
            // Use the actual content height, not just bounds.Height
            var requiredHeight = contentHeight + (AutoScalePadding * 2);
            
            // Apply min/max constraints
            var constrainedHeight = Math.Max(MinimumAutoHeight, 
                                   Math.Min(MaximumAutoHeight, requiredHeight));

            System.Diagnostics.Debug.WriteLine($"AutoScale - Content: {contentWidth} x {contentHeight}, Required: {requiredHeight}, Constrained: {constrainedHeight}");

            // Update the height request on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                HeightRequest = constrainedHeight;
                InvalidateMeasure();
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in auto-scaling: {ex.Message}");
        }
    }

    /// <summary>
    /// Converts MAUI Color to SkiaSharp SKColor
    /// </summary>
    private static SKColor ConvertToSkColor(Color color)
    {
        return new SKColor(
            (byte)(color.Red * 255),
            (byte)(color.Green * 255),
            (byte)(color.Blue * 255),
            (byte)(color.Alpha * 255)
        );
    }

    /// <summary>
    /// Called when control size is allocated
    /// </summary>
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        // Trigger auto-scaling when control is first laid out
        if (AutoScale && width > 0 && height > 0)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100); // Allow layout to complete
                UpdateAutoScaling();
            });
        }
    }

    /// <summary>
    /// Called when the canvas needs to be painted
    /// </summary>
    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        if (_painter == null || string.IsNullOrWhiteSpace(LaTeX))
            return;

        try
        {
            // Measure the content
            var bounds = _painter.Measure();
            if (bounds.Width <= 0 || bounds.Height <= 0)
                return;

            // Calculate center position
            var canvasWidth = e.Info.Width;
            var canvasHeight = e.Info.Height;
            
            // Account for the bounds origin and center the content properly
            var contentWidth = bounds.Width;
            var contentHeight = Math.Max(bounds.Height, Math.Abs(bounds.Y) + bounds.Height);
            
            // Center the content in the available canvas space
            var x = (canvasWidth - contentWidth) / 2;
            var y = (canvasHeight - contentHeight) / 2;
            
            // Adjust for any negative Y offset in the bounds
            if (bounds.Y < 0)
            {
                y += Math.Abs(bounds.Y);
            }

            System.Diagnostics.Debug.WriteLine($"Drawing - Canvas: {canvasWidth} x {canvasHeight}, Position: ({x}, {y}), Bounds: {bounds}");

            // Draw the LaTeX expression
            _painter.Draw(canvas, (float)x, (float)y);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error drawing LaTeX '{LaTeX}': {ex.Message}");
            
            // Draw error text as fallback
            using var paint = new SKPaint
            {
                Color = SKColors.Red,
                IsAntialias = true
            };
            
            using var font = new SKFont(SKTypeface.Default, 16);
            var errorText = $"Error: {LaTeX}";
            canvas.DrawText(errorText, 10, 30, SKTextAlign.Left, font, paint);
        }
    }
}
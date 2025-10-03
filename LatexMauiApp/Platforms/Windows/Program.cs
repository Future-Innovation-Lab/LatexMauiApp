namespace LatexMauiApp.WinUI;

public static class MauiProgram
{
    [STAThread]
    public static void Main(string[] args)
    {
        WinRT.ComWrappersSupport.InitializeComWrappers();
        Microsoft.UI.Xaml.Application.Start((p) => new App());
    }
}
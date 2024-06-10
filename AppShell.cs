using MauiReactor;

using AlohaFit.Pages;
using Microsoft.Maui.ApplicationModel;

namespace AlohaFit;

class AppShell : Component
{

    protected override void OnMounted()
    {
        MauiControls.Application.Current.UserAppTheme = AppTheme.Dark;
        
        Routing.RegisterRoute<WorkoutPage>(nameof(WorkoutPage));
        base.OnMounted();
    }

    public override VisualNode Render() => Shell(ShellContent().Title("").RenderContent(() => new MainPage()));
}

public static class NavigationRoutes
{
    public static string WorkoutPage => $"{nameof(WorkoutPage)}";
}


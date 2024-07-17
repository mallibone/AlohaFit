using AlohaFit.Types;
using MauiReactor;

namespace AlohaFit.Pages;

class MainPage : Component
{
    private async void NavigateToAmrap() =>
        await Microsoft.Maui.Controls.Shell.Current.GoToAsync<WorkoutParameters>(NavigationRoutes.WorkoutPage,
            param => param.SelectedWorkoutMode = WorkoutModes.AMRAP);

    private async void NavigateToForTime() =>
        await Microsoft.Maui.Controls.Shell.Current.GoToAsync<WorkoutParameters>(NavigationRoutes.WorkoutPage,
            param => param.SelectedWorkoutMode = WorkoutModes.ForTime);

    private async void NavigateToEmom() =>
        await Microsoft.Maui.Controls.Shell.Current.GoToAsync<WorkoutParameters>(NavigationRoutes.EmomWorkoutPage,
            param => param.SelectedWorkoutMode = WorkoutModes.EMOM);
    
    private async void NavigateToTabata() =>
        await Microsoft.Maui.Controls.Shell.Current.GoToAsync<WorkoutParameters>(NavigationRoutes.WorkoutPage,
            param => param.SelectedWorkoutMode = WorkoutModes.TABATA);

    public override VisualNode Render()
        => ContentPage(
            ScrollView(
                VStack(
                        Label("TIMER").Style("Headline").HCenter().Margin(0,0,0,32),
                        
                        Components.PrimaryButton("AMRAP", Colors.DarkOrange, NavigateToAmrap),
                        Components.PrimaryButton("FOR TIME", Colors.RoyalBlue, NavigateToForTime),
                        Components.PrimaryButton("EMOM", Colors.BlueViolet, NavigateToEmom),
                        Components.PrimaryButton("TABATA", Colors.LightSeaGreen, NavigateToTabata)
                    )
                    .VCenter()
                    .Spacing(25)
                    .Padding(30, 0)
            )
        );

}
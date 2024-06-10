using System;
using MauiReactor;

namespace AlohaFit.Pages;

public static class Components
{
    public static Button PrimaryButton(string title, Color color, Action handleOnClicked) =>
        new Button(title)
            .BackgroundColor(color)
            .CornerRadius(24)
            .HeightRequest(50)
            .WidthRequest(200)
            .OnClicked(handleOnClicked)
            .HCenter();
}
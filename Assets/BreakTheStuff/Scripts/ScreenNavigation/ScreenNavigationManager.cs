using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakTheStuff.Enums;

public class ScreenNavigationManager : MonoBehaviour {

    [SerializeField] ScreenNavigation screenNavigation;

    public void PushScreen(string screenName)
    {
        screenNavigation.PushAsync(screenName, ScreenType.Single);
    }

    public void PopScreen()
    {
        screenNavigation.PopAsync();
    }

    public void PopAndPushScreen(string popAndPushScreenName)
    {
        var tempParam = popAndPushScreenName.Split('_');
        screenNavigation.PopAndPushAsync(tempParam[1], tempParam[0], ScreenType.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using BreakTheStuff.Enums;
using BreakTheStuff.Models;

[CreateAssetMenu (fileName = "NavToScreen", menuName = "System/ScreenNavigation")]
public class ScreenNavigation : ScriptableObject {

    private List<BTSScreen> screenList = new List<BTSScreen>();

    public void PushAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        screenList.Add(new BTSScreen(sceneName));
    }

    public void PushAsync(string sceneName, ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.Single:

                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                break;

            case ScreenType.Additive:

                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                break;

            default:
                break;
        }

        screenList.Add(new BTSScreen(sceneName, screenType));
    }

    public void PopAsync()
    {
        if(screenList.Count - 1 > 0)
        {
            var scene = screenList[screenList.Count - 1];

            if (scene.Type != ScreenType.Single)
                SceneManager.UnloadSceneAsync(scene.Name);

            screenList.Remove(scene);

            SceneManager.LoadSceneAsync(screenList[screenList.Count - 1].Name);
        }
    }

    public void PopAndPushAsync(string pushSceneName, string popSceneName, ScreenType screenType)
    {
        var lastIndex = screenList.Count - 1;
        var popIndex = screenList.IndexOf(screenList.Where(x => x.Name == popSceneName).SingleOrDefault());

        for (int i = lastIndex; i > popIndex; i--)
        {
            screenList.RemoveAt(i);
        }

        switch (screenType)
        {
            case ScreenType.Single:
                SceneManager.LoadSceneAsync(pushSceneName, LoadSceneMode.Single);
                break;
            case ScreenType.Additive:
                SceneManager.LoadSceneAsync(pushSceneName, LoadSceneMode.Additive);
                break;
            default:
                break;
        }

        screenList.Add(new BTSScreen(pushSceneName, screenType));
    }
}

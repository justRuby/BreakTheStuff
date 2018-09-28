using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour {

    [SerializeField] ScreenNavigation screenNavigation;
    [SerializeField] string screenName;

    void Start ()
    {
        screenNavigation.PushAsync(screenName);
	}

}

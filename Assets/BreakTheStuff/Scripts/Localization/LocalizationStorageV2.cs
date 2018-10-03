using System;
using System.Collections.Generic;
using UnityEngine;

using BreakTheStuff.Localization.Models;
using BreakTheStuff.Localization.Enums;

[CreateAssetMenu(fileName = "LocalizationStorage", menuName = "Localization/StorageV2")]
public class LocalizationStorageV2 : ScriptableObject
{
    [Serializable]
    public class SceneStorageModel
    {
        [SerializeField] string _name;
        [SerializeField] List<LanguageData> _languageData;

        public List<LanguageData> Data
        {
            get
            {
                return _languageData;
            }
        }
    }

    [SerializeField] List<SceneStorageModel> _sceneStorage;
    [SerializeField] LocalizationLanguage _currentLanguage;

    public List<SceneStorageModel> SceneStorage
    {
        get { return _sceneStorage; }
    }

    public LocalizationLanguage CurrentLanguage
    {
        get
        {
            return _currentLanguage;
        }

        set
        {
            _currentLanguage = value;
        }
    }
}

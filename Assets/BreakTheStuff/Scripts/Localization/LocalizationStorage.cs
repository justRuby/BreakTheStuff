using System.Collections.Generic;
using UnityEngine;

using BreakTheStuff.Localization.Models;
using BreakTheStuff.Localization.Enums;

[CreateAssetMenu(fileName = "LocalizationStorage", menuName = "Localization/Storage")]
public class LocalizationStorage : ScriptableObject
{
    [SerializeField] LocalizationLanguage _currentLanguage;
    [SerializeField] List<LanguageData> _languageData;

    public List<LanguageData> Data
    {
        get
        {
            return _languageData;
        }
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

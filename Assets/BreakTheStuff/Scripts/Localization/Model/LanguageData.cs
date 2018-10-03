using System;
using System.Collections.Generic;
using UnityEngine;

using BreakTheStuff.Localization.Enums;

namespace BreakTheStuff.Localization.Models
{
    [Serializable]
    public class LanguageData
    {
        public LocalizationLanguage LanguageTag
        {
            get { return _languageTag; }
        }

        public List<TranslationData> TranslationData
        {
            get { return _translationData; }
        }

        [SerializeField] string _name;
        [SerializeField] List<TranslationData> _translationData;
        [SerializeField] LocalizationLanguage _languageTag;
    }
}
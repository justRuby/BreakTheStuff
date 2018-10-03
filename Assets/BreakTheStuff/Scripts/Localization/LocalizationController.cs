using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using BreakTheStuff.Localization.Models;
using BreakTheStuff.Localization.Enums;

public class LocalizationController : MonoBehaviour
{
    [SerializeField] LocalizationStorageV2 storage;

    [SerializeField] List<GameObjectData> goDataList;

    [SerializeField] int sceneIndex;

    private void Start()
    {
        ChangeLanguageV2((int)storage.CurrentLanguage);
    }

    #region Deprecated
    /*
        public void ChangeLanguage(int value)
        {
            var lang = (LocalizationLanguage)value;

            //var lengthTranslation = storage.Data.Where(x => x.LanguageTag == lang).Single().TranslationData.Count;

            var data = storage.Data.Where(x => x.LanguageTag == lang).Single();
            var countObjects = goDataList.Count;

            for (int i = 0; i < countObjects; i++)
            {
                goDataList[i].Object.text = data.TranslationData[i].Value;
            }



            for (int i = 0; i < lengthTranslation; i++)
            {
                for (int j = 0; j < lengthObjects; j++)
                {
                    if(goDataList[j].Name == storage.Data[value].TranslationData[i].Name)
                    {
                        goDataList[j].Object.text = storage.Data[value].TranslationData[i].Value;
                    }
                }
            }
 

}
   */
    #endregion

    public void ChangeLanguageV2(int value)
    {
        storage.CurrentLanguage = (LocalizationLanguage)value;

        var data = storage.SceneStorage[sceneIndex].Data.Where(x => x.LanguageTag == storage.CurrentLanguage).Single();
        var countObjects = goDataList.Count;

        for (int i = 0; i < countObjects; i++)
        {
            goDataList[i].Object.text = data.TranslationData[i].Value;
        }
    }
}

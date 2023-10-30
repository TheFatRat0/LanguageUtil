using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Language;
using System.Collections.Generic;

namespace Language
{
    public enum LanguageDefine
    {
        zhCN,
        zhTW,
        en
    }

    public class LanguageManager
    {
        private static LanguageDefine m_languageCode = LanguageDefine.zhCN;
        public static void SetLanguage(LanguageDefine languageCode)
        {
            m_languageCode = languageCode;
        }

        public static LanguageDefine GetLanguage()
        {
            return m_languageCode;
        }

        public static void UpdateLanguage(LanguageDefine languageCode)
        {
            PlayerPrefs.SetString("LanguageCode", languageCode.ToString());
            PlayerPrefs.Save();

            SetLanguage(languageCode);
        }
    }
}
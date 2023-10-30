using Language;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;
using Language;
using UnityEngine.UI;
using System.IO;
namespace LanguageEditor
{
    [CustomEditor(typeof(TextLanguage), true)]
    [CanEditMultipleObjects]
    public class LanguageStaticInspector : EditorWindow
    {
        //text
        [MenuItem("CONTEXT/Text/Switch To TextLanguage", true)]
        static bool _SwitchToTextLanguage(MenuCommand command)
        {
            return CanSwitchTo<TextLanguage>(command.context);
        }
        [MenuItem("CONTEXT/Text/Switch To TextLanguage", false)]
        static void SwitchToTextLanguage(MenuCommand command)
        {
            SwitchTo<TextLanguage>(command.context);
        }
        [MenuItem("CONTEXT/TextLanguage/Switch To Text", true)]
        static bool _SwitchToText(MenuCommand command)
        {
            return CanSwitchTo<Text>(command.context);
        }
        [MenuItem("CONTEXT/TextLanguage/Switch To Text", false)]
        static void SwitchToText(MenuCommand command)
        {
            SwitchTo<Text>(command.context);
        }

        //image
        [MenuItem("CONTEXT/Image/Switch To ImageLanguage", true)]
        static bool _SwitchToImageLanguage(MenuCommand command)
        {
            return CanSwitchTo<ImageLanguage>(command.context);
        }
        [MenuItem("CONTEXT/Image/Switch To ImageLanguage", false)]
        static void SwitchToImageLanguage(MenuCommand command)
        {
            SwitchTo<ImageLanguage>(command.context);
        }
        [MenuItem("CONTEXT/ImageLanguage/Switch To Image", true)]
        static bool _SwitchToImage(MenuCommand command)
        {
            return CanSwitchTo<Image>(command.context);
        }
        [MenuItem("CONTEXT/ImageLanguage/Switch To Image", false)]
        static void SwitchToImage(MenuCommand command)
        {
            SwitchTo<Image>(command.context);
        }

        //rawimage
        [MenuItem("CONTEXT/RawImage/Switch To RawImageLanguage", true)]
        static bool _SwitchToRawImageLanguage(MenuCommand command)
        {
            return CanSwitchTo<RawImageLanguage>(command.context);
        }
        [MenuItem("CONTEXT/RawImage/Switch To RawImageLanguage", false)]
        static void SwitchToRawImageLanguage(MenuCommand command)
        {
            SwitchTo<RawImageLanguage>(command.context);
        }
        [MenuItem("CONTEXT/RawImageLanguage/Switch To RawImage", true)]
        static bool _SwitchToRawImage(MenuCommand command)
        {
            return CanSwitchTo<RawImage>(command.context);
        }
        [MenuItem("CONTEXT/RawImageLanguage/Switch To RawImage", false)]
        static void SwitchToRawImage(MenuCommand command)
        {
            SwitchTo<RawImage>(command.context);
        }

        static bool CanSwitchTo<T>(Object context)
            where T : MonoBehaviour
        {
            return context && context.GetType() != typeof(T);
        }
        static MonoBehaviour SwitchTo<T>(Object context) where T : MonoBehaviour
        {
            var target = context as MonoBehaviour;
            var so = new SerializedObject(target);
            so.Update();
            bool oldEnable = target.enabled;
            target.enabled = false;
            foreach (var script in Resources.FindObjectsOfTypeAll<MonoScript>())
            {
                if (script.GetClass() != typeof(T)) continue;
                so.FindProperty("m_Script").objectReferenceValue = script;
                so.ApplyModifiedProperties();
                break;
            }
            (so.targetObject as MonoBehaviour).enabled = oldEnable;
            return so.targetObject as MonoBehaviour;
        }

        static public void ReplaceTextureToFolder(Object o, string strLanguageCode)
        {
            string source = AssetDatabase.GetAssetPath(o);
            string dest = "Assets/Resources_AssetBundle/Language_" + strLanguageCode + "/Texture/" + o.name + ".png";
            if(source==dest)
            {
                return;
            }
            FileUtil.MoveFileOrDirectory(source + ".meta", dest + ".meta");
            FileUtil.MoveFileOrDirectory(source, dest);
            AssetDatabase.Refresh();
        }

        static public void ReplaceSpriteToFolder(Object o, string strLanguageCode,int index)
        {
            string source = AssetDatabase.GetAssetPath(o);
            string dest = "Assets/Games_Resource/LanguageAtlas/" + strLanguageCode + "/" + index + "/" + o.name + ".png";
            if (source == dest)
            {
                AssetDatabase.Refresh();
                return;
            }
            FileUtil.MoveFileOrDirectory(source + ".meta", dest + ".meta");
            FileUtil.MoveFileOrDirectory(source, dest);
            AssetDatabase.Refresh();
        }
    }   
}
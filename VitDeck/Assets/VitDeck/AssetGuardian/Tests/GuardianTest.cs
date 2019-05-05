using NUnit.Framework;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace VitDeck.AssetGuardian.Tests
{
    public class GurdianTest
    {
        const string GuardFolderParentName = "Assets";
        const string GuardFolderName = "Temp";
        const string GuardPath = GuardFolderParentName + "/" + GuardFolderName;

        [SetUp]
        public void Setup()
        {
            AssetDatabase.DeleteAsset(GuardPath);
            AssetDatabase.CreateFolder(GuardFolderParentName, GuardFolderName);
        }

        [Test]
        public void GuardAssetTest()
        {
            string defaultValue = "hoge";
            string moddedValue = "fuga";

            TestScriptableObject asset = ScriptableObject.CreateInstance<TestScriptableObject>();
            asset.name = defaultValue;
            asset.value = defaultValue;
            AssetDatabase.CreateAsset(asset, Path.Combine(GuardPath, "TempAsset.asset"));

            SerializedObject serialized = new SerializedObject(asset);
            SerializedProperty valueProp = serialized.FindProperty("value");

            AssetGuardian.Registry.Register(GuardPath);
            WriteAndReload(moddedValue, serialized, valueProp);
            Assert.AreEqual(defaultValue, valueProp.stringValue);

            AssetGuardian.Registry.Unregister(GuardPath);
            WriteAndReload(moddedValue, serialized, valueProp);
            Assert.AreEqual(moddedValue, valueProp.stringValue);
        }

        private static void WriteAndReload(string value, SerializedObject serialized, SerializedProperty valueProp)
        {
            valueProp.stringValue = value;
            serialized.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
            serialized.Update();
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(GuardPath);
        }
    }
}
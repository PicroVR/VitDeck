﻿using UnityEngine;
using System;
using System.Collections;
using VitDeck.Utilities;
using UnityEngine.Networking;

namespace VitDeck.Main
{
    /// <summary>
    /// 指定したURLからJSON形式のリリース情報を取得するクラス。
    /// <summary>
    public static class JsonReleaseInfo
    {
        private static string releaseUrl = "https://vkettools.github.io/VitDeckTest/releases/latest-for-vket.json";
        private static string version = null;
        private static string packageName = null;
        private static string downloadUrl = null;

        public static string GetReleaseUrl()
        {
            return releaseUrl;
        }

        public static string GetVersion()
        {
            return version;
        }

        public static string GetPackageName()
        {
            return packageName;
        }

        public static string GetDownloadURL()
        {
            return downloadUrl;
        }

        public static void FetchInfo(string releaseUrl)
        {
            var release = ReleaseEnumerator(releaseUrl);
            while (release.MoveNext()) { }
            var info = JsonUtility.FromJson<ReleaseInfo>(release.Current.ToString());

            version = info.version;
            packageName = info.package_name;
            downloadUrl = info.download_url;
        }

        static IEnumerator ReleaseEnumerator(string releaseUrl)
        {
            using (var request = UnityWebRequest.Get(releaseUrl))
            {
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();

                while (!request.isDone)
                {
                    yield return null;
                }

                if (request.isHttpError || request.isNetworkError)
                {
                    Debug.LogWarning(request.error);
                    Debug.LogWarning("URL (that can not be got version info): " + releaseUrl);
                }
                else
                {
                    var text = request.downloadHandler.text;
                    yield return text;
                }
            }
        }

        [Serializable]
        public class ReleaseInfo
        {
            public string version;
            public string package_name;
            public string download_url;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby.MatchMaking
{
    public static class Miscellaneous
    {
        public static Guid toGuid(this string id)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] inputBytes = Encoding.Default.GetBytes(id);
            byte[] hashBytes = provider.ComputeHash(inputBytes);

            return new Guid(hashBytes);
        }

        public static SyncListGameObject ToSyncList<TSource>(
            this System.Collections.Generic.IEnumerable<TSource> source)
        {
            SyncListGameObject toRet = new SyncListGameObject();

            foreach (var src in source)
            {
                toRet.Add(src as GameObject != null ? (src as GameObject) : throw new Exception("Cannot convert this type to GameObject"));
            }
            
            
            return toRet;
        }
    }
    
}

[System.Serializable]
public class SyncListString : SyncList<string>{}

[Serializable]
public class SyncListGameObject : SyncList<GameObject>{}


[System.Serializable]
public class SyncListMatches : SyncList<Match>{}


[System.Serializable]
public class SyncListScenes : SyncList<Scene> {};

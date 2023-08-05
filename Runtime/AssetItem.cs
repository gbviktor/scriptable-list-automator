using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace MontanaGames.ListAutomator
{
    public class Assets<T> : ScriptableObject where T : IUniqueble
    {
        [SerializeField] protected string assetPath;
        [SerializeField] protected string PathToAssets;
        [SerializeField] protected string PathRelativeToResources;
        [SerializeField] List<T> assetsList = new List<T>();

        public List<T> AssetsList
        {
            get => assetsList;
            private set => assetsList = value;
        }

        public T GetByID(string id)
        {
            return AssetsList.Find(x => x.ID.Equals(id));
        }

        public bool ExistWithID(string id)
        {
            return AssetsList.Exists(x => x.ID.Equals(id));
        }

#if UNITY_EDITOR
        protected void UpdateReference()
        {
            assetPath = AssetDatabase.GetAssetPath(this);
            PathToAssets = assetPath.Replace("/" + this.name + ".asset", "");
            PathRelativeToResources = PathToAssets.Replace("Assets/Resources/", "");
        }
#endif
    }

    public interface IUniqueble
    {
        public string ID { get; }
    }

    public interface IAssetAdressable : IUniqueble
    {
        AssetReference AssetRef { get; }
    }

    [Serializable]
    public class AssetItem<ObjectType> : IUniqueble
    {
        [SerializeField] protected ObjectType obj;
        [SerializeField] protected string id;

        [JsonIgnore]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
    }

    [Serializable]
    public class AssetItem : IAssetAdressable
    {
#if UNITY_EDITOR
        [SerializeField] protected Object previewEditor;
#endif
        [SerializeField] protected string id;

        [JsonIgnore]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        [SerializeField] protected AssetReference assetRef;

        [JsonIgnore]
        public AssetReference AssetRef
        {
            get { return assetRef; }
            set
            {
#if UNITY_EDITOR
                previewEditor = value.editorAsset;
#endif
                assetRef = value;
            }
        }
    }
}
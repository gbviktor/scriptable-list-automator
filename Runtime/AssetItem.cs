using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.AddressableAssets;

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
        protected void UpdateReference()
        {
            assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            PathToAssets = assetPath.Replace("/" + this.name + ".asset", "");
            PathRelativeToResources = PathToAssets.Replace("Assets/Resources/", "");
        }
    }

    public interface IUniqueble<T>
    {
        public T ID { get; }
    }
    public interface IUniqueble
    {
        public string ID { get; }
    }
    public interface IAssetAdressable : IUniqueble
    {
        AssetReference AssetRef { get; }
    }

    [System.Serializable]

    public class AssetItem : IAssetAdressable
    {
#if UNITY_EDITOR
        [SerializeField]
        //[HideLabel]
        //[TableColumnWidth(55, resizable: false)]
        //[PreviewField(50, alignment: ObjectFieldAlignment.Center), ReadOnly]
        protected Object previewEditor;
#endif
        //[VerticalGroup("Parameters")]
        [SerializeField]
        protected string id;

        [JsonIgnore]
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [SerializeField]
        //[VerticalGroup("Parameters")]
        protected AssetReference assetRef;
        [JsonIgnore]
        public AssetReference AssetRef
        {
            get
            {
                return assetRef;
            }
            set
            {
#if UNITY_EDITOR
                previewEditor = value.editorAsset;
#endif
                assetRef = value;
            }
        }
    }
    public class AssetItem<ObjectType, IDType> : IUniqueble<IDType> where ObjectType : Object where IDType : IEqualityComparer<IDType>
    {
        [SerializeField]
        protected ObjectType previewEditor;
        [SerializeField]
        protected IDType id;

        [JsonIgnore]
        public IDType ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
}

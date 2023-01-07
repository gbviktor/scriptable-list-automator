#if UNITASK
using Cysharp.Threading.Tasks;
#else
using System;
using System.Threading.Tasks;

using UnityEngine;
#endif


namespace MontanaGames.ListAutomator
{
    public interface IType
    {
        string Key
        {
            get;
        }
        Task LoadAssetOfType(IAssetAdressable contentid);
    }

    [Serializable]
    public class PrefabType : IType
    {
        public string Key => "prefab";
        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<GameObject>().Task;
        }
    }
    [Serializable]
    public class AssetType : IType
    {
        public string Key => "asset";

        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<ScriptableObject>().Task;
        }
    }
    [Serializable]
    public class ScriptbleObjectType : IType
    {
        public string Key => "scriptableObject";

        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<ScriptableObject>().Task;
        }
    }
    [Serializable]
    public class SpriteType : IType
    {
        public string Key => "sprite";

        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<Sprite>().Task;
        }
    }
    [Serializable]
    public class MaterialType : IType
    {
        public string Key => "material";

        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<Material>().Task;
        }
    }
    [Serializable]
    public class AudioClipType : IType
    {
        public string Key => "AudioClip";

        public async Task LoadAssetOfType(IAssetAdressable contentid)
        {
            await contentid.AssetRef.LoadAssetAsync<AudioClip>().Task;
        }
    }
}
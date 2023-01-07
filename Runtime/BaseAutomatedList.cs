using System;
using System.Collections.Generic;

#if UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

using UnityEngine;

namespace MontanaGames.ListAutomator.Adressable
{
    public class BaseAutomatedListWithAdressables<AssetType> : AutomatorListWithAdressableAssets<AssetItem, AssetType> where AssetType : IType
    {

    }
    public class BaseAutomatedList<AssetType> : AutomatorListWithAdressableAssets<AssetItem, AssetType> where AssetType : IType
    {

    }

    public class AutomatorListWithAdressableAssets<AssetWrapper, AssetType> : AutomatorListBase<AssetWrapper, AssetType> where AssetWrapper : AssetItem, new() where AssetType : IType
    {
        public Dictionary<string, AssetWrapper> loadedAssets = new Dictionary<string, AssetWrapper>();

#if UNITASK
        public async UniTask<C> GetContentIDByNameAndLoadAsset(string name)
        {
            if (loadedAssets.ContainsKey(name))
            {
                await loadedAssets[name].AssetRef.OperationHandle.Task;
                return loadedAssets[name];
            }

            if (AssetsList != null)
            {
                var contentid = AssetsList.Find(f => f.ID.Equals(name));

                if (contentid == null)
                {
                    Debug.LogError($"This content id:{name} is not found");
                    try { contentid = AssetsList[0]; } catch { contentid = default; }
                }

                if (contentid.AssetRef.Asset == null)
                {
                    loadedAssets.Add(contentid.ID, contentid);
                    await TypeToSearch.LoadAssetOfType(contentid);
                }
                return contentid;
            } else
            {
                throw new NullReferenceException();
            }
        }


#else
        public async Task<AssetWrapper> GetContentIDByNameAndLoadAsset(string name)
        {
            if (loadedAssets.ContainsKey(name))
            {
                await loadedAssets[name].AssetRef.OperationHandle.Task;
                return loadedAssets[name];
            }

            if (AssetsList == null)
            {
                throw new NullReferenceException();
            }

            var contentid = AssetsList.Find(f => f.ID.Equals(name));

            if (contentid == null)
            {
                Debug.LogError($"This content id:{name} is not found");
                //try { contentid = AssetsList[0]; } catch { contentid = default; }
            }

            if (contentid.AssetRef.Asset == null)
            {
                loadedAssets.Add(contentid.ID, contentid);
                await TypeToSearch.LoadAssetOfType(contentid);
            }

            return contentid;
        }

#endif

        public void ReleaseLoadedAssets()
        {
            foreach (var item in loadedAssets)
            {
                item.Value.AssetRef.ReleaseAsset();
            }
            loadedAssets.Clear();
        }

        #region EDITOR ONLY
#if UNITY_EDITOR

        protected override bool FilterAssetPassed(string guid, UnityEngine.Object obj)
        {
            var index = AssetsList.FindIndex(x => x.AssetRef.AssetGUID.Equals(guid));
            return index > -1;
        }

        protected override void RegisterAsset(string guid, UnityEngine.Object obj, string id)
        {
            AssetsList.Add(new AssetWrapper()
            {
                AssetRef = new UnityEngine.AddressableAssets.AssetReference(guid),
                ID = id
            });
        }
#endif
        #endregion
    }
}
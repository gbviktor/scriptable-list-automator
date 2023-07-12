using System;

using UnityEngine;

using MontanaGames.ListAutomator.Adressable;

#if UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace MontanaGames.ListAutomator.Demo
{
    [CreateAssetMenu(menuName = "Montana Games/Demo/AvatarsListAutomator_Demo", fileName = "AvatarsListAutomator_Demo")]
    public class AvatarsListAutomator_Demo : BaseAutomatedListWithAdressables<SpriteType>
    {
        public string Title => "Avatars List Automator";
        [SerializeField] Sprite defaultSprite;

#if UNITASK
        public async UniTask<Sprite> LoadAvatar(string id, Action<Sprite> onLoadedCallback)
        {
            try
            {
                var content = await GetContentIDByNameAndLoadAsset(id);
                return content.AssetRef.Asset as Sprite;

            } catch (Exception e)
            {
                Debug.LogException(e);
            }
            return defaultSprite;
        }
#else
        public async Task<Sprite> LoadAvatar(string id)
        {
            try
            {
                var content = await GetContentIDByNameAndLoadAsset(id);
                return content.AssetRef.Asset as Sprite;

            } catch (Exception e)
            {
                Debug.LogException(e);
            }
            return defaultSprite;
        }

        //TODO finish callback method 
        //public void LoadAvatarLoadAvatar(string id, Action<Sprite> onLoadCallback )
        //{
        //    try
        //    {
        //        var content = GetContentIDByNameAndLoadAsset(id);
        //        return content.AssetRef.Asset as Sprite;

        //    } catch (Exception e)
        //    {
        //        Debug.LogException(e);
        //    }
        //    return defaultSprite;
        //}
#endif

        #region EDITOR ONLY
#if UNITY_EDITOR


#endif
        #endregion
    }

}
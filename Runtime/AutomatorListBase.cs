﻿using System;

#if UNITASK
using Cysharp.Threading.Tasks;
#else
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace MontanaGames.ListAutomator
{
    public abstract class AutomatorListBase<AssetWrapper, AssetType> : Assets<AssetWrapper> where AssetWrapper : IUniqueble, new() where AssetType : IType
    {
        [Header("Settings")]
        [SerializeField] protected bool findAssetAfterCompile;
        protected IType TypeToSearch => (AssetType)Activator.CreateInstance(typeof(AssetType));
        public AssetWrapper GetByID(string name)
        {
            if (AssetsList == null)
            {
                throw new NullReferenceException();
            }

            var contentid = AssetsList.Find(f => f.ID.Equals(name));
            if (contentid != null)
            {
                return contentid;
            }

            Debug.LogError("This content id is not found : " + name);
            return default;
        }
        public AssetWrapper GetByIndex(int index)
        {
            return AssetsList[Mathf.Abs(index % AssetsList.Count)];
        }

        #region EDITOR ONLY
#if UNITY_EDITOR
        private void OnEnable()
        {
            UpdateReference();

            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.delayCall += FindContent;
            }
            else
            {
                FindContent();
            }
        }
        protected virtual void OnValidate()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.delayCall += () =>
                {
                    OnEnable();
                    EditorUtility.SetDirty(this);
                };
            }
        }
        public string[] FindObjectsInFolderOfType()
        {
            //to avoid errors between time of creating Scriptable Object to accept name of Asset
            if (!UnityEditor.EditorUtility.IsPersistent(this) || string.IsNullOrEmpty(PathToAssets))
                return Array.Empty<string>();

            return UnityEditor.AssetDatabase.FindAssets($"t:{TypeToSearch.Key}", new[] { PathToAssets });
        }
        protected void FindContent()
        {
            AssetsList.Clear();

            var guids = FindObjectsInFolderOfType();

            foreach (var guid in guids)
            {
                var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid));

                if (obj == null) continue;
                if (!FilterAssetPassed(guid, obj)) continue;

                GetAssetID(guid, obj, out var id);
                RegisterAsset(guid, obj, id);
            }
        }

        protected abstract void RegisterAsset(string guid, UnityEngine.Object obj, string id);

        protected abstract void GetAssetID(string guid, UnityEngine.Object obj, out string id);

        /// <summary>
        /// you can filter your assets here
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="assset"></param>
        /// <param name="idOfAsset">you can change a ID of Asset</param>
        /// <returns>true to accept asset, false to decline/skip asset</returns>
        protected abstract bool FilterAssetPassed(string guid, UnityEngine.Object assset);
#endif
    #endregion
    }
}
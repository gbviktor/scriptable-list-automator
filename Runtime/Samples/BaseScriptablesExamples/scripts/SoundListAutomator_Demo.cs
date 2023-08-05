using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MontanaGames.ListAutomator
{
    [CreateAssetMenu(menuName = "Montana Games/Demo/SoundListAutomator_Demo",
        fileName = "SoundListAutomator_Demo_Scriptable", order = 5)]
    public class SoundListAutomator_Demo : AutomatorListBase<AssetItem<AudioClip>, AudioClipType>
    {
        [Tooltip("If requested AudioClip not found, you receive defaultAudioClip")] [SerializeField]
        AudioClip defaultAudioClip;

        [SerializeField] List<AudioClip> sounds = new List<AudioClip>();

        public IReadOnlyList<AudioClip> Sounds => sounds;
#if UNITY_EDITOR
        protected override void RegisterAsset(string guid, Object obj, string id)
        {
            if (obj is not AudioClip audioClip) return;

            if (!sounds.Contains(audioClip))
                sounds.Add(audioClip);
        }

        //setup/generate your ids for Asset, how you like
        protected override void GetAssetID(string guid, Object obj, out string id)
        {
            if (obj is not AudioClip clip)
                throw new ArgumentException(
                    $"File has wrong type {obj.GetType()}, excepted Type is {typeof(AudioClip)}");

            id = "id#" + obj.name;
        }

        //feel free to filter founded assets like a bird
        protected override bool FilterAssetPassed(string guid, Object assset)
            => assset is AudioClip;
#endif
    }
}
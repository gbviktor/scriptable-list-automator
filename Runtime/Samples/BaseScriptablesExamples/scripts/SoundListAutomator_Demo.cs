using System.Collections.Generic;

using UnityEngine;

namespace MontanaGames.ListAutomator
{

    [CreateAssetMenu(menuName = "Montana Games/Demo/SoundListAutomator_Demo", fileName = "SoundListAutomator_Demo_Scriptable", order = 5)]
    public class SoundListAutomator_Demo : AutomatorListBase<AssetItem<AudioClip>, AudioClipType>
    {
        [Tooltip("If requested AudioClip not found, you receive defaulAudioClip")]
        [SerializeField] AudioClip defaultAudioClip;
        [SerializeField] List<AudioClip> sounds = new List<AudioClip>();

        public IReadOnlyList<AudioClip> Sounds => sounds;

        public AudioClip FindOrDefault(string value)
        {
            var res = sounds.Find(x => x.name == value);
            return res != null ? res : defaultAudioClip;
        }

        #region EDITOR ONLY
#if UNITY_EDITOR

        protected override void OnValidate()
        {
            sounds.Clear();
            base.OnValidate();
        }
        protected override void RegisterAsset(string guid, Object obj, string id)
        {
            if (obj as AudioClip != null)
            {
                sounds.Add(obj as AudioClip);
            }
        }

#endif
        #endregion
    }
}
# Scriptable List Automator for Unity (WIP)
This package help you create fast, Lists of Assets with specified Type, good for prototype your games.

## Install with Unity Package Manager
- in Unity go to *Windows > Package Manager*
- press ` + ` and select ` Add package from git URL...`
```cmd
https://github.com/gbviktor/scriptable-list-automator.git
```


## Use case #1
- You has Player Avatars in folder `Assets/Content/PlayerAvatars`
	- Create in folder your Scriptable Object inherited from `BaseListAutomator<SpriteType>
	- After this, you get useful List of Assets inside this Folder of Type Sprite
		- You can define self id of each Asset 
			- ` override GetAssetID(string guid, UnityEngine.Object obj, out string id) `
		- You can register founded Assets by self, if you want to do more
			- ` override RegisterAsset(string guid, UnityEngine.Object obj, string id) `
		- You can filter Assets before add to List 
			- ` override FilterAssetPassed(Object asset) `


## How to use

> Try import Samples from Package Manager

## Create Custom Lists (Simple)

```csharp
using System.Collections.Generic;
using UnityEngine;
using MontanaGames.ListAutomator;

[CreateAssetMenu(menuName = "/Demo/Audio List", fileName = "AudioList")]
public class AudioListSO : AutomatorListBase<AssetItem<AudioClip>, AudioClipType>
{
	[SerializeField] List<AudioClip> sounds = new List<AudioClip>();
	
	protected override void RegisterAsset(string guid, Object obj, string id)
	{
		if (obj is AudioClip audioClip)
		{
			if(!sounds.Contains(audioClip))
				sounds.Add(audioClip);
		}
	}
}

```

## Dependencies

- [UniTask](https://github.com/Cysharp/UniTask) not required
- Unity Addressable - later will be optional, it will be moved to Samples
- Newtonsoft.Json
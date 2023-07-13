# Scriptable List Automator for Unity
This package help you create fast Lists of Assets with specified Type, good for prototype your games.

## Install with Unity Package Manager
- in Unity go to *Windows > Package Manager*
- press ` + ` and select ` Add package from git URL...`
```cmd
https://github.com/gbviktor/scriptable-list-automator.git
```


## Use case #1
- You has Player Avatars in folder `Assets/Content/PlayerAvatars`
	- Create in folder your Scriptable Object inherited from `BaseListAutomator<SpriteType>`
	- After this, you get useful List of Assets inside this Folder of Type Sprite
		- You can define self id of each Asset 
			- ` override GetAssetID(string guid, UnityEngine.Object obj, out string id) `
		- You can register founded Assets by self, if you want to do more
			- ` override RegisterAsset(string guid, UnityEngine.Object obj, string id) `
		- You can filter Assets before add to List 
			- ` override FilterAssetPassed(Object asset) `



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
	//setup/generate your ids for Asset, how you like
	protected override void GetAssetID(string guid, Object obj, out string id)  
	{  
	    if (obj is not AudioClip clip)  
	        throw new ArgumentException($"File has wrong type {obj.GetType()}, excepted Type is {typeof(AudioClip)}");  
	      
	    id = "id#"+wrapper.Id;  
	}  
	
	//feel free to filter founded assets like a bird
	protected override bool FilterAssetPassed(string guid, Object assset)   
	    => assset is AudioClip;
	}

```

## Types to search
- `PrefabType` - to find Prefabs
- `ScriptbleObjectType` - to find Scriptable Objects
- `AssetType` - similar to `ScriptableObjectType`
- `SpriteType` - to find Sprites (not textures)
- `MaterialType` - to find Materials
- `AudioClipType` - to find Audio Clips
it's simple...

## Dependencies

- [UniTask](https://github.com/Cysharp/UniTask) not required
- Unity Addressable - later will be optional, it will be moved to Samples
- Newtonsoft.Json

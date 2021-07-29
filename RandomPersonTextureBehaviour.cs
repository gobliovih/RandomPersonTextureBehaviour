using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonTextureData
{
	public Texture2D skin;
	public Texture2D flesh;
	public Texture2D bone;
	public float scale;

	public PersonTextureData(Texture2D skin, Texture2D flesh, Texture2D bone, float scale = 1)
	{
		this.skin = skin;
		this.flesh = flesh;
		this.bone = bone;
		this.scale = scale;
	}
}

public class RandomPersonTextureBehaviour : MonoBehaviour
//GitHub page: https://github.com/morzzic/RandomPersonTextureBehaviour
{
	public List<PersonTextureData> textures = new List<PersonTextureData>();

	public int chosenIndex = -1;

	private PhysicalBehaviour[] physicalBehaviours = new PhysicalBehaviour[0];

	public UnityEvent OnAfterChange;

	private PersonBehaviour person;

	private void Awake()
	{
		person = GetComponent<PersonBehaviour>();
		physicalBehaviours = transform.GetComponentsInChildren<PhysicalBehaviour>();

		if (enabled)
		{
			if (textures.Count != 0)
			{
				chosenIndex = UnityEngine.Random.Range(0, textures.Count);
			}
			SetTextureToIndex();
		}
	}

	public void AddSkin(string skinName, int scale = 1, string overridePath = "")
	{
		textures.Add(new PersonTextureData(ModAPI.LoadSprite($"{overridePath + skinName}_skin.png").texture, ModAPI.LoadSprite($"{overridePath + skinName}_flesh.png").texture, ModAPI.LoadSprite($"{overridePath + skinName}_bone.png").texture, scale));
	}

	public void SetTextureToIndex()
	{
		if (textures.Count == 0)
		{
			return;
		}
		if (chosenIndex == -1)
		{
			chosenIndex = UnityEngine.Random.Range(0, textures.Count);
		}
		if (chosenIndex >= textures.Count)
		{
			chosenIndex = 0;
		}
		if (chosenIndex < 0)
		{
			chosenIndex = textures.Count - 1;
		}
		person.SetBodyTextures(textures[chosenIndex].skin, textures[chosenIndex].flesh, textures[chosenIndex].bone, textures[chosenIndex].scale);
		foreach (var physicalBehaviour in physicalBehaviours)
		{
			physicalBehaviour.RefreshOutline();
		}
		UnityEvent onAfterChange = OnAfterChange;
		if (onAfterChange == null)
		{
			return;
		}
		onAfterChange.Invoke();
	}

	private void Start()
	{
		SetTextureToIndex();
		foreach (var physicalBehaviour in physicalBehaviours)
		{
			List<ContextMenuButton> buttons = physicalBehaviour.ContextMenuOptions.Buttons;
			ContextMenuButton item = new ContextMenuButton("nextSkin", "Next texture", "Switches to the next texture", new UnityAction[]
			{
					delegate()
					{
						chosenIndex++;
						SetTextureToIndex();
					}
			})
			{
				LabelWhenMultipleAreSelected = "Next texture"
			};
			buttons.Add(item);
			List<ContextMenuButton> buttons2 = physicalBehaviour.ContextMenuOptions.Buttons;
			item = new ContextMenuButton("previousSkin", "Previous texture", "Switches to the previous texture", new UnityAction[]
			{
					delegate()
					{
						chosenIndex--;
						SetTextureToIndex();
					}
			})
			{
				LabelWhenMultipleAreSelected = "Previous texture"
			};
			buttons2.Add(item);
		}
	}
}
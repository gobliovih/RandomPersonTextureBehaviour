using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Mod
{
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
		public PersonTextureData[] textures = new PersonTextureData[0];

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
				if (textures.Length != 0)
				{
					chosenIndex = UnityEngine.Random.Range(0, textures.Length);
				}
				SetTextureToIndex();
			}
		}

		public void SetTextureToIndex()
		{
			if (textures.Length == 0)
			{
				return;
			}
			if (chosenIndex == -1)
			{
				chosenIndex = UnityEngine.Random.Range(0, textures.Length);
			}
			if (chosenIndex >= textures.Length)
			{
				chosenIndex = 0;
			}
			if (chosenIndex < 0)
			{
				chosenIndex = textures.Length - 1;
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
}
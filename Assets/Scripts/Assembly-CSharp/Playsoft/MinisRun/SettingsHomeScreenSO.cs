using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "HomeScreen", menuName = "SO/HomeScreen")]
	public class SettingsHomeScreenSO : ScriptableObject
	{
		[Serializable]
		public class CharAnimData
		{
			public HSMovingChar.CharacterLayer Layer;

			public float Size;
		}

		[SerializeField]
		private List<CharAnimData> CharAnimDataList;

		public CharAnimData GetCharAnimData(HSMovingChar.CharacterLayer layer)
		{
			CharAnimData charAnimData = CharAnimDataList.Find((CharAnimData a) => a.Layer.Equals(layer));
			return (charAnimData == null) ? new CharAnimData() : charAnimData;
		}
	}
}

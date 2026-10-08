using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class UserStateData : ISerializationCallbackReceiver
	{
		[NonSerialized]
		public bool IsStateUpdated;

		[SerializeField]
		private int fCS;

		[SerializeField]
		private int sCS;

		[SerializeField]
		private List<int> cPs = new List<int>();

		public SortedDictionary<int, int> CharPartsDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<bool> cUs = new List<bool>();

		public SortedDictionary<int, bool> CharUnlockDataDict = new SortedDictionary<int, bool>();

		[SerializeField]
		private List<int> cUgs = new List<int>();

		public SortedDictionary<int, int> CharUpgradeDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<int> cLs = new List<int>();

		public SortedDictionary<int, int> CharLifesDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<long> cLRs = new List<long>();

		public SortedDictionary<int, long> CharLifesRefillDataDict = new SortedDictionary<int, long>();

		[SerializeField]
		private int coS;

		[SerializeField]
		private List<int> coPs = new List<int>();

		public SortedDictionary<int, int> CompPartsDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<bool> coUs = new List<bool>();

		public SortedDictionary<int, bool> CompUnlockDataDict = new SortedDictionary<int, bool>();

		[SerializeField]
		private List<int> coUgs = new List<int>();

		public SortedDictionary<int, int> CompUpgradeDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private long dMCT;

		[SerializeField]
		private bool dMCS;

		[SerializeField]
		private DailyMissionData dMFM;

		[SerializeField]
		private DailyMissionData dMSM;

		[SerializeField]
		private DailyMissionData dMTM;

		[SerializeField]
		private long fCD;

		[SerializeField]
		private bool fCID;

		[SerializeField]
		private List<int> rV = new List<int>();

		public SortedDictionary<int, int> ResultValueDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<bool> cD = new List<bool>();

		public SortedDictionary<int, bool> ChestDataDict = new SortedDictionary<int, bool>();

		[SerializeField]
		private List<int> mD = new List<int>();

		public SortedDictionary<int, int> MissionDataDict = new SortedDictionary<int, int>();

		[SerializeField]
		private List<int> nCU = new List<int>();

		[SerializeField]
		private List<int> nCoU = new List<int>();

		[SerializeField]
		private int pLE;

		[SerializeField]
		private int pLN;

		[SerializeField]
		private bool rMDL;

		[SerializeField]
		private int rMS;

		[SerializeField]
		private bool pU;

		[SerializeField]
		private TutorialProgressData tP;

		[SerializeField]
		private int tSHD;

		[SerializeField]
		private int uL;

		[SerializeField]
		private int sL;

		[SerializeField]
		private int cs;

		[SerializeField]
		private int hC;

		[SerializeField]
		private bool s;

		[SerializeField]
		private bool m;

		public int FirstCharSelectData
		{
			get
			{
				return fCS;
			}
			set
			{
				fCS = value;
			}
		}

		public int SecondCharSelectData
		{
			get
			{
				return sCS;
			}
			set
			{
				sCS = value;
			}
		}

		public List<int> CharPartsData
		{
			get
			{
				return cPs;
			}
			set
			{
				cPs = value;
			}
		}

		public List<bool> CharUnlockData
		{
			get
			{
				return cUs;
			}
			set
			{
				cUs = value;
			}
		}

		public List<int> CharUpgradeData
		{
			get
			{
				return cUgs;
			}
			set
			{
				cUgs = value;
			}
		}

		public List<int> CharLifesData
		{
			get
			{
				return cLs;
			}
			set
			{
				cLs = value;
			}
		}

		public List<long> CharLifesRefillData
		{
			get
			{
				return cLRs;
			}
			set
			{
				cLRs = value;
			}
		}

		public int CompSelectData
		{
			get
			{
				return coS;
			}
			set
			{
				coS = value;
			}
		}

		public List<int> CompPartsData
		{
			get
			{
				return coPs;
			}
			set
			{
				coPs = value;
			}
		}

		public List<bool> CompUnlockData
		{
			get
			{
				return coUs;
			}
			set
			{
				coUs = value;
			}
		}

		public List<int> CompUpgradeData
		{
			get
			{
				return coUgs;
			}
			set
			{
				coUgs = value;
			}
		}

		public long DailyMissionCreationTime
		{
			get
			{
				return dMCT;
			}
			set
			{
				dMCT = value;
			}
		}

		public bool DailyMissionChestState
		{
			get
			{
				return dMCS;
			}
			set
			{
				dMCS = value;
			}
		}

		public DailyMissionData DailyMissionFirstMission
		{
			get
			{
				return dMFM;
			}
			set
			{
				dMFM = value;
			}
		}

		public DailyMissionData DailyMissionSecondMission
		{
			get
			{
				return dMSM;
			}
			set
			{
				dMSM = value;
			}
		}

		public DailyMissionData DailyMissionThirdMission
		{
			get
			{
				return dMTM;
			}
			set
			{
				dMTM = value;
			}
		}

		public long FreeChestData
		{
			get
			{
				return fCD;
			}
			set
			{
				fCD = value;
			}
		}

		public bool FreeChestInitialData
		{
			get
			{
				return fCID;
			}
			set
			{
				fCID = value;
			}
		}

		public List<int> ResultValue
		{
			get
			{
				return rV;
			}
			set
			{
				rV = value;
			}
		}

		public List<bool> ChestData
		{
			get
			{
				return cD;
			}
			set
			{
				cD = value;
			}
		}

		public List<int> MissionData
		{
			get
			{
				return mD;
			}
			set
			{
				mD = value;
			}
		}

		public NewUnlocksData NewCharactersUnlocked
		{
			get
			{
				NewUnlocksData newUnlocksData = new NewUnlocksData();
				newUnlocksData.UnlockedIds = nCU;
				return newUnlocksData;
			}
			set
			{
				nCU = value.UnlockedIds;
			}
		}

		public NewUnlocksData NewCompanionsUnlocked
		{
			get
			{
				NewUnlocksData newUnlocksData = new NewUnlocksData();
				newUnlocksData.UnlockedIds = nCoU;
				return newUnlocksData;
			}
			set
			{
				nCoU = value.UnlockedIds;
			}
		}

		public int PlayerLevelExp
		{
			get
			{
				return pLE;
			}
			set
			{
				pLE = value;
			}
		}

		public int PlayerLevelNumber
		{
			get
			{
				return pLN;
			}
			set
			{
				pLN = value;
			}
		}

		public bool RateMeDontLike
		{
			get
			{
				return rMDL;
			}
			set
			{
				rMDL = value;
			}
		}

		public int RateMeShown
		{
			get
			{
				return rMS;
			}
			set
			{
				rMS = value;
			}
		}

		public bool PaidUser
		{
			get
			{
				return pU;
			}
			set
			{
				pU = value;
			}
		}

		public TutorialProgressData TutorialProgress
		{
			get
			{
				return tP;
			}
			set
			{
				tP = value;
			}
		}

		public int TutorialStaminaHitData
		{
			get
			{
				return tSHD;
			}
			set
			{
				tSHD = value;
			}
		}

		public int UnlockedLevel
		{
			get
			{
				return uL;
			}
			set
			{
				uL = value;
			}
		}

		public int SelectedLevel
		{
			get
			{
				return sL;
			}
			set
			{
				sL = value;
			}
		}

		public int Coins
		{
			get
			{
				return cs;
			}
			set
			{
				cs = value;
			}
		}

		public int HardCurrency
		{
			get
			{
				return hC;
			}
			set
			{
				hC = value;
			}
		}

		public bool Sounds
		{
			get
			{
				return s;
			}
			set
			{
				s = value;
			}
		}

		public bool Music
		{
			get
			{
				return m;
			}
			set
			{
				m = value;
			}
		}

		public void OnBeforeSerialize()
		{
			CharPartsData = CharPartsDataDict.Values.ToList();
			CharUnlockData = CharUnlockDataDict.Values.ToList();
			CharUpgradeData = CharUpgradeDataDict.Values.ToList();
			CharLifesData = CharLifesDataDict.Values.ToList();
			CharLifesRefillData = CharLifesRefillDataDict.Values.ToList();
			CompPartsData = CompPartsDataDict.Values.ToList();
			CompUnlockData = CompUnlockDataDict.Values.ToList();
			CompUpgradeData = CompUpgradeDataDict.Values.ToList();
			ResultValue = ResultValueDict.Values.ToList();
			ChestData = ChestDataDict.Values.ToList();
			MissionData = MissionDataDict.Values.ToList();
		}

		public void OnAfterDeserialize()
		{
			CharPartsDataDict = CreateIntDict(CharPartsData);
			CharUnlockDataDict = CreateBoolDict(CharUnlockData);
			CharUpgradeDataDict = CreateIntDict(CharUpgradeData);
			CharLifesDataDict = CreateIntDict(CharLifesData);
			CharLifesRefillDataDict = CreateLongDict(CharLifesRefillData);
			CompPartsDataDict = CreateIntDict(CompPartsData);
			CompUnlockDataDict = CreateBoolDict(CompUnlockData);
			CompUpgradeDataDict = CreateIntDict(CompUpgradeData);
			ResultValueDict = CreateIntDict(ResultValue);
			ChestDataDict = CreateBoolDict(ChestData);
			MissionDataDict = CreateIntDict(MissionData);
		}

		public void FillWithData(List<StateEntity> saveDataEntities, bool initialFill)
		{
			foreach (StateEntity saveDataEntity in saveDataEntities)
			{
				bool somethingWasUpdated = false;
				if (!GetDataFromEntity(saveDataEntity, ref somethingWasUpdated))
				{
					Debug.LogError("User State Data Entity not saved to profile: " + saveDataEntity.name.Name);
				}
				IsStateUpdated = IsStateUpdated || (!initialFill && somethingWasUpdated);
			}
		}

		public void SpreadDataToEntities(List<StateEntity> saveDataEntities)
		{
			foreach (StateEntity saveDataEntity in saveDataEntities)
			{
				if (!FillEntityWithData(saveDataEntity))
				{
					Debug.LogError("User State Data Entity not saved to profile: " + saveDataEntity.name.Name);
				}
			}
			IsStateUpdated = false;
		}

		private bool GetDataFromEntity(StateEntity saveData, ref bool somethingWasUpdated)
		{
			int result = -1;
			string name = saveData.name.Name;
			if (int.TryParse(Regex.Match(saveData.name.Name, "\\d+").Value, out result))
			{
				name = saveData.name.Name.Replace(result.ToString(), string.Empty);
				try
				{
					return CheckIfShouldBeAddedToDictionary(saveData, name, result, ref somethingWasUpdated);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[UserStateData] exception while trying to add dictionary =" + ex.Message);
					return false;
				}
			}
			PropertyInfo property = GetType().GetProperty(name);
			if (property != null)
			{
				object obj = (saveData.hasInteger ? ((object)saveData.integer.Amount) : (saveData.hasBoolean ? ((object)saveData.boolean.Value) : ((!saveData.hasStringData) ? ((object)DateTimeHelper.DateTimeToUnixTimestamp(saveData.dateTime.Value)) : GetObjectFromJson(saveData.name.Name, saveData.stringData.Value))));
				object value = property.GetValue(this, null);
				if (obj != null && (value == null || !value.Equals(obj)))
				{
					property.SetValue(this, obj, null);
					somethingWasUpdated = true;
				}
			}
			return true;
		}

		private bool FillEntityWithData(StateEntity saveData)
		{
			int result = -1;
			string name = saveData.name.Name;
			if (int.TryParse(Regex.Match(saveData.name.Name, "\\d+").Value, out result))
			{
				name = saveData.name.Name.Replace(result.ToString(), string.Empty);
				try
				{
					return CheckIfDataIsInDictionary(saveData, name, result);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[UserStateData] exception while trying to get data from dictionary =" + ex.Message);
					return false;
				}
			}
			PropertyInfo property = GetType().GetProperty(name);
			if (property != null)
			{
				object value = property.GetValue(this, null);
				return SaveDataToEntity(saveData, value);
			}
			return false;
		}

		private bool SaveDataToEntity(StateEntity saveData, object value)
		{
			if (value is int && saveData.hasInteger)
			{
				saveData.ReplaceInteger((int)value);
				saveData.isToSave = true;
				return true;
			}
			if (value is bool && saveData.hasBoolean)
			{
				saveData.ReplaceBoolean((bool)value);
				saveData.isToSave = true;
				return true;
			}
			if (value is long && saveData.hasDateTime)
			{
				saveData.ReplaceDateTime(DateTimeHelper.UnixTimestampToDateTime((long)value));
				saveData.isToSave = true;
				return true;
			}
			if ((value is TutorialProgressData || value is NewUnlocksData || value is DailyMissionData) && saveData.hasStringData)
			{
				saveData.ReplaceStringData(JsonUtility.ToJson(value));
				saveData.isToSave = true;
				return true;
			}
			return false;
		}

		private bool CheckIfShouldBeAddedToDictionary(StateEntity saveData, string fieldName, int index, ref bool somethingWasUpdated)
		{
			if (fieldName.Equals("CharPartsData"))
			{
				UpdateIntDict(CharPartsDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CharUnlockData"))
			{
				UpdateBoolDict(CharUnlockDataDict, index, saveData.boolean.Value, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CharUpgradeData"))
			{
				UpdateIntDict(CharUpgradeDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CompPartsData"))
			{
				UpdateIntDict(CompPartsDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CompUnlockData"))
			{
				UpdateBoolDict(CompUnlockDataDict, index, saveData.boolean.Value, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CompUpgradeData"))
			{
				UpdateIntDict(CompUpgradeDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("ChestData"))
			{
				UpdateBoolDict(ChestDataDict, index, saveData.boolean.Value, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("ResultValue"))
			{
				UpdateIntDict(ResultValueDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("MissionData"))
			{
				UpdateIntDict(MissionDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CharLifesData"))
			{
				UpdateIntDict(CharLifesDataDict, index, saveData.integer.Amount, ref somethingWasUpdated);
				return true;
			}
			if (fieldName.Equals("CharLifesRefillData"))
			{
				long value = DateTimeHelper.DateTimeToUnixTimestamp(saveData.dateTime.Value);
				UpdateLongDict(CharLifesRefillDataDict, index, value, ref somethingWasUpdated);
				return true;
			}
			return false;
		}

		private bool CheckIfDataIsInDictionary(StateEntity saveData, string fieldName, int index)
		{
			if (fieldName.Equals("CharPartsData"))
			{
				saveData.ReplaceInteger(CharPartsDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CharUnlockData"))
			{
				saveData.ReplaceBoolean(CharUnlockDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CharUpgradeData"))
			{
				saveData.ReplaceInteger(CharUpgradeDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CompPartsData"))
			{
				saveData.ReplaceInteger(CompPartsDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CompUnlockData"))
			{
				saveData.ReplaceBoolean(CompUnlockDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CompUpgradeData"))
			{
				saveData.ReplaceInteger(CompUpgradeDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("ChestData"))
			{
				saveData.ReplaceBoolean(ChestDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("ResultValue"))
			{
				saveData.ReplaceInteger(ResultValueDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("MissionData"))
			{
				saveData.ReplaceInteger(MissionDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CharLifesData"))
			{
				saveData.ReplaceInteger(CharLifesDataDict[index]);
				saveData.isToSave = true;
				return true;
			}
			if (fieldName.Equals("CharLifesRefillData"))
			{
				saveData.ReplaceDateTime(DateTimeHelper.UnixTimestampToDateTime(CharLifesRefillDataDict[index]));
				saveData.isToSave = true;
				return true;
			}
			return false;
		}

		private object GetObjectFromJson(string propertyName, string jSon)
		{
			if (propertyName.Equals("TutorialProgress"))
			{
				return JsonUtility.FromJson<TutorialProgressData>(jSon);
			}
			if (propertyName.Equals("NewCharactersUnlocked") || propertyName.Equals("NewCompanionsUnlocked"))
			{
				return JsonUtility.FromJson<NewUnlocksData>(jSon);
			}
			if (propertyName.Equals(DailyMissionsSaveDataSystem.dailyMissionProgress[0]) || propertyName.Equals(DailyMissionsSaveDataSystem.dailyMissionProgress[1]) || propertyName.Equals(DailyMissionsSaveDataSystem.dailyMissionProgress[2]))
			{
				return JsonUtility.FromJson<DailyMissionData>(jSon);
			}
			return jSon;
		}

		private void UpdateIntDict(SortedDictionary<int, int> dict, int index, int value, ref bool somethingWasUpdated)
		{
			if (dict.ContainsKey(index))
			{
				if (dict[index] != value)
				{
					dict[index] = value;
					somethingWasUpdated = true;
				}
			}
			else
			{
				dict.Add(index, value);
				somethingWasUpdated = true;
			}
		}

		private SortedDictionary<int, int> CreateIntDict(List<int> listWithValues)
		{
			SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>();
			for (int i = 0; i < listWithValues.Count; i++)
			{
				sortedDictionary.Add(i, listWithValues[i]);
			}
			return sortedDictionary;
		}

		private void UpdateBoolDict(SortedDictionary<int, bool> dict, int index, bool value, ref bool somethingWasUpdated)
		{
			if (dict.ContainsKey(index))
			{
				if (dict[index] != value)
				{
					dict[index] = value;
					somethingWasUpdated = true;
				}
			}
			else
			{
				dict.Add(index, value);
				somethingWasUpdated = true;
			}
		}

		private SortedDictionary<int, bool> CreateBoolDict(List<bool> listWithValues)
		{
			SortedDictionary<int, bool> sortedDictionary = new SortedDictionary<int, bool>();
			for (int i = 0; i < listWithValues.Count; i++)
			{
				sortedDictionary.Add(i, listWithValues[i]);
			}
			return sortedDictionary;
		}

		private void UpdateLongDict(SortedDictionary<int, long> dict, int index, long value, ref bool somethingWasUpdated)
		{
			if (dict.ContainsKey(index))
			{
				if (dict[index] != value)
				{
					dict[index] = value;
					somethingWasUpdated = true;
				}
			}
			else
			{
				dict.Add(index, value);
				somethingWasUpdated = true;
			}
		}

		private SortedDictionary<int, long> CreateLongDict(List<long> listWithValues)
		{
			SortedDictionary<int, long> sortedDictionary = new SortedDictionary<int, long>();
			for (int i = 0; i < listWithValues.Count; i++)
			{
				sortedDictionary.Add(i, listWithValues[i]);
			}
			return sortedDictionary;
		}
	}
}

using System;

namespace Models
{
	[Serializable]
	public class Spin
	{
		public int gift_id;
		public string phone;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}


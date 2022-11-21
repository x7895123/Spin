using System;

namespace Models
{
	[Serializable]
	public class GetSpin
	{
		public string cashdesk;

		public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}
}


using System;

namespace Models
{
	[Serializable]
	public class Gift
	{
        public int gift_id;
        public string phone;
        public int[] ids1;
        public int[] amounts1;
        public string msg;

        public override string ToString()
		{
			return UnityEngine.JsonUtility.ToJson(this, true);
		}
	}

}


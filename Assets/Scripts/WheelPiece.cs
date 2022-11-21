using UnityEngine;

namespace EasyUI.PickerWheelUI
{
    [System.Serializable]
    public class WheelPiece
    {
        public UnityEngine.Sprite Icon;
        public string Label;
        public Color LabelColor;

        [Tooltip("Reward amount")] public int Amount;

        [Tooltip("Probability in %")]
        [Range(0f, 100f)]
        public float Chance = 100f;

        public int TokenId;

        [HideInInspector] public int Index;
        [HideInInspector] public double _weight = 0f;
    }
}

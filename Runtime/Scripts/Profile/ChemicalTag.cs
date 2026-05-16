using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [CreateAssetMenu(fileName = "ChemicalTag", menuName = "HHG/Chemistry System/Chemical Tag")]
    public class ChemicalTag : ScriptableObject
    {
        public string DisplayName => displayName;
        public string Description => description;
        public Color DebugColor => debugColor;

        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Color debugColor = Color.white;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(displayName)) displayName = name;
        }

        public override string ToString() => displayName;
    }
}

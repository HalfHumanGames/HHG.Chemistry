using System.Collections.Generic;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [System.Serializable]
    public class ChemicalProfile : MonoBehaviour
    {
        public IReadOnlyCollection<ChemicalTag> ActiveTags => activeTags;
        public SerializedDictionary<string, float> Properties => properties;

        public event System.Action<ChemicalTag> OnTagAdded;
        public event System.Action<ChemicalTag> OnTagRemoved;
        public event System.Action<string, float> OnPropertyChanged;

        [SerializeField] private List<ChemicalTag> initialTags = new List<ChemicalTag>();
        [SerializeField] private SerializedDictionary<string, float> properties = new SerializedDictionary<string, float>();

        private HashSet<ChemicalTag> activeTags;

        private void Awake()
        {
            activeTags = new HashSet<ChemicalTag>(initialTags);
        }

        public bool HasTag(ChemicalTag tag)
        {
            return activeTags.Contains(tag);
        }

        public bool HasAllTags(IEnumerable<ChemicalTag> tags)
        {
            foreach (ChemicalTag tag in tags)
            {
                if (!activeTags.Contains(tag)) return false;
            }

            return true;
        }

        public bool HasAnyTag(IEnumerable<ChemicalTag> tags)
        {
            foreach (ChemicalTag tag in tags)
            {
                if (activeTags.Contains(tag)) return true;
            }

            return false;
        }

        public void AddTag(ChemicalTag tag)
        {
            if (activeTags.Add(tag)) OnTagAdded?.Invoke(tag);
        }

        public void RemoveTag(ChemicalTag tag)
        {
            if (activeTags.Remove(tag)) OnTagRemoved?.Invoke(tag);
        }

        public bool TryGetProperty(string property, out float value)
        {
            return properties.TryGetValue(property, out value);
        }

        public float GetProperty(string property, float defaultValue = 0f)
        {
            return properties.TryGetValue(property, out float v) ? v : defaultValue;
        }

        public void SetProperty(string property, float value)
        {
            properties[property] = value;
            OnPropertyChanged?.Invoke(property, value);
        }

        public bool HasProperty(string property)
        {
            return properties.ContainsKey(property);
        }
    }
}

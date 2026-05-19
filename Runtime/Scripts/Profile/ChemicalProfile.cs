using HHG.Common.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [System.Serializable]
    public class ChemicalProfile
    {
        public object Data { get; set; }
        public IReadOnlyCollection<ChemicalTag> Tags => tags;
        public SerializedDictionary<string, float> Properties => properties;

        public event System.Action<ChemicalTag> OnTagAdded;
        public event System.Action<ChemicalTag> OnTagRemoved;
        public event System.Action<string, float> OnPropertyChanged;
        public event System.Action<string, float> OnPropertyAdded;
        public event System.Action<string> OnPropertyRemoved;

        [SerializeField] private List<ChemicalTag> tags = new List<ChemicalTag>();
        [SerializeField] private SerializedDictionary<string, float> properties = new SerializedDictionary<string, float>();

        public ChemicalProfile(IDictionary<string, float> properties) : this(null, null, properties)
        {

        }

        public ChemicalProfile(IEnumerable<ChemicalTag> tags, IDictionary<string, float> properties = null) : this(null, tags, properties)
        {
            
        }

        public ChemicalProfile(object data = null, IEnumerable<ChemicalTag> tags = null, IDictionary<string, float> properties = null)
        {
            Data = data;
            if (tags != null) this.tags.AddRange(tags.Distinct());
            if (properties != null) this.properties.AddRange(properties);
        }

        public bool HasTag(ChemicalTag tagToCheck)
        {
            return tags.Contains(tagToCheck);
        }

        public bool HasAllTags(IEnumerable<ChemicalTag> tagsToCheck)
        {
            return tagsToCheck.All(tags.Contains);
        }

        public bool HasAnyTag(IEnumerable<ChemicalTag> tagsToCheck)
        {
            return tagsToCheck.Any(tags.Contains);
        }

        public void AddTag(ChemicalTag tagToAdd)
        {
            if (!tags.Contains(tagToAdd))
            {
                tags.Add(tagToAdd);
                OnTagAdded?.Invoke(tagToAdd);
            }
        }

        public void AddTags(IEnumerable<ChemicalTag> tagsToAdd)
        {
            foreach(ChemicalTag tagToAdd in tagsToAdd) AddTag(tagToAdd);
        }

        public void RemoveTag(ChemicalTag tagToRemove)
        {
            if (tags.Remove(tagToRemove)) OnTagRemoved?.Invoke(tagToRemove);
        }

        public void RemoveTags(IEnumerable<ChemicalTag> tagsToRemove)
        {
            foreach (ChemicalTag tagToRemove in tagsToRemove) RemoveTag(tagToRemove);
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
            if (properties.TryAdd(property, value))
            {
                OnPropertyAdded?.Invoke(property, value);
            }
            else
            {
                properties[property] = value;
                OnPropertyChanged?.Invoke(property, value);
            }
        }

        public void RemoveProperty(string property)
        {
            if (properties.Remove(property)) OnPropertyRemoved?.Invoke(property);
        }

        public void RemoveProperties(IEnumerable<string> properties)
        {
            foreach(string property in properties) RemoveProperty(property);
        }

        public bool HasProperty(string property)
        {
            return properties.ContainsKey(property);
        }
    }
}

using HHG.Common.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    public interface IReactionCriteria
    {
        public bool Evaluate(ChemicalProfile profile);
    }

    [System.Serializable]
    public class RequireTagCriteria : IReactionCriteria
    {
        [SerializeField] private ChemicalTag tag;

        public bool Evaluate(ChemicalProfile profile) => profile.HasTag(tag);
    }

    [System.Serializable]
    public class ExcludeTagCriteria : IReactionCriteria
    {
        [SerializeField] private ChemicalTag tag;

        public bool Evaluate(ChemicalProfile profile) => !profile.HasTag(tag);
    }

    [System.Serializable]
    public class AnyOfTagsCriteria : IReactionCriteria
    {
        [SerializeField] private List<ChemicalTag> tags = new List<ChemicalTag>();

        public bool Evaluate(ChemicalProfile profile) => profile.HasAnyTag(tags);
    }

    [System.Serializable]
    public class PropertyCriteria : IReactionCriteria
    {
        [SerializeField] private string property;
        [SerializeField] private ComparisonOp op;
        [SerializeField] private float threshold;

        public bool Evaluate(ChemicalProfile profile)
        {
            float value = profile.GetProperty(property);
            return op.Evaluate(value, threshold);
        }
    }
}

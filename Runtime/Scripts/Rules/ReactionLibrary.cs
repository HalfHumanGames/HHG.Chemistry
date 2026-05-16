using System.Collections.Generic;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [CreateAssetMenu(fileName = "ReactionLibrary", menuName = "HHG/Chemistry System/Reaction Library")]
    public class ReactionLibrary : ScriptableObject
    {
        public IReadOnlyList<ReactionRule> Rules => rules;

        [SerializeField, ReorderableList] private List<ReactionRule> rules = new List<ReactionRule>();

        public void Evaluate(ChemicalProfile a, ChemicalProfile b, List<ReactionResult> results)
        {
            results.Clear();

            foreach (ReactionRule rule in rules)
            {
                if (rule.Matches(a, b))
                {
                    results.Add(new ReactionResult(rule, a, b));
                }
                else if (rule.Bidirectional && rule.Matches(b, a))
                {
                    results.Add(new ReactionResult(rule, b, a));
                }
            }
        }
    }
}

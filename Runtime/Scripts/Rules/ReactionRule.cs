using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [CreateAssetMenu(fileName = "ReactionRule", menuName = "HHG/Chemistry System/Reaction Rule")]
    public class ReactionRule : ScriptableObject
    {
        public bool Bidirectional => bidirectional;
        public IReadOnlyList<IReactionCriteria> InitiatorCriteria => initiatorCriteria;
        public IReadOnlyList<IReactionCriteria> ReceiverCriteria  => receiverCriteria;
        public IReadOnlyList<IReactionEffect> Effects => effects;

        [SerializeField] private bool bidirectional = true;
        [SerializeReference, SubclassSelector] private List<IReactionCriteria> initiatorCriteria = new List<IReactionCriteria>();
        [SerializeReference, SubclassSelector] private List<IReactionCriteria> receiverCriteria = new List<IReactionCriteria>();
        [SerializeReference, SubclassSelector] private List<IReactionEffect> effects = new List<IReactionEffect>();

        public bool Matches(ChemicalProfile initiator, ChemicalProfile receiver) =>
            initiatorCriteria.All(c => c.Evaluate(initiator)) &&
            receiverCriteria.All(c => c.Evaluate(receiver));
    }
}

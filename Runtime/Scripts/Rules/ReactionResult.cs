namespace HHG.Chemistry.Runtime
{
    public class ReactionResult
    {
        public ReactionRule Rule { get; }
        public ChemicalProfile Initiator { get; }
        public ChemicalProfile Receiver { get; }

        internal ReactionResult(ReactionRule rule, ChemicalProfile initiator, ChemicalProfile receiver)
        {
            Rule = rule;
            Initiator = initiator;
            Receiver = receiver;
        }

        public void Apply()
        {
            foreach (IReactionEffect effect in Rule.Effects)
            {
                if (effect.Target == EffectTarget.Initiator || effect.Target == EffectTarget.Both) effect.Apply(Initiator);
                if (effect.Target == EffectTarget.Receiver || effect.Target == EffectTarget.Both) effect.Apply(Receiver);
            }
        }
    }
}

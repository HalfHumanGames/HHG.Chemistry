using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    public interface IReactionEffect
    {
        public EffectTarget Target => EffectTarget.None;

        public void Apply(ChemicalProfile profile) { }
    }

    public enum EffectTarget { None, Initiator, Receiver, Both }

    [System.Serializable]
    public class AddTagEffect : IReactionEffect
    {
        public EffectTarget Target => target;

        [SerializeField] private ChemicalTag tag;
        [SerializeField] private EffectTarget target;

        public void Apply(ChemicalProfile profile) => profile.AddTag(tag);
    }

    [System.Serializable]
    public class RemoveTagEffect : IReactionEffect
    {
        public EffectTarget Target => target;

        [SerializeField] private ChemicalTag tag;
        [SerializeField] private EffectTarget target;

        public void Apply(ChemicalProfile profile) => profile.RemoveTag(tag);
    }

    [System.Serializable]
    public class SetPropertyEffect : IReactionEffect
    {
        public EffectTarget Target => target;

        [SerializeField] private string key;
        [SerializeField] private float value;
        [SerializeField] private EffectTarget target;

        public void Apply(ChemicalProfile profile) => profile.SetProperty(key, value);
    }
}

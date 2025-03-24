using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HHG.Chemistry.Runtime
{
    [CreateAssetMenu(fileName = "Chemistry Engine", menuName = "HHG/Chemistry System/Chemistry Engine")]
    public class ChemistryEngineAsset : ScriptableObject
    {
        [SerializeField] private List<ChemistryInteraction> interactions = new List<ChemistryInteraction>();

        public bool TryInteract(ElementAsset interactor, ElementAsset interactee, out ElementAsset result)
        {
            ChemistryInteraction interaction = interactions.FirstOrDefault(i => i.Interactor == interactor && i.Interactee == interactee);

            result = interaction?.Result;

            return result != null;
        }
    }
}
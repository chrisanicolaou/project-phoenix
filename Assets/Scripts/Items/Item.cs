using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items/Item", order = 1)]
    public class Item : ScriptableObject
    {
        [field: SerializeField]
        public int Id { get; set; }
        
        [field: SerializeField]
        public string Name { get; set; }
        
        [field: SerializeField]
        public string Description { get; set; }
        
        [field: SerializeField]
        public Sprite Sprite { get; set; }
        
        [field: SerializeField]
        public int Cost { get; set; }
        
        [field: SerializeField]
        public int Value { get; set; }
    }
}
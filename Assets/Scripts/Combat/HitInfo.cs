using ChiciStudios.ProjectPhoenix.Enums;

namespace ChiciStudios.ProjectPhoenix.Combat
{
    public class HitInfo
    {
        public int Damage { get; set; }
        public HitType Type { get; set; }

        public HitInfo(int damage, HitType type)
        {
            Damage = damage;
            Type = type;
        }
    }
}
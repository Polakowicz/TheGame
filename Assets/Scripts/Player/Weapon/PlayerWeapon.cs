using Scripts.Tools;

namespace Scripts.Player
{
    public abstract class PlayerWeapon : ExtendedMonoBehaviour
    {
        // Weapon types
        public enum WeaponType
        {
            Blaster,
            Blade
        }
        public WeaponType Type { get; protected set; }

        // Weapon actions
        public virtual void PerformBasicAttack() { }

        public virtual void PerformStrongerAttack() { }
        public virtual void CancelStrongerAttack() { }

        public virtual void StartAlternativeAttack() { }
        public virtual void PerformAlternativeAttack() { }
        public virtual void CancelAlternativeAttack() { }

        // Action needed only for blaster
        public virtual void PerformBeamPullAction(float input) { }
    }
}

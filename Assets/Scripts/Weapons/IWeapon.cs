using System;

namespace Weapons
{
    public interface IWeapon
    {
        public void Shoot();

        public void Reload(Action onComplete);
        
        public int RoundsRemaining { get; }
        public bool IsReloading { get; }
    }
}

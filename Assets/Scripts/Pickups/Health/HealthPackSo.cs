using UnityEngine;

namespace Pickups
{
    [CreateAssetMenu(fileName = "HealthPack", menuName = "HostileGrounds/HealthPack")]
    public class HealthPackSo : PickupData
    {
        public int healthToRestore = 0;
    }
}

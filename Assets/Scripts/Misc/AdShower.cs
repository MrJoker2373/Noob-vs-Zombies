namespace Game
{
    using UnityEngine;
    using System.Runtime.InteropServices;
    public class AdShower : MonoBehaviour
    {
        [DllImport("__Internal")]
        public static extern void ShowAd();
    }
}
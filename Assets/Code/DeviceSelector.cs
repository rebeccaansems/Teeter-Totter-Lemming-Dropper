using UnityEngine;

public class DeviceSelector : MonoBehaviour
{
    public static DeviceType DEVICE;
    public enum DeviceType { Phone, Tablet }

    public GameObject TabletGame, PhoneGame;

    void Awake()
    {
        if (Camera.main.aspect > 0.6f)
        {
            DEVICE = DeviceType.Tablet;
            Destroy(PhoneGame);

        }
        else
        {
            DEVICE = DeviceType.Phone;
            Destroy(TabletGame);
        }
    }
}
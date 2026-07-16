using UnityEngine;

public class MovileAssetsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mobileAssets;
    [SerializeField]
    private GameObject[] desktopAssets;

    private void Awake()
    {
        #if UNITY_IOS|| UNITY_ANDROID
        foreach (var asset in mobileAssets)
        {
            asset.SetActive(true);
        }
        #else
            foreach(var asset in desktopAssets)
            {
                asset.SetActive(true);
            }
        #endif
    }
}

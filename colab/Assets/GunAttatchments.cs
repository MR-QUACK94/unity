using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunAttatchments : MonoBehaviour
{
    public enum OvverideType
    {
        AdsOverride,
        ZoomOverride,
    }

    public GameObject[] attatchments;
    public Gun gun;
    public Recoil recoil;

    private void Start()
    {
        gun = GetComponent<Gun>();
        recoil = GetComponent<Recoil>();
        foreach(var attatchment in attatchments)
        {
            var values = attatchment.GetComponent<GunAttatchment>();
            if (values.overrideAdsPos) gun.adsPos = values.adsPos;
            if (values.overrideAdsZoom) gun.adsZoom = values.adsZoom;
            if (values.recoilOverride) 
            {
                recoil.minRecoil = values.minRecoil;
                recoil.maxRecoil = values.maxRecoil;
                recoil.minAngle = values.minRecoilAngle;
                recoil.maxAngle = values.maxRecoilAngle;
            }
        }
    }
}

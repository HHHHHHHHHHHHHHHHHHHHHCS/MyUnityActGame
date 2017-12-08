using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private WeaponTrail myTrail;
    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

    private void Awake()
    {
        myTrail = gameObject.GetComponentInChildren<WeaponTrail>(true);
    }

    private void LateUpdate()
    {
        if(myTrail)
        {
            t = Mathf.Clamp(Time.deltaTime,0, 0.066f);

            if(t>0)
            {
                while(tempT<t)
                {
                    tempT += animationIncrement;
                    if(myTrail.time>0)
                    {
                        myTrail.Itterate(Time.time - t + tempT);
                    }
                    else
                    {
                        myTrail.ClearTrail();
                    }
                }
            }
        }

        tempT -= t;

        if(myTrail.time>0)
        {
            myTrail.UpdateTrail(Time.time, t);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Toggle()
    {
        if (gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}

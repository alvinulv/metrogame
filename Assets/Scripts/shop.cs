using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    int upgradePrice1 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void upgrade1()
    {
        upgradePrice1++;
        float tempUpgradePrice1 = upgradePrice1;
        upgradePrice1 = (int)(upgradePrice1 * 1.2f);
        Debug.Log(upgradePrice1);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public void goToLevel(int level)
    {
        Application.LoadLevel(level);
    }

    public void exit()
    {
        Application.Quit();
    }
}

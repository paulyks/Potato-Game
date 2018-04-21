using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour {

	public void goToLevel(int level)
    {
        Application.LoadLevel(level);
    }
}

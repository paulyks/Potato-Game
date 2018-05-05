using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameMode : MonoBehaviour {

	public void normalMode()
    {
        StaticVariables.gameMode = "normal";
        goToLevel();
    }

    public void survivalMode()
    {
        StaticVariables.gameMode = "survival";
        goToLevel();
    }

    private void goToLevel()
    {
        Application.LoadLevel(2);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegendButtons : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Image legend;

    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    private int legendIndex = 0;

	public void goToLevel(int level)
    {
        Application.LoadLevel(level);
    }

    private void Start()
    {
        if (legendIndex == 0)
        {
            leftButton.GetComponent<Button>().interactable = false;
        }
    }

    public void left()
    {
        if (legendIndex > 0)
        {
            legendIndex--;

            if (legendIndex == 0)
            {
                leftButton.GetComponent<Button>().interactable = false;
            }
            legend.sprite = sprites[legendIndex];
        }
        if (legendIndex < sprites.Length - 1)
        {
            rightButton.GetComponent<Button>().interactable = true;
        }
    }

    public void right()
    {
        print("ok");
        if (legendIndex < sprites.Length - 1)
        {
            legendIndex++;

            if (legendIndex == sprites.Length - 1)
            {
                rightButton.GetComponent<Button>().interactable = false;
            }
            legend.sprite = sprites[legendIndex];
        }
        if (legendIndex > 0)
        {
            leftButton.GetComponent<Button>().interactable = true;
        }
    }
}

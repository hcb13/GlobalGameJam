using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textTime;
    [SerializeField] private Image imageHouse;
    [SerializeField] private Image imageAnne;

    private void Start()
    {
        Win();
    }
    private void Win()
    {
        if(PlayerPrefs.GetInt("WIN") == 1)
        {
            textTime.text = "You took " + PlayerPrefs.GetFloat("TIME").ToString() + " seconds to find Anne Bunny's house";
            imageHouse.gameObject.SetActive(true);
        }
        else
        {
            textTime.text = "You couldn't find Anne Bunny's house";
            imageHouse.gameObject.SetActive(false);
            imageAnne.GetComponent<Animator>().SetBool("Lost", true);
        }
    }
}

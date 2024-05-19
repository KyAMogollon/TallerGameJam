using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    [SerializeField] Image[] images;
    [SerializeField] Button btnContinue;
    [SerializeField] Image imageToReplace;
    public int index = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        images[index].gameObject.SetActive(true);
        index++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(index < images.Length) 
            {
                images[index].gameObject.SetActive(true);
                index++;

            }
            if (index == images.Length)
            {
                btnContinue.gameObject.SetActive(true);
            }
        }
    }
}

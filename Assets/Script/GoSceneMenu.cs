using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoSceneMenu : MonoBehaviour
{
    public bool canPassSceneMenu=false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        canPassSceneMenu=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPassSceneMenu)
        {
            if(Input.GetKeyUp(KeyCode.Mouse0)) 
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}

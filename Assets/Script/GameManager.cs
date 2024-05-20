using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    Controller _player;
    [SerializeField] TMP_Text piecesText;
    [SerializeField] GameObject panelPause;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _player = FindObjectOfType<Controller>();
        _player.RestartState();
        _player.setIndexPos(0);
        _player.InitPlayer();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            panelPause.SetActive(true);
            OnPause();
        }
    }
    public void UpdatePiecesText(int currentPieces, int quantityPieces)
    {
        piecesText.text = currentPieces + "/" + quantityPieces;
    }
    public void OnPause()
    {
        Time.timeScale = 0;
    }
    public void OnReanudar()
    {
        Time.timeScale = 1;
    }
    public void OnLeave()
    {
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance;
    [Header("分數")]
    public int score;

    public GameObject getScoreSfx;
    public Transform getScoreSfxPos;
    public Text scoreText;
    private void Start()
    {
        Instance = this;
    }

    [PunRPC]
    public void OnAddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        GameObject sfx = Instantiate(getScoreSfx, getScoreSfxPos.position, getScoreSfxPos.rotation);
        Destroy(sfx, 5);
    }

    /// <summary>
    /// 房主呼叫
    /// </summary>
    public void AddScore(int value)
    {
        photonView.RPC(nameof(OnAddScore), RpcTarget.AllBuffered, value);
    }

}
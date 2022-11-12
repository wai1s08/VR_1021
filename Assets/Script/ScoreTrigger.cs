using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ScoreTrigger : MonoBehaviour
{
    [Header("分數")]
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GameManager.Instance.AddScore(score);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public GameObject ik;
    public Transform headPos;
    public Transform rightHandPos;
    public Transform leftHandPos;

    public Text playerNameText;
    void Start()
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < ik.transform.childCount; i++)
            {
                ik.transform.GetChild(i).gameObject.layer = 8;
            }
            Debug.LogError("我自己");
            gameObject.name = PhotonNetwork.LocalPlayer.NickName;
            playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            Debug.LogError("別的玩家");
            gameObject.name = photonView.Owner.NickName;
            playerNameText.text = photonView.Owner.NickName;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            headPos.SetPositionAndRotation(NetworkConnect.Instance.headPos.position, NetworkConnect.Instance.headPos.rotation);
            rightHandPos.SetPositionAndRotation(NetworkConnect.Instance.rightHandPos.position, NetworkConnect.Instance.rightHandPos.rotation);
            leftHandPos.SetPositionAndRotation(NetworkConnect.Instance.leftHandPos.position, NetworkConnect.Instance.leftHandPos.rotation);

            photonView.RPC(nameof(OnSyncPlayerPos), RpcTarget.Others, headPos.position, headPos.rotation, rightHandPos.position, rightHandPos.rotation, leftHandPos.position, leftHandPos.rotation);
        }
    }

    [PunRPC]
    private void OnSyncPlayerPos(Vector3 _headPos, Quaternion _headRot, Vector3 _rightHandPos, Quaternion _rightHandRot, Vector3 _leftHandPos, Quaternion _leftHandRot)
    {
        headPos.SetPositionAndRotation(_headPos, _headRot);
        rightHandPos.SetPositionAndRotation(_rightHandPos, _rightHandRot);
        leftHandPos.SetPositionAndRotation(_leftHandPos, _leftHandRot);
    }

}
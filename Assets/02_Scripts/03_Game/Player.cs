using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2;

    Vector2 targetPos;

    float speed = 9f;

    public bool isGetItem;

    public GameObject getItem;

    GameObject clickedGameObject;//�N���b�N���ꂽ�Q�[���I�u�W�F�N�g��������ϐ�

    public enum PLAYER_MODE
    {
        MOVE,  // �ړ�
        BUSY,  // ���
        REST,  // �x�e
    }

    // �v���C���[�̃��[�h
    public PLAYER_MODE mode;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        isGetItem = false;
        mode = PLAYER_MODE.REST;
        targetPos = new Vector2();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && mode != PLAYER_MODE.BUSY)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                Debug.Log(clickedGameObject.name);//�Q�[���I�u�W�F�N�g�̖��O���o��
                Debug.Log(clickedGameObject.tag);//�Q�[���I�u�W�F�N�g�̖��O���o��

                if (clickedGameObject.transform.tag == "Sapling")
                {// �c�؂̏ꍇ
                    targetPos = clickedGameObject.transform.position;

                    if (targetPos.y > 0)
                    {
                        targetPos = new Vector2(targetPos.x, targetPos.y - 1.5f);
                    }
                    else
                    {
                        targetPos = new Vector2(targetPos.x, targetPos.y + 1.5f);
                    }
                }
                else if(clickedGameObject.transform.tag == "Item")
                {
                    targetPos = clickedGameObject.transform.position;

                    targetPos = new Vector2(targetPos.x - 1.5f, targetPos.y);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (targetPos == Vector2.zero)
        {
            return;
        }

        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);

        if(clickedGameObject.tag == "Item")
        {
            float dis = Vector3.Distance(targetPos, this.transform.position);

            if (dis <= 0.2f)
            {// �ړI�n�ɂ��ǂ蒅����

                if(getItem != null)
                {
                    getItem.GetComponent<Item>().isPlayer = false;
                }

                isGetItem = true;
                clickedGameObject.GetComponent<Item>().isPlayer = true;
                getItem = clickedGameObject;
            }
        }
    }
}

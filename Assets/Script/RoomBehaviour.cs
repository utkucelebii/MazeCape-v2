using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] floors;
    public GameObject[] walls;
    public GameObject[] doors;
    public Transform enemies;

    public GameObject enemy;


    public void UpdateRoom(bool[] status, int currentRoomCount, bool finishRoom)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }

        if(Random.Range(0, 5) <= 2 && currentRoomCount > 1)
        {
            GenerateEnemy();
        }

        if (finishRoom)
        {
            floors[0].GetComponent<MeshRenderer>().material.color = Color.blue;
            floors[0].transform.tag = "Finish";
        }
    }

    private void GenerateEnemy()
    {
        GameObject randomFloor = floors[Random.Range(1, floors.Length)];
        Vector3 enemyPos = randomFloor.transform.position;
        enemyPos.y += enemy.transform.localScale.y / 2;
        enemy = Instantiate(enemy, enemyPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().startingPos = enemyPos;
        enemy.transform.LookAt(floors[0].transform);
        enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.rotation.eulerAngles.y, 0);
        enemy.transform.parent = enemies;
    }
}

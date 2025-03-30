using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CustomerScript : MonoBehaviour
{
    public enum Role { Buyer, Seller };

    public Role role;
    public int price;
    public float rotationSpeed;
    public GameObject itemForSale;
    private GameObject player;
    private WorldInfo worldInfo;
    private float targetPlayerDistance;
    private Vector3 currentDirection;
    private Quaternion targetRotation;

    private void Start() {
        player = GameObject.FindWithTag("Player");
        LoadWorldInfo();
        targetPlayerDistance = worldInfo.targetCustomerDistance;
        currentDirection = new Vector3(0.0f, 0.0f, 0.0f);
        targetRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }


    private void FixedUpdate() {
        if (DistanceToPlayer() <= targetPlayerDistance) {
            TurnToPlayer();
        }
    }


    private void LoadWorldInfo() {
        worldInfo = AssetDatabase.LoadAssetAtPath<WorldInfo>("Assets/WorldInfo.asset");
        if (!worldInfo) {
            worldInfo = ScriptableObject.CreateInstance<WorldInfo>();
            AssetDatabase.CreateAsset(worldInfo, "Assets/WorldInfo.asset");
        }
    }


    private float DistanceToPlayer() {
        if (player) {
            return Vector3.Distance(player.transform.position, transform.position);
        }
        return 0.0f;
    }


    private void TurnToPlayer() {
        if (player) {
            currentDirection = (player.transform.position - transform.position).normalized;
            currentDirection.y = 0.0f;

            targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    public GameObject GetItemForSale()
    {
        return itemForSale;
    }

    public int GetPrice()
    {
        return price;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectController : MonoBehaviour
{
    public List<GameObject> list;
    public GameObject nodestart;
    [SerializeField] private Animator animator;
    [SerializeField] private Image fill;
    [SerializeField] private float count;
    public static int coinCount;
    public static bool coinTake;
    public GameObject gemPrefab;
    private float timeRemaining;
    private void Start()
    {
        timeRemaining = 0f;
        list = new List<GameObject>();
        animator = this.GetComponent<Animator>();

        if (PlayerPrefs.GetInt("UpgradeCount") > 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("UpgradeCount"); i++)
            {
                GameObject gem = Instantiate(gemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                gem.gameObject.transform.position = transform.position + Vector3.forward;

                gem.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                gem.gameObject.AddComponent<NodeMovement>();
                if (list.Count == 0)
                {
                    gem.gameObject.GetComponent<NodeMovement>().connectedNode = nodestart.transform;

                }
                else
                {

                    gem.gameObject.GetComponent<NodeMovement>().connectedNode = list[list.Count - 1].transform;
                }
                list.Add(gem.gameObject);

                gem.gameObject.tag = "Collected";
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (list.Count < 8)
        {
            if (other.gameObject.CompareTag("Collect"))
            {

                other.gameObject.transform.position = transform.position + Vector3.forward;

                other.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                other.gameObject.AddComponent<NodeMovement>();
                if (list.Count == 0)
                {
                    other.gameObject.GetComponent<NodeMovement>().connectedNode = nodestart.transform;

                }
                else
                {

                    other.gameObject.GetComponent<NodeMovement>().connectedNode = list[list.Count - 1].transform;
                }
                list.Add(other.gameObject);
                
                other.gameObject.tag = "Collected";
            }
        }

        if (other.gameObject.CompareTag("EndLine"))
        {
            ForwardMovement.start = false;
            animator.SetBool("Start", false);
            animator.SetBool("Finish", true);
            int count = list.Count-1;
            for (int i = count; i >-1; i--)
            {
                Destroy(list[i]);
                list.RemoveAt(i);
                coinCount += 3;
            }
            GameObject.Find("StackBar").SetActive(false);

            UIManager.gameEnd = true;
            UIManager.check = true;

        }

        if (other.gameObject.CompareTag("Obstacle") && timeRemaining == 0f)
        {
            if (list.Count < 1)
            {

            }
            else if(list.Count >=1 && list.Count < 2)
            {
                Destroy(list[0]);
                list.RemoveAt(0);
            }
            else if(list.Count >=2 && list.Count < 3)
            {
                Destroy(list[1]);
                list.RemoveAt(1);
                Destroy(list[0]);
                list.RemoveAt(0);
                
            }
            else if(list.Count >= 3)
            {
                Destroy(list[list.Count - 3]);
                list.RemoveAt(list.Count - 3);
                Destroy(list[list.Count - 2]);
                list.RemoveAt(list.Count - 2);
                Destroy(list[list.Count-1]);   
                list.RemoveAt(list.Count - 1);
                
                
            }
            timeRemaining = 1.5f; /// blade hits twice or thrice when pass through
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("GoldCount", PlayerPrefs.GetInt("GoldCount") + 1);
            Destroy(other.gameObject);
            coinCount += 1;
            coinTake = true;
        }
    }


    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
        {
            timeRemaining = 0f;
        }
        if (list.Count == 8)
        {
            animator.SetBool("StackFull", true);
            ForwardMovement.moveSpeed = 8f;
        }
        if (list.Count < 8)
        {
            animator.SetBool("StackFull", false);
            ForwardMovement.moveSpeed = 4f;
        }
        count = list.Count / 8f;
        fill.fillAmount = count;

        if (UIManager.upgradeDone)
        {
            GameObject gem = Instantiate(gemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            gem.gameObject.transform.position = transform.position + Vector3.forward;

            gem.gameObject.GetComponent<MeshCollider>().isTrigger = false;
            gem.gameObject.AddComponent<NodeMovement>();
            if (list.Count == 0)
            {
                gem.gameObject.GetComponent<NodeMovement>().connectedNode = nodestart.transform;

            }
            else
            {

                gem.gameObject.GetComponent<NodeMovement>().connectedNode = list[list.Count - 1].transform;
            }
            list.Add(gem.gameObject);

            gem.gameObject.tag = "Collected";
            UIManager.upgradeDone = false;
        }
    }
}

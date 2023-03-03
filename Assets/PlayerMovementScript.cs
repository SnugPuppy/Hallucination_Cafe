using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject customerPrefab;
    public Vector3 horizMove;
    public Vector3 vertMove;
    public bool gotIngredient;
    public bool customerTimerOn;
    public bool brewingTimer;
    public int beverageCount;
    public int customerCount;
    public float customerTimer;
    public float brewingTimerCount;
    public TMP_Text informationText;
    public GameObject song2;
    // Start is called before the first frame update
    void Start()
    {
        informationText = GameObject.Find("InformationText").GetComponent<TMP_Text>();
        informationText.text = "Start by grabbing a green ingredient.";
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().position -= horizMove;
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().position += vertMove;
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().position += horizMove;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().position -= vertMove;
        }
        // Customers
        if (customerTimer >= 2)
        {
            Instantiate(customerPrefab);
            customerTimer = 0;
            customerTimerOn = false;
        }
        if (customerTimerOn == true)
        {
            CustomerWait();
        }
        // Items
        if (brewingTimer == true)
        {
            BrewWait();
        }
        if (brewingTimerCount >= 5)
        {
            Debug.Log("Done!");
            beverageCount += 1;
            brewingTimer = false;
            brewingTimerCount = 0;
            informationText.text = "Done!";
        }
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Ingredient1") //Ingredient Interaction
        {
            if (gotIngredient == false)
            {
                gotIngredient = true;
                Debug.Log("Ingredient 1 Got!");
                informationText.text = "";
                if (customerCount == 0)
                {
                    informationText.text = "Bring the ingredient to the brewing machine";
                }
            }
            else if (gotIngredient == true)
            {
                Debug.Log("You can only carry one!");
                informationText.text = "You can only carry one batch of ingredients at a time.";
            }
        }
        if (collision.gameObject.tag == "BrewMach")
        {
            if(gotIngredient == true)
            {
                Debug.Log("Brewing...");
                gotIngredient = false;
                brewingTimer = true;
                informationText.text = "Brewing...";
            }
            else if(gotIngredient == false)
            {
                Debug.Log("Get an ingredient first!");
            }
        }
        if(collision.gameObject.tag == "customer") //Serving
        {
            if(beverageCount >= 1)
            {
                beverageCount -= 1;
                Debug.Log("Order Up!");
                customerTimerOn = true;
                customerCount += 1;
                if (customerCount == 1)
                {
                    informationText.text = "That's how you complete an order!";
                    Instantiate(song2);
                }
                else if (customerCount >= 1)
                {
                    informationText.text = "";
                }
                if (customerCount != 3)
                {
                    Destroy(collision.gameObject);
                }
                if (customerCount == 3)
                {
                    informationText.text = "You spilled the beverage, the customer... Dissapeared?";
                    Destroy(collision.gameObject);
                }
            }
            else if (beverageCount == 0)
            {
                Debug.Log("You don't have any beverages to give!");
                if (customerCount == 3)
                {
                    informationText.text = "Huh? Nobody was there.";
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    public void CustomerWait()
    {
        customerTimer += Time.deltaTime;
    }
    public void BrewWait()
    {
        brewingTimerCount += Time.deltaTime;
    }
}

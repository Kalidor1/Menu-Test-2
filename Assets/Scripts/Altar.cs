using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Altar : MonoBehaviour
{

    public GameObject altarPopup;
    public ParticleSystem altarParticles;
    public GameObject altarButtonContainer;
    public GameObject altarButton;
    private bool visible = false;


    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.atAltar && !visible && GameController.Instance.canSacrifice)
        {
            ShowAltar();
            visible = true;
        }
        else if (!GameController.Instance.canSacrifice)
        {
            visible = false;
            foreach (Transform child in altarButtonContainer.transform)
            {
                Destroy(child.gameObject);
            }

            //add text to altarButtonContainer
            var text = Instantiate(altarButton, altarButtonContainer.transform);
            text.GetComponentInChildren<Image>().enabled = false;

            text.GetComponentInChildren<TextMeshProUGUI>().text = "You sacrificed too much today";
            text.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        if (!GameController.Instance.atAltar)
        {
            visible = false;
            foreach (Transform child in altarButtonContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator ShowPopup(InventoryItem item)
    {
        //change texture sheet to item texture sheet
        altarParticles.textureSheetAnimation.SetSprite(0, item.Icon);
        altarParticles.Play();

        //Create copy of popup
        var popup = Instantiate(altarPopup, altarPopup.transform.parent);
        popup.SetActive(true);

        //Set sprite of sprite renderer
        popup.GetComponentInChildren<SpriteRenderer>().sprite = item.PopUp;

        //Add upwards force
        popup.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));

        yield return new WaitForSeconds(2);

        Destroy(popup);
    }

    void ShowAltar()
    {
        foreach (Transform child in altarButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Render inventory as buttons
        foreach (var item in GameController.Instance.inventory.items)
        {
            var button = Instantiate(altarButton, altarButtonContainer.transform);
            //move button down a bit
            var offset = 50 * GameController.Instance.inventory.items.IndexOf(item);
            button.transform.position += new Vector3(0, -offset, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            button.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                GameController.Instance.inventory.RemoveItem(item);
                StartCoroutine(ShowPopup(item));
                GameController.Instance.ApplyStats(item);
                GameController.Instance.karma++;
                GameController.Instance.sacrificed++;
                ShowAltar();
            });
        }

        if (GameController.Instance.karma >= GameController.Instance.karmaEasterEgg)
        {
            var button = Instantiate(altarButton, altarButtonContainer.transform);
            //move button down a bit
            var offset = 50 * GameController.Instance.inventory.items.Count;
            button.transform.position += new Vector3(0, -offset, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Yourself";
            button.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                //DIE 
                SceneController.Instance.LoadScene("GameWin");
            });
        }
    }
}

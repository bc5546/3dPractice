using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lashCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractedGameObject;
    private Iinteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        CharacterManager.Instance.player.getItem += GetItem;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.Instance.player.controller.isThirdView == false && Time.time - lashCheckTime > checkRate)
        {
            lashCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractedGameObject)
                {
                    curInteractedGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<Iinteractable>();

                    SetPromptText();
                }
            }
            else
            {
                curInteractedGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if(CharacterManager.Instance.player.controller.isThirdView == true)
        {
            promptText.gameObject.SetActive(false);
        }
        
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractedGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    public void GetItem()
    {
        switch(CharacterManager.Instance.player.itemData.consumables.type)
        {
            case ConsumableType.Speed:
                StartCoroutine(SpeedBoost(CharacterManager.Instance.player.itemData.consumables.value));
                break;
            case ConsumableType.Health:
                break;
        }
    }

    IEnumerator SpeedBoost(float value)
    {
        GetComponent<PlayerController>().isRunning = true;
        GetComponent<PlayerController>().moveSpeed += value;
        GetComponent<Animator>().SetBool("Run", true);
        yield return new WaitForSeconds(10f);
        GetComponent<PlayerController>().isRunning = false;
        GetComponent<PlayerController>().moveSpeed -= value;
        GetComponent<Animator>().SetBool("Run", false);
    }
}
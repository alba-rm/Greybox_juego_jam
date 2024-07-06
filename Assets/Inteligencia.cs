using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Inteligencia : MonoBehaviour
{
    public NPCConversation myConversation;

    private void Update()
    {
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        if (Input.GetButton("Fire2"))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
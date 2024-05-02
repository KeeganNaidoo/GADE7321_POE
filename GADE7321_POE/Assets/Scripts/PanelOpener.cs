using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject p1Character1Panel;
    public GameObject p1Character2Panel;
    public GameObject p2Character1Panel;
    public GameObject p2Character2Panel;
    
    public GameObject p1CharacterSelectPanel;
    public GameObject p2CharacterSelectPanel;
    
    public Text dialogueText;
    // Add a field to keep track of the selected player unit
    public string selectedPlayerUnit;
    
    
    void Start()
    {
        p1Character1Panel.SetActive(false);
        p1Character2Panel.SetActive(false);
        p2Character1Panel.SetActive(false);
        p2Character2Panel.SetActive(false);
    }
    
    public void SelectPlayerUnit(string unitName)
    {
        selectedPlayerUnit = unitName;
        // You can perform additional actions here based on the selected unit
        Debug.Log("Selected Player Unit: " + selectedPlayerUnit);
    }
    
    public void P1Character1OpenPanel()
    {
        if (p1Character1Panel != null)
        {
            p1CharacterSelectPanel.SetActive(false);
            p1Character1Panel.SetActive(true);
            dialogueText.text = "Player 1's turn!\n" + selectedPlayerUnit + " chosen\nChoose an Action:";
            SelectPlayerUnit("Player 1 Unit 1");
        }
        
    }
    
    public void P1Character2OpenPanel()
    {
        if (p1Character2Panel != null)
        {
            p1CharacterSelectPanel.SetActive(false);
            p1Character2Panel.SetActive(true);
            dialogueText.text = "Player 1's turn!\n" + selectedPlayerUnit + " chosen\nChoose an Action:";
            SelectPlayerUnit("Player 1 Unit 2");
        }
        
    }
    
    public void P2Character1OpenPanel()
    {
        if (p2Character1Panel != null)
        {
            p2CharacterSelectPanel.SetActive(false);
            p2Character1Panel.SetActive(true);
            dialogueText.text = "Player 2's turn!\n" + selectedPlayerUnit + " chosen\nChoose an Action:";
            SelectPlayerUnit("Player 2 Unit 1");
        }
        
    }
    
    public void P2Character2OpenPanel()
    {
        if (p2Character2Panel != null)
        {
            p2CharacterSelectPanel.SetActive(false);
            p2Character2Panel.SetActive(true);
            dialogueText.text = "Player 2's turn!\n" + selectedPlayerUnit + " chosen\nChoose an Action:";
            SelectPlayerUnit("Player 2 Unit 2");
        }
        
    }
}

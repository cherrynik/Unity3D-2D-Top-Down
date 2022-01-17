using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField, ReadOnly] private Interaction interaction;
    [SerializeField, ReadOnly] private bool isAccessible = false;
    [SerializeField] GameObject compareWith;

    public bool IsAccessible {
        get => isAccessible;
    }

    private void SwitchAccessIfPlayer(Collider2D other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            interaction ??= target.GetComponent<Interaction>();
            isAccessible = !isAccessible;
        }

        if (isAccessible)
        {
            OnAccess();
        }
        else
        {
            OnInaccess();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SwitchAccessIfPlayer(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SwitchAccessIfPlayer(other);
    }

    private void OnAccess()
    {
        interaction.Targets.Add(this);
    }

    private void OnInaccess()
    {
        if (interaction.Targets.Contains(this))
        {
            interaction.Targets.Remove(this);
        }
    }

}

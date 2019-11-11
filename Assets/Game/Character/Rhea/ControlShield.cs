using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShield : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private GameObject character;

    private GameObject shield;

    private RheaShield rs;

    private Selene selene;

    private SeleneStateInput input;

   
    // Start is called before the first frame update
    void Start()
    {
        rs = shieldPrefab.GetComponent<RheaShield>();
        selene = character.GetComponent<Selene>();
        input = selene.input;

    }

    // Update is called once per frame
    void Update()
    {
        if (SpellMap.Instance.GetSpellDown(input.cc.playerNumber, SpellType.HEAVY) && shield == null)
        {
            shield = Instantiate(shieldPrefab, character.transform);
        }
        
    }
}

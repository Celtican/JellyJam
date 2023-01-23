using UnityEngine;

public class PlayerTraits : MonoBehaviour
{
    public static PlayerTraits Instance { get; private set; }
    
    public Traits playerTraits;
    public TraitsList playerTraitsList;
    public bool prefersFemales;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    
        playerTraits = GetComponent<Traits>();
        playerTraitsList = playerTraits.GetComponent<TraitsList>();
        playerTraitsList.athletic.value = 5; 
        playerTraitsList.nerdy.value = 5;
        playerTraitsList.romantic.value = 5;
        playerTraitsList.funny.value = 5;
        playerTraitsList.animalLover.value = 5;
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrefersFemales(bool prefersFemales)
    {
        this.prefersFemales = prefersFemales;
    }

    


}

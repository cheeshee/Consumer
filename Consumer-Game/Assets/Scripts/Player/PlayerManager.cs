using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player instance that will save everything on scene changes
    public static PlayerManager Instance;

    // Array of possible controllers for player
    private PlayerController[] characterSlots = new PlayerController[8];
    [SerializeField]
    private int currCharacter = 0;
    private Rigidbody2D playerRb;

    // checking interactions for raycast
    private Collider2D closestInteraction;
    private float closestDist;
    private int interactionLayerMask;

    private PhysicsMaterial2D fullFriction;
    private PhysicsMaterial2D noFriction;

    [SerializeField] LayerMask collisionLayerMask;
    
    // detect consume variables
    protected bool canConsume;
    protected float startConsume;

    // detect climb variables
    protected bool canClimb;
    protected bool climbing;
    protected float climbTop;
    protected float climbBottom;

    protected bool inSlotSelection;


    private void Awake(){
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Instance.gameObject.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        playerRb = gameObject.GetComponent<Rigidbody2D>();


        fullFriction = Resources.Load<PhysicsMaterial2D>("PhysicsMaterial/" + "FullFrictionMaterial");
        noFriction = Resources.Load<PhysicsMaterial2D>("PhysicsMaterial/" + "NoFrictionMaterial");
        Debug.Assert(fullFriction != null);
        Debug.Assert(noFriction != null);


        // testing stuff
        characterSlots[0] = new InitialController(gameObject);
        characterSlots[0].OnSwitch(gameObject);
        characterSlots[0].percentageHealth = 1f;
        characterSlots[0].storedMaxHealth = 100f;
        characterSlots[1] = new VillagerController(gameObject);
        
        closestDist = Mathf.Infinity;
        
        // load player from playerstate
        // LoadPlayer();

    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("current Slot:" + currCharacter);
        DefaultState(); 
        if (inSlotSelection){
            SaveController();
        } else {           
            DetectMove();
            DetectJump();
            DetectClimb();
            DetectAttack();
            DetectConsume();
            DetectShapeShift();
        }

    }

    private void FixedUpdate(){
        characterSlots[currCharacter].FixedUpdate();        
    }

    private void DefaultState(){
        characterSlots[currCharacter].Default();
    }

    private void DetectMove(){
        if (Input.GetButton("Move")){
            characterSlots[currCharacter].Move();
        }        
    }

    private void DetectJump(){
        if (Input.GetButton("Jump")){
            characterSlots[currCharacter].Jump();
        }
    }

    private void DetectClimb(){
        if (!characterSlots[currCharacter].GetCanClimb()){
            climbing = false;
        } else if (climbing){
            if (Input.GetButtonDown("Interact")){
                climbing = false;
            } else {                
                characterSlots[currCharacter].Climb();
            }
        }
    }
    
    private void DetectAttack(){
        if(Input.GetButtonDown("Attack")){
            characterSlots[currCharacter].Attack();
        }
    }

    private void DetectConsume(){   
        if (canConsume){
            if (Input.GetButtonDown("Interact")){
                startConsume = Time.time;
            } else if (Input.GetButton("Interact") && (Time.time - startConsume) > 1f){     //hold time is 1 second
                // start consume
                inSlotSelection = true;
                Debug.Log("display some UI here");
            }
        }
    }

    private void DetectShapeShift(){
        int slot = GetSlotSelected();
        int prevCharacter = currCharacter;

        currCharacter = slot < 0? currCharacter : slot;
        if (characterSlots[currCharacter] != null){
            characterSlots[currCharacter].OnSwitch(gameObject);
        }
        else{
            currCharacter = prevCharacter;
        }
        
    }

    private void SaveController(){
        int saveSlot = GetSlotSelected();
        Debug.Log("choose a slot");
        if (saveSlot >= 0){
            Debug.Log("saveSlot = " + saveSlot);
            NpcAi deadNPC;
            deadNPC = closestInteraction.gameObject.GetComponent<NpcAi>();
            characterSlots[saveSlot] = deadNPC.GetController();
            //Delete Body
            closestInteraction.gameObject.SetActive(false);
            Debug.Log("newly saved: " + characterSlots[saveSlot]);
            LeaveClosestInteraction(closestInteraction);
            canConsume = false;
            inSlotSelection = false;
        }
    }

    private int GetSlotSelected(){
        int slot = -1;
        if(Input.GetButtonDown(InputProperties.FIRST))
        {
            slot = (int)InputProperties.Slots.FIRST;
        } 
        else if(Input.GetButtonDown(InputProperties.SECOND))
        {
            slot = (int)InputProperties.Slots.SECOND;
        } 
        else if(Input.GetButtonDown(InputProperties.THIRD))
        {
            slot = (int)InputProperties.Slots.THIRD;
        } 
        else if(Input.GetButtonDown(InputProperties.FOURTH))
        {
            slot = (int)InputProperties.Slots.FOURTH;
        } 
        else if(Input.GetButtonDown(InputProperties.FIFTH))
        {
            slot = (int)InputProperties.Slots.FIFTH;
        } 
        else if(Input.GetButtonDown(InputProperties.SIXTH))
        {
            slot = (int)InputProperties.Slots.SIXTH;
        } 
        else if(Input.GetButtonDown(InputProperties.EIGHTH))
        {
            slot = (int)InputProperties.Slots.EIGHTH;
        } 
        return slot;
    }

    public Vector2 GetPosition(){
        return gameObject.transform.position;
    }

    public GameObject GetGameObject(){
        return gameObject;
    }

    public Vector2 GetLocalScale(){
        return gameObject.transform.localScale;
    }

    public PhysicsMaterial2D GetFriction (bool friction){
        if (friction){
            return fullFriction;
        
        }
        else{
            return noFriction;
        }
    }

    public LayerMask GetPlayerLayerMask (){
        return collisionLayerMask;
    }

    public int GetCharType(){
        return characterSlots[currCharacter].GetCharType();
    }

    public Collider2D GetClosestInteraction(){
        return closestInteraction;
    }

    public float GetClimbTop(){
        return climbTop;
    }

    public float GetClimbBottom(){
        return climbBottom;
    }

    public void FlipHorizontal(bool facingRight){
        if (facingRight){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else{
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
    }

    public bool CheckClosestInteraction(Collider2D range){
        if (closestInteraction != range){
            float distTo = Mathf.Abs(range.transform.position.x - transform.position.x);
            if(distTo < closestDist){
                closestDist = distTo;
                closestInteraction = range;
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    }

    public void MarkClosestInteraction(Collider2D range){
        closestInteraction = range;
        closestDist = Mathf.Abs(range.transform.position.x - transform.position.x);
    }

    public void LeaveClosestInteraction(Collider2D range){
        if(closestInteraction == range){
            closestDist = Mathf.Infinity;
            closestInteraction = null;
        }
    }

    public void SetCanConsume(bool consume){
        // Debug.Log("consumable:" + consume);
        canConsume = consume;
    }

    public bool GetCanClimb() {
        return characterSlots[currCharacter].GetCanClimb();
    }
    public void Climbing(){
        if (characterSlots[currCharacter].GetCanClimb()){
            climbing = true;
            // playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
            climbTop = GetClosestInteraction().bounds.max.y;
            climbBottom = GetClosestInteraction().bounds.min.y;
        }
    }
     
    public void StopClimbing(){
        climbing = false;
    }

    public bool IsClimbing(){
        return climbing;
    }
}

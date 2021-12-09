using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePattern : MonoBehaviour
{
    private float checkRate = 0.1f;
    private float nextCheck = 0;
    public float sightRange = 10;
    public float detectBehindRange = 5;
    public float meleeAttackRange = 4;
    [Range(10, 50)]
    public float meleeAttackDamage = 10;

    [Range(0, 40)]
    public float rangeAttackRange = 35;
    [Range(5, 10)]
    public float rangeAttackDamage = 5;
    public float rangeAttackSpread = 0.5f;
    public float attackRate = 0.4f;
    public float nextAttack;
    public float offset = 0.4f;
    public int requiredDetectionCount = 15;
    public bool hasRangeAttack;
    public bool hasMeleeAttack;
    public bool isMeleeAttacking;

    public Transform myFollowTarget;
    [HideInInspector]
    public Transform pursueTarget;
    [HideInInspector]
    public Vector3 locationOfInterest;
    [HideInInspector]
    public Vector3 wanderTarget;
    [HideInInspector]
    public Transform myAttacker;

    //Used for sight
    public LayerMask sightLayers;
    public LayerMask myEnemyLayers;
    public LayerMask myFriendlyLayers;
    public string[] myEnemyTags;
    public string[] myFriendlyTags;

    //Ref 
    public Transform[] waypoints;
    public Transform head;
    public MeshRenderer meshRendererFlag;
    public GameObject rangeWeapon;
    public Ai_Delegation AiMaster;
    [HideInInspector]
    public NavMeshAgent myNavMeshAgent;

    //Used for state AI                
    public Ai_Interface currentState;
    public Ai_Interface capturedState;
    public Ai_MeleeAttack meleeAttackState;
    public Ai_RangeAttack rangeAttackState;
    public Ai_Patrol patrolState;
    public Ai_Alert alertState;
    public Ai_Pursue pursueState;
    public Ai_Struck struckState;
    public Ai_investigateharm investigateHarmState;
    // Start is called before the first frame update

    private void Awake()
    {
        SetupUpStateReferences();
        SetInitialReferences();

    }

    private void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(Time.time);
        if (StartGame.Instance.gm.isReady)
        {
            CarryOutUpdateState();
           // Debug.Log("IsReady is set");
        }
       // Debug.Log(" checking update");
    }
    private void OnDisable()
    {
        AiMaster.EventDeductHealth += ActivateStruckState;
    }
    public void SetupUpStateReferences()
    {
        struckState = new Ai_Struck(this);
        patrolState = new Ai_Patrol(this);
        alertState = new Ai_Alert(this);
        pursueState = new Ai_Pursue(this);
        meleeAttackState = new Ai_MeleeAttack(this);
        rangeAttackState = new Ai_RangeAttack(this);
        investigateHarmState = new Ai_investigateharm(this);
    }

    public void SetInitialReferences()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        ActivatePatrolState();
    }

    public void CarryOutUpdateState()
    {
        // Debug.Log(Time.time);
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            currentState.UpdateState();
            // Debug.Log("Checking state");
        }
    }
    public void ActivateStruckState(int dummy)
    {
        StopAllCoroutines();

        if (currentState != struckState)
        {
            capturedState = currentState;
        }
        if (myNavMeshAgent.enabled)
        {
            myNavMeshAgent.isStopped = true;
        }

        currentState = struckState;

        isMeleeAttacking = false;

        AiMaster.CallEventUnitStuck();

        StartCoroutine(RecoverFromStruckState());

    }

    IEnumerator RecoverFromStruckState()
    {
        yield return new WaitForSeconds(1.5f);

        AiMaster.CallEventUnitRecoveredAnim();

        if (rangeWeapon != null)
        {
            rangeWeapon.SetActive(true);
        }

        if (myNavMeshAgent.enabled)
        {
            myNavMeshAgent.isStopped = false;
        }

        currentState = capturedState;

    }

    void ActivatePatrolState()
    {
        currentState = patrolState;
        Debug.Log("Active patrol state");
    }
    public void OnEnemyAttack() //Called by melee attack animation
    {
        if (pursueTarget != null)
        {
            if (Vector3.Distance(transform.position, pursueTarget.position) <= meleeAttackRange)
            {
                Vector3 toOther = pursueTarget.position - transform.position;
                if (Vector3.Dot(toOther, transform.forward) > 0.5f)
                {
                //    pursueTarget.SendMessage("CallEventDeductHealth", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    pursueTarget.SendMessage("ProcessDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    pursueTarget.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
                }
            }
            isMeleeAttacking = false;
        }
    }
}
 
 
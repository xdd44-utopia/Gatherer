using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    ///<summary>
    ///状态机
    ///</summary>
    public class FSMBase : MonoBehaviour
    {
        #region 脚本生命周期
        private void Start()
        {
            InitComponents();
            ConfigFSM();
            InitDefaultState();
        }

        public FSMStateID test_stateID;

        public void Update()
        {
            test_stateID = currentState.StateID;
            currentState.Reason(this);
            currentState.ActionState(this);
        }
        #endregion

        #region 状态机自身成员
        //状态列表
        private List<FSMState> states;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;
        //当前状态
        private FSMState currentState;
        //默认状态
        private FSMState defaultState;
        [Tooltip("状态机配置文件")]
        public string fileName = "AI01.txt";

        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        //--通过配置文件
        private void ConfigFSM()
        {
            states = new List<FSMState>();
           
            var Map = AIConfigReaderFactory.GetMap(fileName);
           
            foreach (var state in Map)
            {
                //state.Key 状态名称
                //state.Value 映射
                Type type = Type.GetType("AI.FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                states.Add(stateObj);
                foreach (var map in state.Value)
                {
                    //map.Key 条件编号
                    //map.Value 状态编号
                    FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), map.Key);
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), map.Value);
                    //添加映射
                    stateObj.AddMap(triggerID, stateID);
                }
            }
        }

        //切换状态
        public void ChangeActiveState(FSMStateID stateID)
        {
            currentState.ExitState(this);
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.StateID == stateID);
            currentState.EnterState(this);
        }
        #endregion

        #region 为状态与条件提供的成员
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public EnemyStatus status;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("攻击目标标签")]
        public string targetTags = "Unit";
        public float runSpeed = 2;
        public float walkSpeed = 1;
        
        public Transform[] wayPoints;
        public PatrolMode patrolMode;
        public bool isPatrolComplete;
        private AIDestinationSetter destSetter;
        private AIPath aiPath;
        

        protected virtual void InitComponents()
        {
            anim = GetComponentInChildren<Animator>();
            status = GetComponent<EnemyStatus>();
            destSetter = GetComponent<AIDestinationSetter>();
            aiPath = GetComponent<AIPath>();
        }

        //private Dictionary<int, Transform> targetCache;
        private Transform currentTF;
        //查找目标
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == targetTags && currentTF == null)
            {
                targetTF = collision.transform;
                currentTF = targetTF;
                //targetCache.Add(targetTF.GetInstanceID(), targetTF);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == targetTags)
                currentTF = null;
        }

        
        public void MoveToTarget(Vector3 targetPos, float stopDistance, float speed)
        {
            LookRotation(targetPos - transform.position);
            //Debug.DrawLine(transform.position, targetPos);

            if ((Vector3.Distance(targetPos, transform.position) > stopDistance))
            {
               transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }

        public void MoveToTarget(Transform targetPos, float stopDistance, float speed)
        {
            destSetter.target = targetPos;
            aiPath.maxSpeed = speed;
            aiPath.endReachedDistance = stopDistance;
        }

        public void LookRotation(Vector3 direction)
        {
            transform.GetChild(0).right = direction.normalized;
        }

        public void StopMove()
        {
            destSetter.target = transform;
        }

        public virtual void Attack() { } 

        #endregion

    }
}

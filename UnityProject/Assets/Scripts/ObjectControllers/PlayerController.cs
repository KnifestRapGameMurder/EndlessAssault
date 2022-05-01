using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float searchDistance = 20;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private LayerMask barricadeLayer;
    [SerializeField] private string barricadeTag;
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private Gun gun;
    [SerializeField] private HealthControl healthControl;
    [SerializeField] private string damageSound;

    [SerializeField] private CharacterController2D characterController;

    private Rigidbody2D _rb;
    private Transform _transform;

    //private Vector2 _targetPosition => (Vector2)(closestTarget ? closestTarget.position : defaultTarget.position);
    private Transform closestTarget;
    private List<string> barricadeTags;

    private Camera _cam;
    private Touch _touch;
    private Vector2 _touchPosition;
    private Vector2 _previousTouchPosition;
    private bool _isTouchingScreen => Input.touchCount > 0;
    private float _touchCheckRadius;
    private bool _isTouchOnCharacter => _transform.DistanceFrom(_touchPosition) <= _touchCheckRadius;
    private bool _moveToTouch;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _transform = transform;
        _cam = Camera.main;
        _touchCheckRadius = GetComponent<CircleCollider2D>().radius;
    }

    private void Start()
    {
        closestTarget = null;
        barricadeTags = new List<string> { barricadeTag };
        _moveToTouch = false;


        healthControl.DamageTaken += OnDamageTaken;
    }

    private void OnDamageTaken(DamageInfo obj)
    {
        AudioManager.Instance.PlaySound(damageSound);
    }

    private void Update()
    {
        UpdateRotation();
        Shot();
        if (!_isTouchingScreen) return;
        UpdateTouchPosition();
        if (!_isTouchingScreen)
        {
            if (_rb.velocity != Vector2.zero) _rb.StopMoving();
            return;
        }
        SwitchOnTouchPhase();
    }

    private void FixedUpdate()
    {
        
    }

    private void UpdateRotation()
    {
        closestTarget = _transform.GetClosestVisibleOfTag(new SearchData(enemyTag, searchDistance, enemiesLayer, barricadeTags));
        characterController.LookTarget = closestTarget;
    }

    private void Shot() { if (closestTarget) gun.Use(); }

    private void UpdateTouchPosition()
    {
        _touch = Input.GetTouch(0);
        _previousTouchPosition = _touchPosition;
        _touchPosition = _cam.ScreenToWorldPoint(_touch.position);
        
    }

    private void SwitchOnTouchPhase()
    {
        //switch (_touch.phase)
        //{
        //    case TouchPhase.Began:
        //        UpdateMoveToTouchValue();
        //        break;
        //    case TouchPhase.Moved:
        //    case TouchPhase.Stationary:
        //        if (!_moveToTouch)
        //        {
        //            if (_rb.velocity != Vector2.zero) _rb.StopMoving();
        //            _transform.position = _touchPosition;
        //        }
        //        else
        //        {
        //            //_rb.velocity = (_touchPosition - (Vector2)_transform.position).normalized * moveSpeed;
        //            UpdateMoveToTouchValue();
        //        }
        //        break;
        //    case TouchPhase.Ended:
        //    default:
        //        _rb.StopMoving();
        //        break;
        //}

        switch (_touch.phase)
        {
            case TouchPhase.Began:
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                characterController.RequiredPosition = _touchPosition;
                break;
            case TouchPhase.Ended:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                break;
        }
    }

    private void UpdateMoveToTouchValue() => _moveToTouch = !_isTouchOnCharacter;

    private void OnCollisionEnter2D(Collision2D collision) => CheckBarricadeCollision(collision.gameObject.tag);

    private void OnCollisionStay2D(Collision2D collision) => CheckBarricadeCollision(collision.gameObject.tag);

    private void CheckBarricadeCollision(string tag)
    {
        if (tag == barricadeTag)
        {
            UpdateMoveToTouchValue();
        }
    }
}

public class JumpForceChangedData
{
    public readonly int Id;
    public readonly float Force;
    public readonly string Notes;

    public JumpForceChangedData(int id, float force, string notes)
    {
        Id = id;
        Force = force;
        Notes = notes;
    }
}

public static class TransformExt
{
    public static float DistanceFrom(this Transform trans, Vector3 other) => Vector3.Distance(trans.position, other);

    public static void MoveLookAt(this Transform trans, Vector3 targetPos, float speedByDeltaTime) => trans.up = Vector2.MoveTowards(trans.up, targetPos - trans.position, speedByDeltaTime);

    public static Transform GetClosestVisibleOfTag(this Transform trans, SearchData searchData)
    {
        var allInRadius = Physics2D.OverlapCircleAll(trans.position, searchData.Radius, searchData.Layer);
        Transform closest = null;
        foreach (var o in allInRadius)
        {
            if (o.tag != searchData.Tag) continue;
            var distToO = trans.DistanceFrom(o.transform.position);
            bool visible = true;
            var hits = Physics2D.RaycastAll(trans.position, o.transform.position - trans.position);
            foreach (var h in hits)
            {
                if (searchData.BreakTags.Contains(h.transform.tag)) visible = distToO < trans.DistanceFrom(h.transform.position);
            }
            if (visible && (!closest || (distToO <= trans.DistanceFrom(closest.position)))) closest = o.transform;
        }
        return closest;
    }
}

public static class Rigidbody2DExt
{
    public static void MoveTo(this Rigidbody2D rb, Vector3 pos) => rb.position = pos;

    public static void MoveToBy(this Rigidbody2D rb, Vector3 pos, float speedByDeltaTime) => rb.position = Vector3.MoveTowards(rb.position, pos, speedByDeltaTime);

    public static void StopMoving(this Rigidbody2D rb) => rb.velocity = Vector3.zero;
}

public class SearchData
{
    public readonly string Tag;
    public readonly float Radius;
    public readonly LayerMask Layer;
    public readonly List<string> BreakTags;

    public SearchData(string tag, float radius, LayerMask layer, List<string> breakTags)
    {
        Tag = tag;
        Radius = radius;
        Layer = layer;
        BreakTags = breakTags;
    }
}
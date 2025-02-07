using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosit : MonoBehaviour
{
    Transform player;
    LayerMask _targetPlayer;
    [SerializeField] Transform _currentTarget;
    private List<Transform> _targetsInRange = new List<Transform>();
    private int _targetIndex = 0; // Chỉ mục của mục tiêu hiện tại

    private static TargetClosit _instance;
    public static TargetClosit Instance => _instance;

    private void Awake()
    {
        if (Instance != null && Instance.gameObject.GetInstanceID() != gameObject.GetInstanceID())
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
            _instance = this;
    }

    private void Start()
    {
        player = GetComponent<Transform>();
        _targetPlayer = ~LayerMask.GetMask(TagManager.Player);
    }

    void Update()
    {
        UpdateTargetList(); // Cập nhật danh sách mục tiêu

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectNextTarget(); // Chọn mục tiêu tiếp theo
        }
    }

    void UpdateTargetList()
    {
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(player.position, 5f, _targetPlayer);
        _targetsInRange = targetColliders
            .Select(t => t.transform)
            .OrderBy(t => Vector2.Distance(player.position, t.position))
            .ToList();

        // Nếu danh sách trống, reset target
        if (_targetsInRange.Count == 0)
        {
            _currentTarget = null;
            _targetIndex = 0;
        }
    }

    void SelectNextTarget()
    {
        if (_targetsInRange.Count == 0) return;

        _currentTarget = _targetsInRange[_targetIndex];
        Debug.Log("Target: " + _currentTarget.name);

        // Tăng chỉ mục, nếu vượt quá danh sách thì quay lại đầu
        _targetIndex = (_targetIndex + 1) % _targetsInRange.Count;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, 5f);
    }

    public Transform GetTarget() => _currentTarget;
    public void ClearTarget() => _currentTarget = null;
}

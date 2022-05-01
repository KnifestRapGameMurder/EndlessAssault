using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform fillTransform;
    [SerializeField] private float startFillChangeTime;

    private float _fillChangeTime;
    private float _maxHealth;
    private float _currentHealth;
    private float _oldFill;
    private float _currentFill;
    private bool _isAnimating => _fillChangeTime > 0;

    private void Awake()
    {
        _fillChangeTime = 0;
        _oldFill = 0;
        _currentFill = 0;
    }

    private void Update()
    {
        if (_isAnimating)
        {
            _fillChangeTime -= Time.deltaTime;
            SetFill(NormalizeFill(_oldFill + (_currentFill - _oldFill) * (1 - _fillChangeTime / startFillChangeTime)));
        }
        else _oldFill = _currentFill;
    }

    private void SetFill(float fill)
    {
        fillTransform.localScale = new Vector3(fill, 1, 1);
    }

    private float NormalizeFill(float fill) => Mathf.Clamp(fill, 0, 1);

    public void SetMaxHealth(float maxHealth) => _maxHealth = maxHealth;

    public void SetHealth(float health)
    {
        _currentHealth = health;
        UpdateFill();
    }

    private void UpdateFill() => AnimateFillChange(_currentHealth / _maxHealth);

    private void AnimateFillChange(float newFill)
    {
        _oldFill = _currentFill;
        _currentFill = newFill;
        if (!_isAnimating) _fillChangeTime = startFillChangeTime;
    }    
}
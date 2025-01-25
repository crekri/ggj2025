using UnityEngine;

public class PlayerSoakController : MonoBehaviour, IParamModifier
{
    [SerializeField] private float maxSoak = 1f;
    
    public float MaxSoak => maxSoak;

    public float Soak { get; private set; }

    //public bool ReachedSoaked(float amount) => Soak >= amount;
	
    private void Awake()
    {
        Soak = 0f;
        playerParams.AddModifer(this);
    }

    [SerializeField] private PlayerParams playerParams;
    
    [Header("Gravity Curve")][SerializeField] private AnimationCurve gravityCurve;
    [Header("Gravity Increase Rate on Airborne Curve")][SerializeField] private AnimationCurve gravityIncrCurve;
    [Header("Run Damp Curve")][SerializeField] private AnimationCurve runDampCurve;
    [Header("Base Stun Time Curve")][SerializeField] private AnimationCurve baseStunTimeCurve;
    [Header("Reduce Trap Per Click Curve")][SerializeField] private AnimationCurve clickEscapeCurve;
    public void AddSoak(float amount)
    {
        Soak = Mathf.Min(Soak + amount, maxSoak);
    }

    public PlayerStat ApplyTo(PlayerStat stat)
    {
        stat.gravity *= gravityCurve.Evaluate(Soak);
        stat.airBorneGravityIncreaseRate *= gravityIncrCurve.Evaluate(Soak);
        stat.runDamp *= runDampCurve.Evaluate(Soak);
        stat.baseStunTime *= baseStunTimeCurve.Evaluate(Soak);
        stat.reduceTrapPerClick *= clickEscapeCurve.Evaluate(Soak);
        return stat;
    }
    
}
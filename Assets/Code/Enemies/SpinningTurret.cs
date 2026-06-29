using UnityEngine;

public class SpinningTurret : Turret
{
    [SerializeField] private float AngularSpeed = 10f;

    private bool _isSpinning = false;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, AngularSpeed) * Time.deltaTime);
    }

    public override void Activate()
    {
        base.Activate();
        _isSpinning = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _isSpinning = false;
    }
}

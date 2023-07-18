public interface IDamageable
{
    public void TakeDamage(DamageSource damageSource);
}

public interface IWeapon
{
    public void Attack();
    public WeaponInfoSO GetWeaponInfo();
}

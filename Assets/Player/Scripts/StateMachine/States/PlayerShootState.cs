using UnityEngine;

public class PlayerShootState : PlayerState
{
    public float chargeTimer = 0f;
    public float chargeTimerMax = 1f;
    bool readyToShoot;
    EquipmentController.Equipment shootState = EquipmentController.Equipment.Shoot;
    EquipmentController.Equipment swordState = EquipmentController.Equipment.Sword;


    public PlayerShootState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.equipmentController.ChangeState(shootState);
        readyToShoot = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("isCharging", false);

    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if (Input.GetMouseButton(1))
        {
            player.Anim.SetBool("isCharging", true);
            player.ChangeRotationToCursor();
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeTimerMax)
            {
                readyToShoot = true;
                chargeTimer = chargeTimerMax;

            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            chargeTimer = 0f;
            player.Anim.SetBool("isCharging", false);
            if (readyToShoot)
            {
                Fire();
            }
            player.equipmentController.ChangeState(swordState);


            if (player.Speed > 0.1f)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (player.Speed < 0.1f)
            {
                stateMachine.ChangeState(player.IdleState);
            }


        }

    }

    private void Fire()
    {
        Player.Instantiate(player.MagicBall, player.PorjectilePosition.position, player.PorjectilePosition.rotation);
        readyToShoot = false;
    }


}

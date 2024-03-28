using Godot;

namespace SquashtheCreeps3D;

public partial class Mob : CharacterBody3D
{
    [Export]
    public int MinSpeed { get; set; } = 10;

    [Export]
    public int MaxSpeed { get; set; } = 18;

    public void Initialize( Vector3 startPosition, Vector3 playerPosition )
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        Velocity = Vector3.Forward * randomSpeed;
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }
    
    public override void _PhysicsProcess( double delta )
    {
        MoveAndSlide();
    }

    private void OnVisibilityNotifierScreenExited()
    {
        QueueFree();
    }
}
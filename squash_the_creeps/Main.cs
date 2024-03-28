using Godot;

namespace SquashtheCreeps3D;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMobTimerTimeout()
	{
		var mob = MobScene.Instantiate<Mob>();

		var mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		Vector3 playerPosition = GetNode<Player>("Player").Position;
		mob.Initialize(mobSpawnLocation.Position, playerPosition);
		
		AddChild(mob);
	}
}
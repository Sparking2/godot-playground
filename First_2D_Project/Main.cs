using Godot;

namespace First_2D_Project;

public partial class Main : Node
{
    [Export]
    public PackedScene MobScene { get; set; }

    private int _score;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // NewGame();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process( double delta ) { }

    public void GameOver()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();

        GetNode<Hud>("HUD").ShowGameOver();
        
        GetNode<AudioStreamPlayer2D>("Music").Stop();
        GetNode<AudioStreamPlayer2D>("DeadSound").Play();
    }

    public void NewGame()
    {
        GetNode<AudioStreamPlayer2D>("Music").Play();
        
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

        _score = 0;

        var hud = GetNode<Hud>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
    }

    private void OnScoreTimerTimeout()
    {
        _score++;
        GetNode<Hud>("HUD").UpdateScore(_score);
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    private void OnMobTimerTimeout()
    {
        var mob = MobScene.Instantiate<Mob>();

        var mobSpawnLocation = GetNode<PathFollow2D>("Path2D/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        mob.Position = mobSpawnLocation.Position;

        direction += (float) GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2((float) GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }
}
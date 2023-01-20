using _Scripts;
using NSubstitute;
using NUnit.Framework;

public class PlayerTests
{
    [Test]
    public void Initialize_state_initializes_current_state_to_standing_state()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();

        // ACT
        player.InitializeState(player.StandingState);
        
        // ASSERT
        Assert.AreEqual(player.CurrentState, player.StandingState);
    }
    
    [Test]
    public void Initialize_state_initializes_current_state_to_null()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();

        // ACT
        player.InitializeState(null);
        
        // ASSERT
        Assert.AreEqual(player.CurrentState, player.StandingState);
    }
    
    [Test]
    public void Initialize_state_does_not_initialize_previous_state()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();

        // ACT
        player.InitializeState(player.StandingState);
        
        // ASSERT
        Assert.AreEqual(player.PreviousState, null);
    }
    
    [Test]
    public void Initialize_state_does_not_initialize_after_first_call()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();
        player.InitializeState(player.StandingState);

        // ACT
        player.InitializeState(player.WalkingState);
        
        // ASSERT
        Assert.AreEqual(player.CurrentState, player.WalkingState);
    }

    [Test]
    public void Change_state_updates_current_state_to_standing_state()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();
        player.InitializeState(player.CrouchingState);

        // ACT
        player.ChangeState(player.StandingState);
        
        // ASSERT
        Assert.AreEqual(player.CurrentState, player.StandingState);
    }
    
    [Test]
    public void Change_state_does_not_update_current_state_to_null()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();
        player.InitializeState(player.StandingState);

        // ACT
        player.ChangeState(null);
        
        // ASSERT
        Assert.AreEqual(player.CurrentState, player.StandingState);
    }
    
    [Test]
    public void Change_state_updates_previous_state_to_standing_state()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();
        player.InitializeState(player.StandingState);

        // ACT
        player.ChangeState(player.StandingState);
        
        // ASSERT
        Assert.AreEqual(player.PreviousState, player.StandingState);
    }
    
    [Test]
    public void Change_state_does_not_update_previous_state_to_null()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();
        player.InitializeState(player.StandingState);
        player.ChangeState(player.WalkingState);

        // ACT
        player.ChangeState(null);

        // ASSERT
        Assert.AreEqual(player.PreviousState, player.StandingState);
    }
    
    [Test]
    public void Change_state_does_not_update_if_state_uninitialized()
    {
        // ARRANGE
        var player = Substitute.For<IPlayer>();

        // ACT
        player.ChangeState(player.StandingState);

        // ASSERT
        Assert.AreEqual(player.CurrentState, null);
    }
}

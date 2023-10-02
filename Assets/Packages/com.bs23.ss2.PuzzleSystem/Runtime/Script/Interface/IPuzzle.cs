using System;
using Packages.com.bs23.ss2.PuzzleSystem.Runtime.Script.Model;

namespace Package.com.bs23.ss2.PuzzleSystem.Script.Interface
{
    // IPuzzle Interface Definition
public interface IPuzzle
{
    // Actions

    // Initializes the puzzle with its initial configuration.
    // Sets up all dials and parameters.
    void SetupPuzzle();

    // Resets the puzzle to its initial state, clearing any progress made by the player.
    // Invokes OnPuzzleReset event.
    void ResetPuzzle();

    // Initiates the puzzle, allowing the player to interact with it and attempt to solve it.
    // Shows puzzle visuals. Subscribes to the dials event.
    void StartPuzzle();

    // Verifies whether the current combination of dial values matches the correct solution (ResultValues).
    bool CheckResult();

    // Provides a hint or clue to the player, aiding them in solving the puzzle.
    // Useful for UI feedback.
    void ShowHint();

    // Allows the player to exit the puzzle, ending their current session.
    // Hides visual puzzle. Useful for UI action. Unsubscribes dials event.
    void ExitPuzzle();

    // Useful to select dial as per input command.
    void SelectDial(int dialIndex);

    // Unlocks and opens the puzzle, indicating that it has been successfully solved.
    // Saves the puzzle state to unlock. Invokes OnPuzzleUnlocked event.
    void UnlockPuzzle();

    // Properties

    // Currently selected dial.
    Dial SelectedDial { get; set; }

    // All dials information.
    Dial[] Dials { get; }

    // Stores information about the puzzle's configuration, including its dials, hints, and solution.
    PuzzleInfo PuzzleInfo { get; }
    
    // Events

    // Triggered when the player successfully unlocks and solves the puzzle.
    event Action OnPuzzleUnlocked;

    // Triggered when the puzzle is reset to its initial state, either by player action or automatically.
    event Action OnPuzzleReset;
}

public class Dial
{
}
}
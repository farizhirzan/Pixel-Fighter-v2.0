using System.Collections;
using UnityEngine;

public class MainMenuSpriteSetter : MonoBehaviour
{
    public Animator characterAnimator;
    public AnimationClip evolution01;

    void Start()
    {
        if (characterAnimator == null)
        {
            Debug.LogError("Character Animator is not assigned.");
            return;
        }
        SetAnimatorToEvolutionState(PlayableSpriteController.EvoLevel);
    }

    void Update()
    {
        CheckForEvolution();
    }

    void CheckForEvolution()
    {
        if (PlayableSpriteController.EXP >= 1000 && PlayableSpriteController.EvoLevel == 0)
        {
            TriggerEvolution(1);
        }
        else if (PlayableSpriteController.EXP >= 2000 && PlayableSpriteController.EvoLevel == 1)
        {
            TriggerEvolution(2);
        }
    }

    private void TriggerEvolution(int newEvoLevel)
    {
        string evolutionTrigger = newEvoLevel == 1 ? "TriggerEvolve01" : "TriggerEvolve02";
        characterAnimator.SetTrigger(evolutionTrigger);
        characterAnimator.SetBool("isEvolve", true);

        PlayableSpriteController.EvoLevel = newEvoLevel;
        GainValuesAfterEvolution();

        StartCoroutine(UpdateEvolutionStateAfterAnimation(newEvoLevel, evolution01.length));
    }

    private IEnumerator UpdateEvolutionStateAfterAnimation(int newEvoLevel, float animationLength)
    {
        yield return new WaitForSeconds(animationLength);

        UpdateEvolutionState(newEvoLevel);
        characterAnimator.SetBool("isEvolve", false);

        string animationState = GetAnimationStateByColor(PlayableSpriteController.FighterColour, newEvoLevel);
        characterAnimator.Play(animationState, 0, 0);
    }

    public void UpdateEvolutionState(int newEvoLevel)
    {
        PlayableSpriteController.EvoLevel = newEvoLevel;
        if (characterAnimator != null)
        {
            characterAnimator.SetInteger("EvoLevel", newEvoLevel);
        }
    }

    private void SetAnimatorToEvolutionState(int evoLevel)
    {
        string animationState = GetAnimationStateByColor(PlayableSpriteController.FighterColour, evoLevel);
        characterAnimator.Play(animationState, 0, 0);
    }

    private string GetAnimationStateByColor(int fighterColor, int evoLevel)
    {
        switch (fighterColor)
        {
            case 1: return evoLevel == 0 ? "RedPanda" : (evoLevel == 1 ? "RedPandaEvo01" : "RedPandaEvo02");
            case 2: return evoLevel == 0 ? "PyroMonkey" : (evoLevel == 1 ? "PyroMonkeyEvo01" : "PyroMonkeyEvo02");
            case 3: return evoLevel == 0 ? "Taiyaki" : (evoLevel == 1 ? "TaiyakiEvo01" : "TaiyakiEvo02");
            case 4: return evoLevel == 0 ? "UFOCat" : (evoLevel == 1 ? "UFOCatEvo01" : "UFOCatEvo02");
            case 5: return evoLevel == 0 ? "HydroPhantasm" : (evoLevel == 1 ? "HydroPhantasmEvo01" : "HydroPhantasmEvo02");
            case 6: return evoLevel == 0 ? "PurpleFighter" : (evoLevel == 1 ? "PurpleFighterEvo01" : "PurpleFighterEvo02");
            case 7: return evoLevel == 0 ? "PlusPlayer" : (evoLevel == 1 ? "PlusPlayerEvo01" : "PlusPlayerEvo02");
            default: return "PlusPlayer";
        }
    }

    private void GainValuesAfterEvolution()
    {
        int rGain = 0, gGain = 0, bGain = 0;

        switch (PlayableSpriteController.FighterColour)
        {
            case 1:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 50 : 100;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                break;
            case 2:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 15 : 30;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 10 : 20;
                break;
            case 3:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                break;
            case 4:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 50 : 100;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                break;
            case 5:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 50 : 100;
                break;
            case 6:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 0 : 0;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                break;
            case 7:
                rGain = PlayableSpriteController.EvoLevel == 1 ? 10 : 20;
                gGain = PlayableSpriteController.EvoLevel == 1 ? 25 : 50;
                bGain = PlayableSpriteController.EvoLevel == 1 ? 15 : 30;
                break;
            default:
                Debug.LogWarning("Fighter color not recognized. No RGB gain applied.");
                break;
        }

        PlayableSpriteController.RValue += rGain;
        PlayableSpriteController.GValue += gGain;
        PlayableSpriteController.BValue += bGain;

        Debug.Log($"Gained Values for Evo {PlayableSpriteController.EvoLevel} - R: {rGain}, G: {gGain}, B: {bGain}");
        Debug.Log($"Updated RGB - R: {PlayableSpriteController.RValue}, G: {PlayableSpriteController.GValue}, B: {PlayableSpriteController.BValue}");
    }
}

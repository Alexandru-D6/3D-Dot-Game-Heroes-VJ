using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    #region Singleton

    public static SoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Instance = this;
        } else Instance = this;
    }

    #endregion

    [SerializeField] private AudioSource bowShoot;
    [SerializeField] private AudioSource creeperDeath;
    [SerializeField] private AudioSource creeperFuse;
    [SerializeField] private AudioSource creeperHit;
    [SerializeField] private AudioSource enderDeath;
    [SerializeField] private AudioSource enderHit;
    [SerializeField] private AudioSource enderTeleport;
    [SerializeField] private AudioSource minecraftClick;
    [SerializeField] private AudioSource minecraftDoor;
    [SerializeField] private AudioSource minecraftDropBlock;
    [SerializeField] private AudioSource minecraftLevelUp;
    [SerializeField] private AudioSource minecraftTNT;
    [SerializeField] private AudioSource playerEating;
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource playerWalking;
    [SerializeField] private AudioSource respawnSong;
    [SerializeField] private AudioSource skeletonDeath;
    [SerializeField] private AudioSource skeletonHit;
    [SerializeField] private AudioSource skeletonSounds;
    [SerializeField] private AudioSource zombieDeath;
    [SerializeField] private AudioSource zombieHit;
    [SerializeField] private AudioSource zombieSound;
    [SerializeField] private AudioSource chestOpen;
    [SerializeField] private AudioSource chestClose;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource dragonFlamethrower;
    [SerializeField] private AudioSource dragonFly;
    [SerializeField] private AudioSource dragonRoar;
    [SerializeField] private AudioSource othersPadlock;
    [SerializeField] private AudioSource othersVase;
    [SerializeField] private AudioSource playerBoomerang;
    [SerializeField] private AudioSource playerSword;
    [SerializeField] private AudioSource minecraftMainTheme;
    [SerializeField] private AudioSource zeldaMainTheme;

    public void PlayBowShoot() { bowShoot.Play(); }
    public void PlayCreeperDeath() { creeperDeath.Play(); }
    public void PlayCreeperFuse() { creeperFuse.Play(); }
    public void StopCreeperFuse() { creeperFuse.Stop(); }
    public void PlayCreeperHit() { creeperHit.Play(); }
    public void PlayEnderDeath() { enderDeath.Play(); }
    public void PlayEnderHit() { enderHit.Play(); }
    public void PlayEnderTeleport() { enderTeleport.Play(); }
    public void PlayMinecraftClick() { minecraftClick.Play(); }
    public void PlayMinecraftDoor() { minecraftDoor.Play(); }
    public void PlayMinecraftDropBlock() { minecraftDropBlock.Play(); }
    public void PlayMinecraftLevelUp() { minecraftLevelUp.Play(); }
    public void PlayMinecraftTNT() { minecraftTNT.Play(); }
    public void PlayPlayerEating() { playerEating.Play(); }
    public void PlayPlayerHit() { playerHit.Play(); }
    public void PlaySkeletonDeath() { skeletonDeath.Play(); }
    public void PlaySkeletonHit() { skeletonHit.Play(); }
    public void PlaySkeletonSounds() { skeletonSounds.Play(); }
    public void PlayZombieDeath() { zombieDeath.Play(); }
    public void PlayZombieHit() { zombieHit.Play(); }
    public void PlayZombieSound() { zombieSound.Play(); }
    public void PlayChestOpen() { chestOpen.Play(); }
    public void PlayChestClose() { chestClose.Play(); }
    public void PlayDashSound() { dashSound.Play(); }
    public void PlayDragonFlamethrower() { dragonFlamethrower.Play(); }
    public void StopDragonFlamethrower() { dragonFlamethrower.Stop(); }
    public void PlayDragonFly() { dragonFly.Play(); }
    public void StopDragonFly() { dragonFly.Stop(); }
    public void PlayDragonRoar() { dragonRoar.Play(); }
    public void PlayOthersPadlock() { othersPadlock.Play(); }
    public void PlayOthersVase() { othersVase.Play(); }
    public void PlayPlayerBoomerang() { playerBoomerang.Play(); }
    public void PlayPlayerSword() { playerSword.Play(); }
    public void PlayMinecraftMainTheme() { minecraftMainTheme.Play(); }
    public void StopMinecraftMainTheme() { minecraftMainTheme.Stop(); }
    public void PlayZeldaMainTheme() { zeldaMainTheme.Play(); }
    public void StopZeldaMainTheme() { zeldaMainTheme.Stop(); }


    private bool walkingState = false;
    public void PlayPlayerWalking(bool value) { 
        if (walkingState != value) {
            walkingState = value;
            if (value)playerWalking.Play(); 
            else playerWalking.Stop(); 
        }
    }

    public void PlayRespawnSong(bool value) { if (value) respawnSong.Play(); else respawnSong.Stop(); }

}

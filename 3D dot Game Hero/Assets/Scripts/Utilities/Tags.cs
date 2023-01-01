[System.Serializable]
public enum Tags {
    Player,
    Sword,
    Wall,
    GiroCoconut,
    ExternalAnchor,
    Boomerang,
    SceneObjects,
    Hand,
    Chest,
    Arrow,
    Bomb,
    BossKey,
    Bow,
    Coin,
    Creeper,
    Dragon,
    EnderKey,
    Enderman,
    GoldenKey,
    Hamburguer,
    Shield,
    Skeleton,
    SkullKey,
    Vase,
    ZombieArm,
    ExplosionCreeper,
    Floor,
    EndermanParticles,
    CreeperParticles,
    RunningParticles,
    SpawnParticles,
    EndermanArm,
    Debug,
    ExplotionBomb,
    Uknown,
}

[System.Serializable]
public enum Layers{
    Default,
    TransparentFX,
    IgnoreRaycast,
    Empty3,
    Water,
    UI,
    Enemies,
    Particles,
    Empty8,
    Empty9,
    Empty10,
    Empty11,
    Empty12,
    Empty13,
    Empty14,
    Empty15,
    Empty16,
    Empty17,
    Empty18,
    Empty19,
    Empty20,
    Empty21,
    Empty22,
    Empty23,
    Empty24,
    Empty25,
    Debug,
    Key,
    Player,
    SceneObjects,
    Obstacles,
    Weapon
}

public class TagsUtils {
    public static Tags GetTag(string name) {
        return (Tags)System.Enum.Parse( typeof(Tags), name );
    }

    public static Layers GetLayers(string name) {
        return (Layers)System.Enum.Parse( typeof(Layers), name );
    }
}

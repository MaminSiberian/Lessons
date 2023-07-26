using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfigExample : SceneConfig
{
    public const string SCENE_NAME = "SampleScene";
    public override string sceneName => SCENE_NAME;
    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        this.CreateRepository<BankRepository>(repositoriesMap);

        return repositoriesMap;
    }
    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        this.CreateInteractor<BankInteractor>(interactorsMap);
        this.CreateInteractor<PlayerInteractor>(interactorsMap);

        return interactorsMap;
    }
}

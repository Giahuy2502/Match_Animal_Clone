using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpinRewardData", menuName = "SpinRewardData")]
public class SpinReward : ScriptableObject
{
    [SerializeField]private List<GiftReward> rewards;
    public List<GiftReward> Rewards { get => rewards; }

}


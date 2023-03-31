using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBook : MonoBehaviour
{
    public Result GetResult(int decidedCardTypeNum)
    {
      if (decidedCardTypeNum == 0)
      {
        return Result.Boom;
      }
      else if (decidedCardTypeNum == 2)
      {
        return Result.success;
      }
      return Result.silent;
    }
}

public enum Result
{
    Boom,
    silent,
    success,
}
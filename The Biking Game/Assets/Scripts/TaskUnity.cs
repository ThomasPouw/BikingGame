using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public static class TaskUtity
{
    public static async Task WaitUntil(bool predicate, int sleep = 100)
    {
        while (!predicate)
        {
            await Task.Delay(sleep);
        }
    }
}

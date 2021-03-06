﻿using OpenTK;
using System;

namespace Racing.Prizes
{
    /// <summary>
    /// Interface for a prize factory.
    /// </summary>
    interface IPrizeFactory
    {
        Prize GeneratePrize(Random random, Vector2 position);
    }
}

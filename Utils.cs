﻿using System;

namespace snake_game
{
    public class Utils
    {
        public static int RandomNumber(int min, int max) => new Random().Next(min, max);
    }
}

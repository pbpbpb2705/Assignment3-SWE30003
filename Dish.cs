﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Dish : PayableComponent
    {
        public Dish(string name, decimal price) : base(name, price)
        {
        }
    }
}

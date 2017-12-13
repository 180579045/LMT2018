﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMTMainWindow
{
    interface Component
    {
        void Add(Component obj);
        void Remove(Component obj);
        void TraversetoList(int depth);
    }
}
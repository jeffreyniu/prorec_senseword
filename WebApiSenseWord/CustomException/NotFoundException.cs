﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomException
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

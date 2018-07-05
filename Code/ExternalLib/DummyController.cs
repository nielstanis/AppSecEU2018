using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalLib
{
    public class DummyController
    {
        public string Index(string input)
        {
            return $"Hello {input}";
        }
    }
}

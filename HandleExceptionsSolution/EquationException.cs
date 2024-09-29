using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleExceptionsSolution
{
    internal class EquationException : Exception
    {
        public EquationException(string message) : base(message) { }
    }
}

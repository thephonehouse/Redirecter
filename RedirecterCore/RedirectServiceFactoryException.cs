using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedirecterCore
{
    public class RedirectServiceFactoryException : Exception
    {
        public RedirectServiceFactoryException(string? message) : base(message)
        {
        }
    }
}

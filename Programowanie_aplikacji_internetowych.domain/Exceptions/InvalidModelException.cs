using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Exceptions;

public class InvalidModelException : MyAppException
{
    public InvalidModelException(string message) : base(message)
    {
    }
}

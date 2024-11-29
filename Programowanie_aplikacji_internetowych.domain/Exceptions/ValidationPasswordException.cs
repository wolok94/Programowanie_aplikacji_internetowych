using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Exceptions;

public class ValidationPasswordException : MyAppException
{
    public ValidationPasswordException(string message) : base(message)
    {
    }
}

using System;
using System.Collections.Generic;
using Zero.Utils.Models;

namespace Zero.Exceptions
{
    public class DomainValidationsException : Exception
    {
        public IList<Validation> Validations { get; private set; }

        public DomainValidationsException(IList<Validation> validations)
        {
            Validations = validations;
        }
    }
}

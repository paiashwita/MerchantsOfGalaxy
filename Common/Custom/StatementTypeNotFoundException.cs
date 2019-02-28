using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Custom
{
    public class StatementTypeNotFoundException : Exception
    {
        public StatementTypeNotFoundException(): base(Constants.NO_IDEA)
        {

        }

        public StatementTypeNotFoundException(string message)
            : base(message)

        {

        }
    }
}

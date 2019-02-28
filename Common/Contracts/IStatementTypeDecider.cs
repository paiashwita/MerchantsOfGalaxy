using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IStatementTypeDecider
    {
        StatementType StatementType { get; }

        bool isTrue(string inputStatement);
    }
}

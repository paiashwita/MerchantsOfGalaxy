using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public abstract class Statement
    {
        public StatementType StatementType { get; set; }
        public string[] StatementWords { get; set; }

        public Statement()
        {
            this.StatementType = StatementType.NoIdea;
        }

        public Statement(string[] StatementWords)
        {
            this.StatementType = StatementType.NoIdea;
            this.StatementWords = StatementWords;
        }
        public Statement(StatementType StatementType, string[] StatementWords)
        {
            this.StatementType = StatementType;
            this.StatementWords = StatementWords;
        }
    }
}

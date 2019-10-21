using System;
using System.Collections.Generic;
using System.Text;

namespace TEST
{
    interface ITest
    {
        string GetTest(string testData, bool cached = true);
    }
}

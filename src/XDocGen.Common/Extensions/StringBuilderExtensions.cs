using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDocGen.Common.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void AppendFormatLine(this StringBuilder sb, string format, params object[] paramsArr)
        {
            sb.AppendFormat(format, paramsArr);
            sb.AppendLine();
        }
    }
}

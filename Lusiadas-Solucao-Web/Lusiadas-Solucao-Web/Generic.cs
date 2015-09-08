using LDFHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LusiadasSolucaoWeb
{
    public static class Generic
    {
        public static string GetItemValue(LDFTableRow item, string field)
        {
            return item.rowItems.First(q => q.itemColumnName == field).itemValue;
        }
    }
}
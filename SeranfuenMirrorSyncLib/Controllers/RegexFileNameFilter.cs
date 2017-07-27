using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class RegexFileNameFilter : IFileFilter
    {
        private Regex _regex;
        private string _regexExpression;

        public RegexFileNameFilter(string regex)
        {
            _regexExpression = regex;
            _regex = new Regex(regex, RegexOptions.IgnoreCase);
        }

        public bool ShouldFilter(string fullFilePath)
        {
            if (string.IsNullOrEmpty(fullFilePath))
            {
                return false;
            }
            else
            {
                return _regex.IsMatch(Path.GetFileName(fullFilePath));
            }
        }

        public override string ToString()
        {
            return string.Format("Filtering files whose name matches the regex \"{0}\"", _regexExpression);
        }
    }
}

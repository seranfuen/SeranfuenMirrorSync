using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class DirectoryPathRegexFilter : IFileFilter
    {
        private string _regexExpression;
        private Regex _regex;

        public DirectoryPathRegexFilter(string regex)
        {
            _regexExpression = RemoveTrailingSlash(regex);
            _regex = new Regex(_regexExpression, RegexOptions.IgnoreCase);
        }

        public bool ShouldFilter(string fullFilePath)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath))
            {
                return false;
            }
            else
            {
                fullFilePath = RemoveTrailingSlash(fullFilePath);
                return _regex.IsMatch(fullFilePath);
            }
        }


        public override string ToString()
        {
            return string.Format("Filtering directories whose path matches the regex \"{0}\"", _regexExpression);
        }

        private string RemoveTrailingSlash(string path)
        {
            if (path.EndsWith("\\\\"))
            {
                return path.Substring(0, path.Length - 2);
            }
            else
            {
                return path.EndsWith("\\") ? path.Substring(0, path.Length - 1) : path;
            }
        }
    }
}

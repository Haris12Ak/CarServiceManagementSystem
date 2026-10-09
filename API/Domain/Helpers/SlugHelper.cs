using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Helpers
{
    public static class SlugHelper
    {
        public static string Generate(string value)
        {
            var normalized = value.Trim().ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);

            var builder = new StringBuilder();

            foreach (var character in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(character)
                    == UnicodeCategory.NonSpacingMark)
                {
                    continue;
                }

                builder.Append(character);
            }

            var slug = builder.ToString()
                .Normalize(NormalizationForm.FormC);

            slug = Regex.Replace(slug, @"[^a-z0-9]+", "-");
            slug = slug.Trim('-');

            return slug;
        }
    }
}

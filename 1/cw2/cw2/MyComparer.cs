using cw2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw2
{
    internal class MyComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.FirstName} {x.LastName} {x.IndexNumber}", $"{y.FirstName} {y.LastName} {y.IndexNumber}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode($"{obj.FirstName} {obj.LastName} {obj.IndexNumber}");
        }
    }
}

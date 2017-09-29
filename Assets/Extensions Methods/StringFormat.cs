using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class StringFormat
{
    public static string ReplaceFirst( this string text, string search, string replace )
    {
        var pos = text.IndexOf( search, StringComparison.Ordinal );
        if ( pos < 0 )
        {
            return text;
        }
        return text.Substring( 0, pos ) + replace + text.Substring( pos + search.Length );
    }

    public static string ToMeaningfulName( this string value )
    {
        return Regex.Replace( value, "(?!^)([A-Z])", " $1" );
    }

    public static IEnumerable<T> GetRandomSample<T>( this IList<T> list, int sampleSize )
    {
        if ( list == null ) throw new ArgumentNullException( "list" );
        if ( sampleSize > list.Count ) throw new ArgumentException( "sample larger than list", "sampleSize" );
        var indices = new Dictionary<int, int>(); int index;
        var rnd = new Random();

        for ( var i = 0; i < sampleSize; i++ )
        {
            var j = rnd.Next( i, list.Count );
            if ( !indices.TryGetValue( j, out index ) ) index = j;

            yield return list[index];

            if ( !indices.TryGetValue( i, out index ) ) index = i;
            indices[j] = index;
        }
    }
}
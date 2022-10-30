// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;


Console.WriteLine("\t\tEmail addresses");
Console.WriteLine("=========================================================");
if (args.Length > 0)
{
    string urlString = args[0];
    if (!Uri.IsWellFormedUriString(urlString, UriKind.RelativeOrAbsolute))
    {
        throw new ArgumentException("Invalid URL address");
    }

    HttpClient httpClient = new HttpClient();
    Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
    try
    {
        HttpResponseMessage response = await httpClient.GetAsync(urlString);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Błąd w czasie pobierania strony");
            return;
        }
        string responceBody = await response.Content.ReadAsStringAsync();
        MatchCollection emailMatches = emailRegex.Matches(responceBody);

        ICollection<string> hs = new HashSet<string>();
        foreach (Match match in emailMatches)
        {
            hs.Add(match.Value.ToLower());
        }

        if (hs.Count == 0)
        {
            Console.WriteLine("No email address found");
        }
        foreach (string str in hs)
        {
            Console.WriteLine(str);
        }

    }
    catch (HttpRequestException e)
    {
        Console.WriteLine(e);
        Console.WriteLine("Something went wrong!");
    }
    finally
    {
        httpClient.Dispose();
    }
}
else
    throw new ArgumentNullException("At least one argument should be provided");
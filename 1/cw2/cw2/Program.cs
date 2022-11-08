// See https://aka.ms/new-console-template for more information
/*Parametry wywolania
 * 
 * 
 */

using cw2;
using cw2.Models;
using System.Text.Json;

Console.WriteLine("Param 1 " + args[0]); 
Console.WriteLine("Param 2 " + args[1]); 
Console.WriteLine("Param 3 " + args[2]); 
Console.WriteLine("========================================================");

const string logFileName = "log.txt";
const string logPath = "./logs";




if (args.Length == 3)
{
    var path = args[0];
    IEnumerable<string> fileContent = null;
    ICollection<string> fileErrorContent = new List<string>();
    IList<Student> studentsContent = new List<Student>();
    try
    {
        fileContent = await getFileContentAsync(path);

    }
    catch(Exception e) 
    {
        Console.WriteLine(e);
        fileErrorContent.Add(e.ToString());
        saveFile(logFileName,logPath,"");
    }

    foreach (var item in fileContent)
    {
        string[] tmpStud = item.Split(",");

        if (isValidRow(tmpStud))
        {
            studentsContent.Add(new Student
            {
                FirstName = tmpStud[0],
                LastName = tmpStud[1],
                Study = Study.CreateStudy(tmpStud[2], tmpStud[3]),
                IndexNumber = tmpStud[4],
                BirthDate = DateTime.Parse(tmpStud[5]),
                Email = tmpStud[6],
                MothersName = tmpStud[7],
                FathersName = tmpStud[8]
            });
        }
        else
        {
            fileErrorContent.Add(item);
        }
    }

    ICollection<Student> students = new HashSet<Student>(new MyComparer());
    foreach (Student s in studentsContent)
    {
        students.Add(s);
    }

    foreach (Student s in students)
    {
    //    Console.WriteLine(s);
    }

    Console.WriteLine("--------------------------------------------");
    foreach (string s in fileErrorContent)
    {
       // Console.WriteLine(s);
    }

    foreach (Study st in Study.Studies)
    {
       // Console.WriteLine($"{st.StudiesName} {st.Students.Count()}"  ); 
    }

    HashSet<string> hs2 = new HashSet<string>() { "Aaa", "bbb" };

    var json = JsonSerializer.Serialize(students);
    Console.WriteLine(json);











}
else
{
    Console.WriteLine("Nieprawidlowa liczba argumentow wywolania");
}

async Task<IEnumerable<string>> getFileContentAsync (string path)
{
    FileInfo fi = new FileInfo(path);

    var fileContent = new List<string>();

    using (StreamReader stream = new StreamReader(fi.OpenRead()))
    {
        string line = null;
        while ((line = await stream.ReadLineAsync()) != null)
        {
            fileContent.Add(line);
        }
    }

    return fileContent;
}

bool isValidRow(string[] row)
{

    if(row.Length != 9)
        return false;
    foreach (string s in row)
    {
        if (string.IsNullOrEmpty(s))
            return false;
    }
    return true;
}

async void saveFile(string path, string name, string content)
{ 

}

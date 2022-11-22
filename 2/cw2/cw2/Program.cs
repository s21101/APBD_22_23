 /*Parametry wywolania
  *  args[0] - sorce file path
  *  args[1] - save file path
  *  args[2] - type [JSON]
  */

using cw2;
using cw2.Models;
using System.Text.Json;

for (int i = 0; i < args.Length; i++)
{
    Console.WriteLine("Param " + (i + 1) + ": " + args[i]);
}
Console.WriteLine("========================================================");
const string logFileName = "\\log.txt";

if (args.Length != 3)
{
    Console.WriteLine("Illegal count of arguments");
    //throw new AggregateException("Illegal count of arguments");
}

var sorceFilePath = args[0];
    var resultFilePath = args[1];
    ICollection<string> fileErrorContent = new List<string>();

    if(!File.Exists(sorceFilePath))
    {
        throw new AggregateException("Source file does not exists");
    }

    IEnumerable<string> fileContent = null;
    IList<Student> studentsContent = new List<Student>();

    fileContent = await getFileContentAsync(sorceFilePath);

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

    ICollection<Student> studentsUniqe = new HashSet<Student>(new MyComparer());
    foreach (Student s in studentsContent)
    {
        studentsUniqe.Add(s);
    }

    if (fileErrorContent.Count > 0)
    {
        await saveFileAsync(Path.GetDirectoryName(resultFilePath) + logFileName, string.Join(",", fileErrorContent.ToArray()));
    }

    if (studentsUniqe.Count > 0)
    {
        await saveFileAsync(resultFilePath, getStudentsFormatedJson(studentsUniqe));
    }
    else 
    {
        Console.WriteLine("There is nothong to be saved");
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

async Task saveFileAsync(string path, string content)
{
    if (!string.IsNullOrEmpty(content))
    {
        using (StreamWriter sw = new(path))
        {
            await sw.WriteLineAsync(content);
        }
    }
    else
    {
        Console.WriteLine("There is nothing to be saved!");
    }
}

string getStudentsFormatedJson(IEnumerable<Student> data)
{
    var json = JsonSerializer.Serialize(
    new
        {
            CreatedAt =  DateTime.Now,
            Author = "SA",
            data,
            Student.studies
        }
    );
    return json;
}

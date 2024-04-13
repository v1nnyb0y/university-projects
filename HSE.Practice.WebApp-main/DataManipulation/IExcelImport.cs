using System.Data;

namespace DataManipulation
{
    public interface IExcelImport
    {
        DataTable InputFile
        (
            string    path,
            DataTable dt
        );
    }
}
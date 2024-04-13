using System.Data;

namespace DataManipulation
{
    public interface IExcelExport
    {
        void ExportFile
        (
            string    filePath,
            string    roomName,
            DataTable dt
        );
    }
}
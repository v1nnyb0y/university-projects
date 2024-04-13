namespace Provider.Interfaces
{
    public interface IDataFunc
    {
        void ExportSensors
        (
            int roomIndex,
            string filePath
        );

        CurrentProvider LoadSensors
        (
            int    roomIndex,
            string filePath
        );

    }
}
namespace HugeCarWashBot.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}

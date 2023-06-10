namespace RhythmBack.Data.Interface
{
    public interface IGeneroRepository
    {
        Task<string> GetArtistasConcatByCancionIdAsync(int id);
    }
}

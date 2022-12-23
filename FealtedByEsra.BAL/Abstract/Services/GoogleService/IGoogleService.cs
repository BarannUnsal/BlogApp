namespace FealtedByEsra.BAL.Abstract.Services.GoogleService
{
    public interface IGoogleService
    {
        Task<bool> VertifyToken(string token);
    }
}

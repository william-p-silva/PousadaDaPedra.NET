namespace PousadaDaPedra.Application.DTOs.ResponseDTO;

public class SuccessApiDTO<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}
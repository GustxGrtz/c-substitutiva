
public class IMC
{
    public string UsuarioId { get; set; } = Guid.NewGuid().ToString();
    public string? imcs { get; set; }
    public float? Peso { get; set; }
    public float? Altura { get; set; }

}
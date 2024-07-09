
public class Usuario
{
    public string UsuarioId { get; set; } = Guid.NewGuid().ToString();
    public string? Aluno { get; set; }
    public string? DataNascimento { get; set; }
    // public DateOnly DataNascimento { get; set; } = DateOnly.;
    public string? Status { get; set; } = "Não avaliado";
}
public class Loja
{
    
public string? Id { get; set; }

    public required string Login { get; set; }

    public required string Senha
    { 
        get;
        set
        {
            field = value;
        }
    }

    public string? Nome { get; set; }

    public string? Tipo { get; set; }

    public override string ToString()
    {
        return $"Loja: {Nome},Senha: {Senha}, Login: {Login}, Tipo: {Tipo}";
    }
}

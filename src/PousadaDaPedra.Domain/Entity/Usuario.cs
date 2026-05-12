using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Domain.Entity;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public Cargo Cargo { get; private set; }

    private Usuario(){}
    
    public Usuario(string nome, string email, string senha, Cargo? cargo)
    {
        //Sempre verificar se é null primeiro
        if (nome == null || String.IsNullOrWhiteSpace(nome) )
            throw new ArgumentException("O Nome é obrigatorio");
        if (email == null || String.IsNullOrWhiteSpace(email) )
            throw new ArgumentException("O Email é obrigatorio");
        if (cargo != null && cargo != Cargo.Funcionario && cargo != Cargo.Gerente)
            throw new ArgumentException("Cargo Invalido");

        Nome = nome;
        Email = email;
        SenhaHash = senha;
        Cargo = cargo ?? Cargo.Funcionario;
    }


    public void AlterarCargo(Cargo newCargo)
    {
        if (newCargo == this.Cargo)
            throw new ArgumentException("Você não pode trocar o cargo para o mesmo cargo");

        Cargo = newCargo;
    }

    public void AlterarNome(string newNome, string confirmSenha)
    {
        Nome = newNome;
    }
    
    public void AlterarSenha(string newSenha, string confirmSenha)
    {
        SenhaHash = newSenha;
    }
    
    
    public void AlterarEmail(string newEmail, string confirmSenha)
    {
        Email = newEmail;
    }
}
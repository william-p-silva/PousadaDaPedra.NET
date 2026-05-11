using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Domain.Entity;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public Cargo Cargo { get; private set; }

    private Usuario(){}
    
    public Usuario(string nome, string email, string senha, Cargo? cargo)
    {
        //Sempre verificar se é null primeiro
        if (nome == null || nome.Trim() == "" )
            throw new ArgumentException("O Nome é obrigatorio");
        if (email == null || email.Trim() == "" )
            throw new ArgumentException("O Email é obrigatorio");
        if (senha == null || senha.Trim() == "")
            throw new ArgumentException("A Senha é obrigatorio");

        Nome = nome;
        Email = email;
        Senha = senha;
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
        if (confirmSenha != this.Senha)
            throw new ArgumentException("Informações Invalidas");

        Nome = newNome;
    }
    
    public void AlterarSenha(string newSenha, string confirmSenha)
    {
        if (confirmSenha != this.Senha)
            throw new ArgumentException("Informações Invalidas");

        Senha = newSenha;
    }
    
    
    public void AlterarEmail(string newEmail, string confirmSenha)
    {
        if (confirmSenha != this.Senha)
            throw new ArgumentException("Informações Invalidas");

        Email = newEmail;
    }
}
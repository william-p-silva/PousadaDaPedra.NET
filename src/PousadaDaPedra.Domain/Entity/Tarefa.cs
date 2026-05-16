using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Domain.Entity;

public class Tarefa
{
    // --- ESTADO E INVARIANTES (PROPRIEDADES) ---
    // O 'private set' garante o ENCAPSULAMENTO: o estado só muda através de métodos de negócio.
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public Prioridade Prioridade { get; private set; }
    public Status Status { get; private set; } = Status.Pendente;
    public Dificuldade Dificuldade { get; private set; }
    public List<Usuario> Responsaveis { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataTermino { get; private set; }
    public DateTime? Prazo { get; private set; }

    
    private Tarefa(){}

    // --- CONSTRUTOR DE DOMÍNIO ---
    // Garante que uma Tarefa nunca nasça em um estado inválido.
    public Tarefa(string titulo, string descricao, Prioridade? prioridade, Dificuldade? dificuldade, List<Usuario> responsaveis)
    {
        if (responsaveis == null || responsaveis.Count == 0)
            throw new ArgumentException("É preciso atribuir ao menos um responsavel");
        if (titulo == null || titulo.Trim() == "")
            throw new ArgumentException("É necessario o titulo");
        if (descricao == null || descricao.Trim() == "")
            throw new ArgumentException("É necessario a descrição");
        
        Titulo = titulo;
        Descricao = descricao;
        Responsaveis = new List<Usuario>(responsaveis);
        Prioridade = prioridade ?? Prioridade.Baixa;
        Dificuldade = dificuldade ?? Dificuldade.Facil;
    }
    
    
    // --- COMPORTAMENTOS (MÉTODOS DE NEGÓCIO) ---
    // Em Clean Arch, não usamos "Setters". Usamos métodos que descrevem a intenção do negócio.
    public void Iniciar(DateTime? prazo)
    {
        if (Status == Status.Finalizada || Status == Status.EmAndamento)
            throw new ArgumentException("Você não pode iniciar uma tarefa se ela já está iniciada");
        if (this.Prioridade == Prioridade.Alta)
        {
            if (prazo != null && prazo > DateTime.UtcNow)
            {
                Status = Status.EmAndamento;
                DataInicio = DateTime.UtcNow;
                Prazo = prazo;
                return;
            }

            throw new ArgumentException("Para tarefas de alta prioridade é necessario estipular um prazo");
        }
        Status = Status.EmAndamento;
        DataInicio = DateTime.UtcNow;
        Prazo = prazo;
    }

    public void Finalizar()
    {
        if (Status == Status.Pendente || Status == Status.Finalizada)
            throw new ArgumentException("Você não pode Finalizar uma tarefa se ela já está Finalizada ou ainda não foi Iniciada");
        if (Responsaveis.Count <= 0)
            throw new ArgumentException("É preciso um responsavel para finaizar a Tarefa");
        
        Status = Status.Finalizada;
        DataTermino = DateTime.UtcNow;
    }
    
    public void Reabrir(DateTime? newPrazo) 
    {
        if (Status == Status.EmAndamento || Status == Status.Pendente)
            throw new ArgumentException("Você não pode Reabrir um tarefa que já está aberta");
        if (Responsaveis.Count <= 0)
            throw new ArgumentException("É preciso um responsavel para Reabrir a Tarefa");
        if (newPrazo <= DateTime.UtcNow || newPrazo == null)
            throw new ArgumentException("É preciso um novo prazo para reabrir um tarefa");
        
        Status = Status.EmAndamento;
        DataTermino = null;
        Prazo = newPrazo;
        
    }
    
    public void AdicionarResponsavel(Usuario newResponsavel)
    {
        if (Status == Status.Finalizada)
            throw new ArgumentException("Você não pode adicionar um Responsavel em um tarefa Finalizada");
        if (Responsaveis.Contains(newResponsavel))
            throw new ArgumentException("Usuario já adicionada");
        
        Responsaveis.Add(newResponsavel);
    }
    
    public void AlterarPrazo(DateTime? newPrazo)
    {
        if (Status == Status.Finalizada)
            throw new ArgumentException("Você não pode mudar o Prazo de uma tarefa finalizada");
        if (DataInicio >= newPrazo)
            throw new ArgumentException("Você não pode mudar o prazo para uma data anterior ou igual a data de inicio");
        if (newPrazo == null)
            throw new ArgumentException("Prazo invalido");
        Prazo = newPrazo;
    }
    
    public void AlterarDescricao(string? newDescricao)
    {
        if (Status == Status.Finalizada)
            throw new ArgumentException("Você não pode mudar a descrição de uma tarefa finalizada");
        if (String.IsNullOrWhiteSpace(Descricao))
            throw new ArgumentException("Descrição invalida");
        
        Descricao = newDescricao;
    }
    
    public void RemoverResponsavel(Usuario newResponsavel)
    {
        if (!Responsaveis.Contains(newResponsavel))
            throw new ArgumentException("O Usuário não está como responsavel na tarefa");
        if (Responsaveis.Count == 1)
            throw new ArgumentException("Não é possivel deixar uma tarefa sem Responsavel");

        Responsaveis.Remove(newResponsavel);
    }
    
    public void AlterarDificuldade(Dificuldade newDificuldade)
    {
        if (Status == Status.Finalizada)
            throw new ArgumentException("Você não pode mudar a dificuldade se a tarefa já foi finalizada");
        if (newDificuldade == this.Dificuldade)
            throw new ArgumentException("Você precisa de uma nova Dificuldade");
        
        Dificuldade = newDificuldade;
    }
    
    public void AlterarPrioridade( Prioridade newPrioridade) 
    {
        if (Status == Status.Finalizada)
            throw new ArgumentException("Você não pode mudar a prioridade se a tarefa já foi finalizada");
        if (newPrioridade == this.Prioridade)
            throw new ArgumentException("Você precisa de uma nova prioridade");

        Prioridade = newPrioridade;
    }
    
}
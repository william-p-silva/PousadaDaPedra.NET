using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Domain.Entity;

public class Tarefa
{
    public int Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public Prioridade Prioridade { get; private set; } = Prioridade.Baixa;
    public Status Status { get; private set; } = Status.Pendente;
    public Dificuldade Dificuldade { get; private set; } = Dificuldade.Facil;
    public List<Usuario> Responsavel { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataTermino { get; private set; }
    public DateTime Prazo { get; private set; }

    public void Iniciar()
    {
        if (Status == Status.Finalizada || Status == Status.EmAndamento)
            throw new ArgumentException("Você não pode iniciar uma tarefa se ela já está iniciada");
        
        Status = Status.EmAndamento;
        DataInicio = DateTime.UtcNow;
    }

    public void Finalizar()
    {
        if (Status == Status.Pendente || Status == Status.Finalizada)
            throw new ArgumentException("Você não pode Finalizar uma tarefa se ela já está Finalizada ou ainda não foi Iniciada");
        if (Responsavel == null)
            throw new ArgumentException("É preciso um responsavel para finaizar a Tarefa");
        
        Status = Status.Finalizada;
        DataTermino = DateTime.UtcNow;
    }

    public void Reabrir(DateTime newPrazo)
    {
        if (Status == Status.EmAndamento || Status == Status.Pendente)
            throw new ArgumentException("Você não pode Reabrir um tarefa que já está aberta");
        if (Responsavel == null)
            throw new ArgumentException("É preciso um responsavel para Reabrir a Tarefa");
        if (Prazo == null)
            throw new ArgumentException("É preciso um novo prazo para reabrir um tarefa");
        
        Status = Status.Pendente;
        DataInicio = DateTime.UtcNow;
        Prazo = newPrazo;
        //de uma atenção nesta parte (eu deveria ter um parametro no metodo? tipo um DateTime novoPrazo?
    }

    public void AdicionarResponsavel()
    {
        
    }
    
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO
{
    public class IniciarTarefaRequestDTO
    {
        public DateTime? Prazo { get; set; }
        public int Id { get; set; }
    }
}

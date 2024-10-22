namespace JogoWeb.Models
{
    //View que permite acessar as caracteristicas do objeto jogo, muito utilizado
    public class JogoViewModel
    {
        //SEMPRE, EM QUALQUER OCASIÃO, USE OS MESMOS NOMES QUE TEM NO BANCO DE DADOS
        public int id { get; set; }
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string descricao { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public string valor_locacao { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public DateTime data_aquisicao { get; set; }
        public int categoriaID { get; set; }
    }
}
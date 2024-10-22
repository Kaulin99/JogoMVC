namespace JogoWeb.Models
{
    //View que permite acessar as caracteristicas do objeto jogo, muito utilizado
    public class JogoViewModel
    {
        //SEMPRE, EM QUALQUER OCASIÃO, USE OS MESMOS NOMES QUE TEM NO BANCO DE DADOS
        public int id { get; set; }
        public string descricao { get; set; }
        public double? valor_locacao { get; set; }
        public DateTime data_aquisicao { get; set; }
        public int categoriaID { get; set; }
    }
}
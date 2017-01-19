namespace IcatuInsights
{
    public enum AnalytcsEventsType
    {
        [EnumDescription("Login inválido")]
        LoginInvalido,

        [EnumDescription("Login válido")]
        LoginValido,

        [EnumDescription("Usuário/Senha em branco")]
        UsuarioSenhaEmBranco,

        [EnumDescription("Pessoa selecionada")]
        PessoaSelecionada
    }
}
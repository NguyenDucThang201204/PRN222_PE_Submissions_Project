namespace Application.EndPoints
{
    public static class EndPoints
    {
        public static class BearProfile
        {
            public const string Base = "/api/BearProfile";
            public const string GetAll = Base;
            public const string GetById = Base + "/{id}";
            public const string Create = Base;
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            public const string Search = Base + "/search"; 
        }

        public static class Auth
        {
            public const string Login = "/Accounts/login";
        }
    }
}

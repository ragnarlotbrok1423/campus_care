namespace campusCare

{
    using campusCare.vistas;
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateUser), typeof(CreateUser));
        }
    }
}

namespace CaffeeShop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            FormLogin login = new FormLogin();
            login.ShowDialog();

            /*FormEmployee1 employee1 = new FormEmployee1(new Barista("24", "Trần Tuấn", "02231", "1234", new DateTime(2022, 09, 10), 1000, "tuantran"));
            employee1.ShowDialog();*/
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;


namespace YahooSignIn
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        SqlConnection connection;

        public login()
        {
            string constring = "Data Source=DESKTOP-V19M2D9\\sqlexpress;Initial Catalog=yahoopage;Integrated Security=True";
            connection = new SqlConnection(constring);

            InitializeComponent();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

            MainWindow main = new MainWindow();
            this.Content = main;
            connection.Open();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select count(*) from yahoopage where password = @pass", connection);
            
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pass", pass.Password);
            if(connection.State!=System.Data.ConnectionState.Open)
                connection.Open();

            int w = (int)cmd.ExecuteScalar();
                if (w>0)
                {
                loginsss.Visibility = Visibility.Visible;
                uns.Visibility = Visibility.Hidden;
            }

            
            else 
            {
                loginsss.Visibility = Visibility.Hidden;
                uns.Visibility = Visibility.Visible;


            }
        }
    }
}

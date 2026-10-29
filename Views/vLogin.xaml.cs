namespace dGomezTareaS3._1.Views;

public partial class vLogin : ContentPage
{
	public vLogin()
	{
		InitializeComponent();
	}

    private async void btnCalcular_Clicked(object sender, EventArgs e)
    {
		try
		{
			string nombreC = txtNombre.Text;
			string apellidoC = txtApellido.Text;
			string correoC = txtCorreo.Text;
			string salarioC = txtSalario.Text;
			string fechac = DpFecha.Date.ToString();


            Navigation.PushAsync(new vCalcular(nombreC,apellidoC, correoC, salarioC, fechac ));
        }
		catch (Exception)
		{

			throw;
		}
		//await Navigation.PushAsync(new vCalcular());
    }
}
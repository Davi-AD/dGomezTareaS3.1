namespace dGomezTareaS3._1.Views;

public partial class vCalcular : ContentPage
{
	public vCalcular()
	{
		InitializeComponent();
	}
    public vCalcular(string nombreC, string apellidoC, string correoC, string salarioC, string fechaC )
    {
        InitializeComponent();

        lblNombre.Text = nombreC;
        lblApellido.Text = apellidoC;
        lblFecha.Text = fechaC;
        lblCorreo.Text = correoC;
        lblSalario.Text = salarioC;

        if (decimal.TryParse(salarioC, out decimal salario))
        {
            decimal aporte = salario * 0.0945m;

            lblAporte.Text = $"{aporte}";
        }
        else
        {
            lblAporte.Text = "Ingrese un salario válido";
        }


    }
}
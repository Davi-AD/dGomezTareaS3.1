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
			string fechaC = DpFecha.Date.ToString();

            if (pkIdendificacion.SelectedIndex == -1 ||
            string.IsNullOrWhiteSpace(txtIdentificacion.Text) ||
            string.IsNullOrWhiteSpace(nombreC) ||
            string.IsNullOrWhiteSpace(apellidoC) ||
            string.IsNullOrWhiteSpace(correoC) ||
            string.IsNullOrWhiteSpace(salarioC))
            {
                await DisplayAlertAsync(
                "Advertencia",
                "Por favor, complete todos los campos.",
                "Aceptar");
                return;
            }


            if (!decimal.TryParse(salarioC, out decimal salario) || salario <= 0)
            {
                await DisplayAlert(
                    "Advertencia",
                    "Ingrese un salario válido.",
                    "Aceptar");
                return;
            }

            // Calcular aporte al IESS (9,45 %)
            decimal aporte = salario * 0.0945m;

            // Preparar los datos para el archivo TXT
            string datos =
                "IDENTIFICACIÓN: " + pkIdendificacion.Items[pkIdendificacion.SelectedIndex] + "\n" +
                "NÚMERO: " + txtIdentificacion.Text + "\n" +
                "NOMBRE: " + nombreC + "\n" +
                "APELLIDO: " + apellidoC + "\n" +
                "FECHA: " + fechaC + "\n" +
                "CORREO: " + correoC + "\n" +
                "SALARIO: $" + salario.ToString("F2") + "\n" +
                "APORTE IESS: $" + aporte.ToString("F2") + "\n" +
                "-----------------------------------\n";

            // Guardar los datos en un archivo TXT
            string ruta = Path.Combine(
                FileSystem.AppDataDirectory,
                "AporteIESS.txt");

            await File.AppendAllTextAsync(ruta, datos);
            await DisplayAlertAsync(
                "Archivo guardado",
                "El archivo se encuentra en:\n" + ruta,
                "Aceptar");

            // Abrir la ventana de resultados
            await Navigation.PushAsync(
                new vCalcular(nombreC, apellidoC, correoC, salarioC, fechaC));
        }
		catch (Exception)
		{

			throw;
		}
		//await Navigation.PushAsync(new vCalcular());
    }

    private void pkIdendificacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indice = pkIdendificacion.SelectedIndex;

        txtIdentificacion.Text = "";
        txtIdentificacion.IsEnabled = true;

        if (indice == 0) // Cédula
        {
            txtIdentificacion.MaxLength = 10;
            txtIdentificacion.Keyboard = Keyboard.Numeric;
        }
        else if (indice == 1) // RUC
        {
            txtIdentificacion.MaxLength = 13;
            txtIdentificacion.Keyboard = Keyboard.Numeric;
        }
        else if (indice == 3) // Pasaporte
        {
            txtIdentificacion.MaxLength = int.MaxValue;
            txtIdentificacion.Keyboard = Keyboard.Text;
        }
    }

    private void txtIdentificacion_TextChanged(object sender, TextChangedEventArgs e)
    {
        int indice = pkIdendificacion.SelectedIndex;
        string texto = e.NewTextValue ?? "";
        string resultado = "";

        foreach (char c in texto)
        {
            if (indice == 0 || indice == 1)
            {
                if (char.IsDigit(c))
                    resultado += c;
            }
            else if (indice == 2)
            {
                if (char.IsLetterOrDigit(c))
                    resultado += c;
            }
        }

        if (texto != resultado)
            txtIdentificacion.Text = resultado;
    }
}
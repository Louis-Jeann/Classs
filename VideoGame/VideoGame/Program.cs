using System.Linq;
using Spectre.Console;
using VideoGame.Models;
using VideoGame.Services;

namespace VideoGame
{
    internal class Program
    {
        static bool running = true;
        static VideoGameService service = new VideoGameService();

        static void Main(string[] args)
        {
            while (running)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]Sistema de Gestión de Videojuegos[/]")
                    .AddChoices(
                        "Mostrar videojuegos",
                        "Agregar videojuego",
                        "Eliminar videojuego",
                        "Salir"));

                var juegos = service.FindAll();

                switch (opcion)
                {
                    case "Mostrar videojuegos":

                        AnsiConsole.Clear();

                        var tabla = new Table();

                        tabla.AddColumn("ID");
                        tabla.AddColumn("Nombre");
                        tabla.AddColumn("Género");
                        tabla.AddColumn("Precio");

                        foreach (var juego in juegos)
                        {
                            tabla.AddRow(
                                juego.Id.ToString(),
                                juego.Nombre,
                                juego.Genero,
                                "$" + juego.Precio);
                        }

                        AnsiConsole.Write(tabla);

                        break;

                    case "Agregar videojuego":

                        string nombre = AnsiConsole.Ask<string>("Nombre:");
                        string genero = AnsiConsole.Ask<string>("Género:");
                        decimal precio = AnsiConsole.Ask<decimal>("Precio:");

                        int id = juegos.Any() ? juegos.Max(x => x.Id) + 1 : 1;

                        service.Create(new Juego
                        {
                            Id = id,
                            Nombre = nombre,
                            Genero = genero,
                            Precio = precio
                        });

                        AnsiConsole.MarkupLine("[green]Videojuego agregado correctamente.[/]");

                        break;

                    case "Eliminar videojuego":

                        int eliminar = AnsiConsole.Ask<int>("ID del videojuego:");

                        if (service.Delete(eliminar))
                            AnsiConsole.MarkupLine("[green]Videojuego eliminado.[/]");
                        else
                            AnsiConsole.MarkupLine("[red]No se encontró el videojuego.[/]");

                        break;

                    default:

                        running = false;

                        break;
                }
            }
        }
    }
}
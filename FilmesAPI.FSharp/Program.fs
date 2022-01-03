namespace FilmesAPI.FSharp

#nowarn "20"
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore
open FilmesAPI.FSharp.Data

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()
        builder.Services.AddDbContext<FilmeContext>(fun opt -> opt.UseSqlite(connectionString = (builder.Configuration.GetSection("ConnectionStrings").Item "FilmeConnection")) |> ignore )

        let app = builder.Build()

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode

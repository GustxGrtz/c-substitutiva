using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

//Configurar a política de CORS
builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();


app.MapGet("/", () => "Prova A1");

//ENDPOINTS DE usuario
//GET: http://localhost:5273/usuarios/listar
app.MapGet("/usuarios/listar/", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Usuarios.Any())
    {
        return Results.Ok(ctx.Usuarios.ToList());
    }
    return Results.NotFound("Nenhuma usuario encontrada");
});

//POST: http://localhost:5273/usuarios/cadastrar
app.MapPost("/usuarios/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Usuario usuarios) =>
{
    ctx.Usuarios.Add(usuarios);
    ctx.SaveChanges();
    return Results.Created("", usuarios);
});

//PUT: http://localhost:5273/usuarios/alterar/{id}
app.MapPut("/usuarios/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    //Implementar a alteração do status do usuario
    Usuario? usuarios = ctx.Usuarios.Find(id);
    if (usuarios is null)
    {
        return Results.NotFound("usuario não encontrada");
    }

    if (usuarios.Status == "Não iniciada")
    {
        usuarios.Status = "Em andamento";
    }
    else if (usuarios.Status == "Em andamento")
    {
        usuarios.Status = "Concluída";
    }

    ctx.Usuarios.Update(usuarios);
    ctx.SaveChanges();
    return Results.Ok(ctx.Usuarios.ToList());
});


app.MapGet("IMC/listar{id}", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.iMCs.Any())
    {
        return Results.Ok(ctx.iMCs.ToList());
    }
    return Results.NotFound("Nenhum Imc encontrado");
});


// ENDPOINTS DE usuario
//GET: http://localhost:5273/usuarios/listar
// app.MapGet("/IMC/calcular/", ([FromServices] AppDataContext ctx) =>
// {
//     if (IMC < 18.5)
//     {
//         return Results.Ok(ctx.Usuarios.Where(x => x.Status == "Normal").ToList());
//     }
//     if (IMC > 18.5 && IMC < 24.9)
//     {
//         return Results.Ok(ctx.Usuarios.Where(x => x.Status == "Sobrepeso").ToList());
//     }
//     if (IMC > 25.0 && IMC < 29.9)
//     {
//         return Results.Ok(ctx.Usuarios.Where(x => x.Status == "Obesidade").ToList());
//     }
//     if (IMC > 30.0 && IMC < 39.9)
//     {
//         return Results.Ok(ctx.Usuarios.Where(x => x.Status == "Obesidade Grave").ToList());
//     }
//     if (IMC > 40.0)
//     {
//         return Results.Ok(ctx.Usuarios.Where(x => x.Status == "Obesidade Grave").ToList());
//     }

//     return Results.NotFound("Nenhum imc encontrada");
// });

//POST: http://localhost:usuarios/cadastrar
app.MapPost("IMC/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] IMC imc) =>
{
    ctx.iMCs.Add(imc);
    ctx.SaveChanges();
    return Results.Created("", imc);
});

//PUT: http://localhost:usuarios/alterar/{id}
app.MapPut("IMC/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    IMC? imc = ctx.iMCs.Find(id);
    if (imc is null)
    {
        return Results.NotFound("IMC não calculado");
    }
    ctx.iMCs.Update(imc);
    ctx.SaveChanges();
    return Results.Ok(ctx.Usuarios.ToList());
});



app.MapPut("/usuarios/buscar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id, Usuario aluno) =>
{
    //Implementar a alteração do status do usuario
    Usuario? usuarios = ctx.Usuarios.Find(aluno);
    if (usuarios is null)
    {
        return Results.NotFound("usuario não encontrada");
    }

    ctx.Usuarios.Update(usuarios);
    ctx.SaveChanges();
    return Results.Ok(ctx.Usuarios.ToList());
});

app.UseCors("Acesso Total");

app.Run();


//app run com retorno null?

//nao achei o problema professor, complicado
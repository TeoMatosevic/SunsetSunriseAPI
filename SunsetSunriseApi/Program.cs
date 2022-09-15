using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Context;
using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    foreach (string file in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly))
        c.IncludeXmlComments(file);

    c.UseAllOfForInheritance();
    c.DocInclusionPredicate((_, _) => true);

});

/*builder.Services.AddDbContext<HrNetStagingContext>(o => o.UseSqlServer("Server=" + builder.Configuration.GetConnectionString("Server") + "; Database=" + builder.Configuration.GetConnectionString("Database") + "; User Id=" + builder.Configuration.GetConnectionString("User") + "; Password=" + builder.Configuration.GetConnectionString("Password") + ";"));

var app = builder.Build();

if (!Database.Exists("Server=" + builder.Configuration.GetConnectionString("Server") + "; Database=" + builder.Configuration.GetConnectionString("Database") + "; User Id=" + builder.Configuration.GetConnectionString("User") + "; Password=" + builder.Configuration.GetConnectionString("Password") + ";")) {
    using (var connection = new SqlConnection("Server=" + builder.Configuration.GetConnectionString("Server") + "; User Id=" + builder.Configuration.GetConnectionString("User") + "; Password=" + builder.Configuration.GetConnectionString("Password") + ";")) {
        SqlCommand cmd = new SqlCommand("RESTORE DATABASE [HrNetStaging] FROM  DISK = " + builder.Configuration.GetConnectionString("BackupConnectionString_bak") + "HrNetStaging.bak' WITH  FILE = 8,  MOVE N'HrNetStaging' TO " + builder.Configuration.GetConnectionString("BackupConnectionString_other") + "HrNetStaging.mdf',  MOVE N'HrNetStaging_log' TO " + builder.Configuration.GetConnectionString("BackupConnectionString_other") + "HrNetStaging_log.ldf',  NOUNLOAD,  STATS = 5", connection);
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
    }
}*/
builder.Services.AddDbContext<SunsetSunriseDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Docker")));

var app = builder.Build();

if (!Database.Exists(builder.Configuration.GetConnectionString("Docker"))) {

    using (var connection = new SqlConnection("Server=db,1433; User Id=sa; Password=WeatherApi9!;")) {
        SqlCommand cmd = new SqlCommand("RESTORE DATABASE [SunsetSunriseDB] FROM  DISK = N'/var/lib/mssql/backup/SunsetSunriseDB.bak' WITH  FILE = 3,  MOVE N'SunsetSunriseDB' TO N'/var/opt/mssql/data/SunsetSunriseDB.mdf',  MOVE N'SunsetSunriseDB_log' TO N'/var/opt/mssql/data/SunsetSunriseDB_log.ldf',  NOUNLOAD,  STATS = 5", connection);
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
    }
}

// Configure the HTTP request pipeline.
/* if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
} */

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

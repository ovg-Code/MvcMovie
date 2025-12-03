using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace ari2._0.Controllers;

/// <summary>
/// Controlador para operaciones de diagnostico y consulta de base de datos.
/// </summary>
public class DatabaseController : Controller
{
    private readonly IConfiguration _configuration;

    public DatabaseController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> CheckConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            return Json(new { 
                connected = true, 
                message = "Conexión exitosa a la base de datos ARIV2",
                database = "ariv2",
                host = "ariv2-crm-db.curms68ogomm.us-east-1.rds.amazonaws.com"
            });
        }
        catch (Exception ex)
        {
            return Json(new { 
                connected = false, 
                message = $"Error de conexión: {ex.Message}"
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTableStructure(string tableName)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            var query = @"
                SELECT 
                    column_name,
                    data_type,
                    character_maximum_length,
                    is_nullable,
                    column_default
                FROM information_schema.columns
                WHERE table_schema = 'public' 
                AND table_name = @tableName
                ORDER BY ordinal_position;
            ";
            
            await using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("tableName", tableName);
            
            var columns = new List<object>();
            await using var reader = await cmd.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                columns.Add(new
                {
                    column_name = reader.GetString(0),
                    data_type = reader.GetString(1),
                    max_length = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                    is_nullable = reader.GetString(3),
                    column_default = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            
            return Json(new { 
                table = tableName,
                columns = columns
            });
        }
        catch (Exception ex)
        {
            return Json(new { 
                error = true, 
                message = $"Error: {ex.Message}"
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTablesStructure()
    {
        var tables = new[] {
            "countries", "genders", "actor_types", "phone_types", "address_types",
            "states", "cities", "municipalities", "neighborhoods", "zip_codes",
            "actors", "customers", "phones", "emails", "addresses",
            "identity_cards", "identity_card_types", "actor_relationships",
            "relationship_types", "social_networks", "customer_public_status_types"
        };

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var allStructures = new Dictionary<string, List<object>>();
        
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            foreach (var tableName in tables)
            {
                var query = @"
                    SELECT 
                        column_name,
                        data_type,
                        character_maximum_length,
                        is_nullable,
                        column_default
                    FROM information_schema.columns
                    WHERE table_schema = 'public' 
                    AND table_name = @tableName
                    ORDER BY ordinal_position;
                ";
                
                await using var cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("tableName", tableName);
                
                var columns = new List<object>();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    columns.Add(new
                    {
                        column_name = reader.GetString(0),
                        data_type = reader.GetString(1),
                        max_length = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        is_nullable = reader.GetString(3),
                        column_default = reader.IsDBNull(4) ? null : reader.GetString(4)
                    });
                }
                
                allStructures[tableName] = columns;
            }
            
            return Json(allStructures);
        }
        catch (Exception ex)
        {
            return Json(new { 
                error = true, 
                message = $"Error: {ex.Message}"
            });
        }
    }
}
